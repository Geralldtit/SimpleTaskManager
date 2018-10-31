using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TaskManager.Properties;
using TaskManager.Services;
using TaskManager.Data.Entities;
using System.Configuration;

namespace TaskManager.Web.Views
{
    public partial class NewProject : System.Web.UI.Page
    {

        #region Variables

        private Project _project = new Project();
        private int _id = -1;

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            ReceiveQueryParametrs();
            if ((!IsPostBack) && (_id > 0))
            {
                _project = Global.ExcepHandler.Process(() => Global.ProjectsBlo.GetProjectByProjectId(_id));
                if (_project != null)
                {
                    FillFormFromProject();
                    CreateTaskTable(_id);
                }
            }
        }

        /// <summary>
        /// Create and fill task table
        /// </summary>
        /// <param name="projectId">int projectId</param>
        protected void CreateTaskTable(int projectId)
        {
            List<Task> projectTasks = new List<Task>() ;
            foreach(Task ts in Global.ExcepHandler.Process(() => Global.TasksBlo.GetAllTasks()))
            {
                if(ts.ProjectId == projectId)
                {
                    projectTasks.Add(ts);
                }
            }
            GridProjectTasks.DataSource = projectTasks;
            GridProjectTasks.DataBind();
        }

        /// <summary>
        /// Insert/update project and redirect to add new task form
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void AddNewTaskLink_Click(object sender, EventArgs e)
        {
            int projectId = InsertOrUpdateProject();                        //Create/update project and get him id
            Response.Redirect("~/Views/NewTask.aspx?pr=" + projectId);      //Redirect to NewTask form
        }

        /// <summary>
        /// If data valid confirm creating/updating project and redirect to projects list
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            InsertOrUpdateProject();
            Response.Redirect("~/Views/Projects.aspx?");                    //Redirect to Projects form
        }

        #region Supporting Methods

        /// <summary>
        /// Fill form from project record
        /// </summary>
        private void FillFormFromProject()
        {
            if (_project != null)
            {
                ProjectField.Text = _project.ProjectID.ToString();
                ProjectNameField.Text = _project.PrName;
                PrShortNameField.Text = _project.PrShortName;
                ProjectDescriptionField.Text = _project.Description;
            }
        }

        /// <summary>
        /// Fill project parameter from form
        /// </summary>
        private void FillProjectFromForm()
        {
            _project.PrName = ProjectNameField.Text;
            _project.PrShortName = PrShortNameField.Text;
            _project.Description = ProjectDescriptionField.Text;
        }

        /// <summary>
        /// Read input data and if it valid update/insert into DataBase
        /// </summary>
        /// <returns>int projectId</returns>
        private int InsertOrUpdateProject()
        {
            if (Page.IsValid)
            {
                FillProjectFromForm();
                if (_id >= 0)                                               //update project
                {
                    int projectId;
                    int.TryParse(ProjectField.Text,out projectId);
                    if (projectId > 0)
                    {
                        _project.ProjectID = projectId;
                        Global.ExcepHandler.Process(() => Global.ProjectsBlo.UpdateProject(_project));
                    }
                }
                else                                                        //Insert project
                {
                    _project.ProjectID = Global.ExcepHandler.Process(() => Global.ProjectsBlo.InsertProject(_project));
                }
            }
            return _project.ProjectID;
        }

        /// <summary>
        /// Receive parametrs from QueryString
        /// </summary>
        private void ReceiveQueryParametrs()
        {
            if (Request.QueryString.HasKeys() && (Request.QueryString["id"] != null))
                int.TryParse(Request.QueryString["id"], out _id);
        }

        #endregion

        #endregion
    }
}