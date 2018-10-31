using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TaskManager.Services;
using TaskManager.Data.Entities;
using System.Configuration;

namespace TaskManager.Web.Views
{
    public partial class NewTask : System.Web.UI.Page
    {

        #region Variables

        private Task _editTask = new Task();
        private int _id = -1;
        private int _pr = -1;

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            ReceiveQueryParametrs();
            if (!Page.IsPostBack)
            {
                FillControlsForChoose();
                if (_id >= 0)                                               // Edit task
                {
                    _editTask = Global.ExcepHandler.Process(() => Global.TasksBlo.GetTaskById(_id));
                    if (_editTask != null)
                        FillTaskControls();
                }
            }
        }

        /// <summary>
        /// Set SelectedIndex for DropDownList controls if we edit task or have specific project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            if ((_id >= 0)&&(_editTask != null))                                              //Set selectIndex for update Task
            {
                ProjectShortNameDrop.SelectedIndex = GetSelectedIndexById(_editTask.ProjectId, ProjectShortNameDrop);
                TaskStatusDropFiled.SelectedIndex = GetSelectedIndexById(_editTask.StatusId, TaskStatusDropFiled);
            }

            if (_pr > 0)                                               //Set project dropDownList disabled
            {
                ProjectShortNameDrop.Enabled = false;
                ProjectShortNameDrop.SelectedIndex = GetSelectedIndexById(_pr, ProjectShortNameDrop);
            }
        }
        
        /// <summary>
        /// Submit changes and insert task into DataBase
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                FillTaskFromControls();

                if (_id > 0)                                        //Edit task
                {
                    int taskId;
                    int.TryParse(TaskIDField.Text, out taskId);
                    if (taskId > 0)
                    {
                        _editTask.TaskId = taskId;
                        Global.ExcepHandler.Process(() => Global.TasksBlo.UpdateTask(_editTask));
                    }
                }
                else                                                //Create task
                {
                    Global.TasksBlo.InsertTask(_editTask);
                }

                if (_pr >= 0)                                         //Redirect
                    Response.Redirect("~/Views/NewProject.aspx?id=" + _pr);
                else
                    Response.Redirect("~/Views/Tasks.aspx");
            }
        }

        #region Supporting methods
        
        /// <summary> 
        /// Return item index for select.
        /// Call from Page_PreRenderComplete after databinding
        /// </summary>
        /// <param name="id">int projectId</param>
        /// <param name="dropDownList">DropDownList dropDownList</param>
        /// <returns></returns>
        private int GetSelectedIndexById(int id, DropDownList dropDownList)
        {
            int k, foundItem = 0;
            for (int i = 0; i < dropDownList.Items.Count; i++)
            {
                int.TryParse(dropDownList.Items[i].Value, out k);
                if (k == id) foundItem = i;
            }
            return foundItem;
        }

        /// <summary>
        /// Receive parametrs from QueryString
        /// </summary>
        private void ReceiveQueryParametrs()
        {
            if (Request.QueryString.HasKeys() )
                {
                    if (Request.QueryString["id"] != null)
                        int.TryParse(Request.QueryString["id"], out _id);
                    
                    if (Request.QueryString["pr"] != null)
                        int.TryParse(Request.QueryString["pr"], out _pr);
                }
        }

        /// <summary>
        /// Fills page controls with starting data
        /// </summary>
        private void FillControlsForChoose()
        {
            ProjectShortNameDrop.DataTextField = "PrShortName";                     //Fill Project short names
            ProjectShortNameDrop.DataValueField = "ProjectId";
            ProjectShortNameDrop.DataSource = Global.ExcepHandler.Handle(() => Global.ProjectsBlo.GetAllProjects());

            TaskStatusDropFiled.DataTextField = "StatusTitle";                      //Fill Task Status
            TaskStatusDropFiled.DataValueField = "StatusId";
            TaskStatusDropFiled.DataSource = Global.ExcepHandler.Handle(() => Global.TasksBlo.GetAllStatus());
            PerformersList.DataTextField = "Soname";

            PerformersList.DataValueField = "PersonId";
            PerformersList.DataSource = Global.ExcepHandler.Handle(() => Global.PersonsBlo.GetAllPersons());          //Fill Performers
            this.DataBind();
        }

        /// <summary>
        /// Fills the remaining controls with current task data
        /// </summary>
        private void FillTaskControls()
        {
            TaskIDField.Text = _editTask.TaskId.ToString();
            TaskNameField.Text = _editTask.TaskName;
            TaskHoursField.Text = _editTask.Hours.ToString();
            TaskBeginDateField.Text = _editTask.BeginTime.ToShortDateString();
            TaskEndDateField.Text = _editTask.EndTime.ToShortDateString();

            foreach (Person pr in _editTask.TaskPersons)                        //Fill persons list
            {
                foreach (ListItem li in PerformersList.Items)
                {
                    if (int.Parse(li.Value) == pr.PersonID)
                        li.Selected = true;
                }
            }
        }

        /// <summary>
        /// Fills task from form contols
        /// </summary>
        private void FillTaskFromControls()
        {
            DateTime beginTime;                                                      //Fill task
            DateTime.TryParse(TaskBeginDateField.Text, out beginTime);
            _editTask.BeginTime = beginTime;

            DateTime endTime;
            DateTime.TryParse(TaskEndDateField.Text, out endTime);
            _editTask.EndTime = endTime;

            int hours;
            int.TryParse(TaskHoursField.Text, out hours);
            _editTask.Hours = hours;

            int projectId;
            int.TryParse(ProjectShortNameDrop.SelectedItem.Value, out projectId);
            _editTask.ProjectId = projectId;

            _editTask.PrShortName = ProjectShortNameDrop.SelectedItem.Text;
            _editTask.Status = TaskStatusDropFiled.SelectedItem.Text;

            int statusId;
            int.TryParse(TaskStatusDropFiled.SelectedItem.Value, out statusId);
            _editTask.StatusId = statusId;

            _editTask.TaskName = TaskNameField.Text;

            List<Person> personsForAdd = new List<Person>();
            foreach (ListItem li in PerformersList.Items)                            //Add selected performers
            {
                if (li.Selected)
                    personsForAdd.Add(Global.ExcepHandler.Process(() => Global.PersonsBlo.GetPersonById(int.Parse(li.Value))));
            }
            _editTask.TaskPersons = personsForAdd;
        }

        #endregion

        #endregion
    }
}