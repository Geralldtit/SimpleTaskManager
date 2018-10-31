using System;
using TaskManager.Data.Entities;

namespace TaskManager.Views
{
    public partial class NewPerson : System.Web.UI.Page
    {
        #region Variables

        private int _id = -1;

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            ReceiveQueryParameters();

            if (!Page.IsPostBack)
            {
                if (_id > 0)
                {
                    Person editPerson = Global.ExcepHandler.Process(() => Global.PersonsBlo.GetPersonById(_id));
                    FillsFormFromPerson(editPerson);
                }
            }
        }
        
        /// <summary>
        /// Create or update person
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void SubmitPerson_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (_id >= 0) //update person
                {
                    Person updPerson = GetPersonFromForm();
                    int personId;
                    int.TryParse(PersonIDField.Text, out personId);
                    updPerson.PersonID = personId;
                    Global.ExcepHandler.Process(() => Global.PersonsBlo.UpdatePerson(updPerson));
                }
                else         //Create person
                {
                    Global.ExcepHandler.Process(() => Global.PersonsBlo.InsertPerson(GetPersonFromForm()));
                }
                Response.Redirect("~/Views/Persons.aspx");
            }
        }

        #region Supporting methods

        /// <summary>
        /// Get person fields from form controls
        /// </summary>
        /// <returns>Person</returns>
        private Person GetPersonFromForm()
        {
            return new Person
                       {
                           Name = PersonNameField.Text,
                           Soname = PersonSonameField.Text,
                           SecondName = PersonSecNameField.Text,
                           Position = PersonPositionField.Text
                       };
        }

        /// <summary>
        /// Receive parametrs from QueryString
        /// </summary>
        private void ReceiveQueryParameters()
        {
            if (Request.QueryString.HasKeys())
                if (Request.QueryString["id"] != null)
                    int.TryParse(Request.QueryString["id"], out _id);
        }
        
        /// <summary>
        /// Fills form controls from person object
        /// </summary>
        /// <param name="person"></param>
        private void FillsFormFromPerson(Person person)
        {
            if (person != null)
            {
                PersonIDField.Text = person.PersonID.ToString();
                PersonNameField.Text = person.Name;
                PersonSonameField.Text = person.Soname;
                PersonSecNameField.Text = person.SecondName;
                PersonPositionField.Text = person.Position;
            }
        }

        #endregion

        #endregion
    }
}