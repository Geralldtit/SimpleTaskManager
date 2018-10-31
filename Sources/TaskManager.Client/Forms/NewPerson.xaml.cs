using System.Text;
using System.Windows;
using System.Windows.Controls;
using TaskManager.Client.TaskManagerServices;

namespace TaskManager.Client.Forms
{
	/// <summary>
	/// Interaction logic for NewPerson.xaml
	/// </summary>
	public partial class NewPerson : Window
	{
		#region Variables

		private int _id = -1;
		private Person _person;

		#endregion

		#region Constructors

		/// <summary>
		/// Fills the initial values
		/// </summary>
		/// <param name="id"></param>
		public NewPerson(int id)
		{
			InitializeComponent();
			_id = id;
			_person = CreateNewPersonWithDefaultValues();
			if (_id >= 0)                   //Fills _person if we will be edit record
			{
				using (var client = new ServicesClient())
				{
					_person = client.GetPersonById(_id);
				}
			}
			gridPerson.DataContext = _person;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Button action that accept data changes
		/// </summary>
		/// <param name="sender">object sender</param>
		/// <param name="e">RoutedEventArgs e</param>
		private void BtnPersonSubmitClick(object sender, RoutedEventArgs e)
		{
			if (IsValidPerson())
			{
				using (var client = new ServicesClient())
				{
					if (_id >= 0)
					{
						client.UpdatePerson(_person);
					}
					else
						client.InsertPerson(_person);
				}
				this.Close();
			}
		}

		#region Supporting methods

		/// <summary>
		/// Check data for validation
		/// </summary>
		/// <returns>bool</returns>
		private bool IsValidPerson()
		{
			string message;
			if (!Validator.FormHasError(out message, gridPerson))
			{
				_person = GetPersonFromForm();
				if (_id >= 0)
				{
					int persId;
					int.TryParse(lblPersonId.Content.ToString(), out persId);
					_person.PersonID = persId;
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Create new person with default values
		/// </summary>
		/// <returns>Person</returns>
		private Person CreateNewPersonWithDefaultValues()
		{
			return new Person
			{
				Name = "Name",
				SecondName = "Second Name",
				Soname = "Soname",
				Position = "Position"
			};
		}

		/// <summary>
		/// Read data from form to object
		/// </summary>
		/// <returns></returns>
		private Person GetPersonFromForm()
		{
			return new Person
			{
				Soname = txbPersonSoname.Text.Trim(),
				Name = txbPersonName.Text.Trim(),
				SecondName = txbPersonSecondName.Text.Trim(),
				Position = txbPersonPosition.Text.Trim()
			};
		}

		#endregion

		#endregion
	}

	/// <summary>
	/// Static class for validation all child textBoxes in current container
	/// </summary>
	public static class Validator
	{
		#region Methods

		/// <summary>
		/// Checks for errors
		/// </summary>
		/// <param name="message">out string message</param>
		/// <param name="obj">object obj</param>
		/// <returns>bool</returns>
		public static bool FormHasError(out string message, object obj)
		{
			StringBuilder sb = new StringBuilder();
			GetErrors(sb, (DependencyObject)obj);
			message = sb.ToString();
			return message != "";
		}

		/// <summary>
		/// Add errors in sb for all child texBoxes
		/// </summary>
		/// <param name="sb">StringBuilder sb</param>
		/// <param name="obj">DependencyObject obj</param>
		public static void GetErrors(StringBuilder sb, DependencyObject obj)
		{
			foreach (object child in LogicalTreeHelper.GetChildren(obj))
			{
				TextBox element = child as TextBox;
				if (element == null) continue;
				if (Validation.GetHasError(element))
				{
					sb.Append(element.Text + " has errors:\r\n");
					foreach (var error in Validation.GetErrors(element))
					{
						sb.Append(" " + error.ErrorContent.ToString());
						sb.Append("\r\n");
					}
					GetErrors(sb, element);
				}
			}
		}

		#endregion
	}
}
