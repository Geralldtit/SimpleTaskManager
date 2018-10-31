using System;
using System.Web.UI.WebControls;
using TaskManager.Services;
using TaskManager.Data.Entities;
using System.Configuration;

namespace TaskManager.Web.Views
{
    public partial class Tasks : System.Web.UI.Page
    {   
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["del"] == "1")              //Delete Task if QueryString has "del=1"
                    {
                        int id=-1;
                        int.TryParse(Request.QueryString["id"], out id);
                        if (id > 0)
                        {
                            Global.ExcepHandler.Process(() => Global.TasksBlo.DeleteTask(id));
                        }

                        int pr=-1;
                        int.TryParse(Request.QueryString["pr"], out pr);
                        if (pr > 0)
                            Response.Redirect("~/Views/NewProject.aspx?id=" + pr);
                    }
                }

                GridTasks.DataSource = Global.ExcepHandler.Handle(() => Global.TasksBlo.GetAllTasks());
                GridTasks.DataBind();
            }
            
        }

        protected void CreateTaskLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/NewTask.aspx?");
        }

        #endregion
    }
}