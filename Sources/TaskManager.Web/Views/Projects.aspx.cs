using System;
using System.Web.UI.WebControls;
using System.Configuration;
using TaskManager.Services;
using TaskManager.Data.Entities;

namespace TaskManager.Web.Views
{
    public partial class Projects : System.Web.UI.Page
    {
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["del"] == "1")          // Delete project if QueryString has "del=1"
                    {
                        int id;
                        int.TryParse(Request.QueryString["id"], out id);
                        if (id > 0)
                        {
                            Global.ExcepHandler.Handle(() => Global.ProjectsBlo.DeleteProject(id));
                        }
                    }
                }
                GridProjects.DataSource = Global.ExcepHandler.Handle(() => Global.ProjectsBlo.GetAllProjects());
                GridProjects.DataBind();
            }
        }

        protected void CreateNewProjectLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/NewProject.aspx");
        }

        #endregion
    }
}
