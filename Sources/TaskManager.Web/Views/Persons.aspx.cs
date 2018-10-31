using System;
using System.Web.UI.WebControls;
using System.Configuration;
using TaskManager.Properties;
using TaskManager.Services;
using TaskManager.Data.Entities;

namespace TaskManager.Web.Views
{
    public partial class Persons : System.Web.UI.Page
    {   
        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["del"] == "1")
                    {
                        int id;
                        int.TryParse(Request.QueryString["id"], out id);
                        if (id > 0)
                             Global.ExcepHandler.Process(() => Global.PersonsBlo.DeletePerson(id));
                    }
                }
				
                GridPersons.DataSource = Global.ExcepHandler.Handle(() => Global.PersonsBlo.GetAllPersons());
                GridPersons.DataBind();
            }
        }

        protected void LnkCreatePersonClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/NewPerson.aspx");
        }

        #endregion
    }
}