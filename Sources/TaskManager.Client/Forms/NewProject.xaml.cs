using System.Windows;
using System.Windows.Controls;
using TaskManager.Client.TaskManagerServices;

namespace TaskManager.Client.Forms
{
	/// <summary>
	/// Interaction logic for NewProject.xaml
	/// </summary>
	public partial class NewProject : Window
	{
		#region Variables

		private const int CreateNew = -1;
		private int _pro = -1;
		private Project _project;

		#endregion

		#region Methods

		public NewProject(int pro)
		{
			InitializeComponent();
			_pro = pro;
			UpdateProject();
		}

		/// <summary>
		/// Open Modal form for edit current task
		/// </summary>
		/// <param name="sender">object sender</param>
		/// <param name="e">RoutedEventArgs e</param>
		private void EditClick(object sender, RoutedEventArgs e)
		{
			Button cmd = (Button)sender;
			int id = (int)cmd.Tag;
			if (_pro >= 0)
			{
				var editTaskForm = new Forms.NewTask(id, _pro);
				editTaskForm.ShowDialog();
			}
			else
				_pro = CreateProject();
			UpdateProject();
		}

		/// <summary>
		/// Delete current task
		/// </summary>
		/// <param name="sender">object sender</param>
		/// <param name="e">RoutedEventArgs e</param>
		private void DeleteClick(object sender, RoutedEventArgs e)
		{
			Button cmd = (Button)sender;
			int id = (int)cmd.Tag;
			using (var client = new ServicesClient())
			{
				client.DeleteTask(id);
				UpdateProject();
			}
		}

		/// <summary>
		/// Save project and add new task if data from form is valid
		/// </summary>
		/// <param name="sender">object sender</param>
		/// <param name="e">RoutedEventArgs e</param>
		private void BtnAddTaskClick(object sender, RoutedEventArgs e)
		{
			if (IsValid())
			{
				if (_pro < 0)
					_pro = CreateProject();
				else
					using (ServicesClient client = new ServicesClient())
					{
						client.UpdateProject(_project);
					}

				var editTaskForm = new Forms.NewTask(CreateNew, _pro);
				editTaskForm.ShowDialog();
				UpdateProject();
			}
		}

		/// <summary>
		/// Accept project changes
		/// </summary>
		/// <param name="sender">object sender</param>
		/// <param name="e">RoutedEventArgs e</param>
		private void BtnSubmitProjectClick(object sender, RoutedEventArgs e)
		{
			if (IsValid())
			{
				if (_pro < 0)
					_pro = CreateProject();
				else
					using (ServicesClient client = new ServicesClient())
					{
						client.UpdateProject(_project);
					}
				UpdateProject();
				this.Close();
			}
		}

		#region Supporting methods

		/// <summary>
		/// Check form data for validation
		/// </summary>
		/// <returns>bool</returns>
		private bool IsValid()
		{
			string message;
			return !Validator.FormHasError(out message, gridProject);
		}

		/// <summary>
		/// Update project data from DB
		/// </summary>
		private void UpdateProject()
		{

			if (_pro >= 0)
			{
				using (ServicesClient client = new ServicesClient())
				{
					_project = client.GetProjectByProjectId(_pro);
				}
			}
			else
			{
				_project = GetProjectWithDefaultData();
			}
			gridProject.DataContext = _project;
			TasklistView.ItemsSource = _project.PrTasks;
		}

		/// <summary>
		/// Fills project with default data
		/// </summary>
		/// <returns>Project</returns>
		private Project GetProjectWithDefaultData()
		{
			return new Project
			{
				PrName = "Project Name",
				PrShortName = "Short Name",
				Description = "Project Description"
			};
		}

		/// <summary>
		/// Create and save project if data is valid
		/// </summary>
		/// <returns>int projectId</returns>
		private int CreateProject()
		{
			if (IsValid())
			{
				_project = GetProjectFromForm();
				using (var client = new ServicesClient())
				{
					return client.InsertProject(_project);
				}
			}
			return -1;
		}

		/// <summary>
		/// Get data from form to the Project
		/// </summary>
		/// <returns>Project</returns>
		private Project GetProjectFromForm()
		{
			return new Project
			{
				PrName = txbPrName.Text,
				PrShortName = txbPrShortName.Text,
				Description = txbPrDescription.Text
			};
		}

		#endregion

		#endregion
	}
}
