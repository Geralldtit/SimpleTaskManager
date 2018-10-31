using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskManager
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session != null)
                if (Session["LastException"] != null)
                {
                    Exception exception = (Exception)Session["LastException"];
                    ltrError.Text = exception.ToString();
                }
                else ltrError.Text = "No error found!";
        }
    }
}