using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TaskManager.Client.TaskManagerServices;

namespace TaskManager.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class ProjectsForm : Window
	{
		#region Constants

		private const int CreateNew = -1;

		#endregion
		#region Variables

		ObservableCollection<Project> projectsCollection = new ObservableCollection<Project>();
		ObservableCollection<Person> personsCollection = new ObservableCollection<Person>();
		ObservableCollection<Task> tasksCollection = new ObservableCollection<Task>();

		#endregion

		#region Methods

		public ProjectsForm()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateCollections();
		}

		/// <summary>
		/// Update collections with all our data
		/// </summary>
		private void UpdateCollections()
		{
			using (var client = new ServicesClient())
			{
				//fill our collections
				personsCollection.Clear();
				foreach (Person pr in client.GetAllPersons())
				{
					personsCollection.Add(pr);
				}
				PersonlistView.DataContext = personsCollection;

				projectsCollection.Clear();
				foreach (Project pr in client.GetAllProjects())
				{
					projectsCollection.Add(pr);
				}
				ProjectlistView.DataContext = projectsCollection;

				tasksCollection.Clear();
				foreach (Task ts in client.GetAllTasks())
				{
					tasksCollection.Add(ts);
				}
				TasklistView.DataContext = tasksCollection;
			}
		}

		/// <summary>
		/// Create new Person or Task or Project
		/// </summary>
		/// <param name="sender">object sender</param>
		/// <param name="e">RoutedEventArgs e</param>
		private void BtnCreateNewClick(object sender, RoutedEventArgs e)
		{
			switch (tabAppControl.SelectedIndex)
			{
				case 0:
					var createProjectForm = new Forms.NewProject(CreateNew);
					createProjectForm.ShowDialog();
					UpdateCollections();
					break;
				case 1:
					var createTaskForm = new Forms.NewTask(CreateNew, CreateNew);
					createTaskForm.ShowDialog();
					UpdateCollections();
					break;
				case 2:
					var createPersonForm = new Forms.NewPerson(CreateNew);
					createPersonForm.ShowDialog();
					UpdateCollections();
					break;
			}
		}

		/// <summary>
		/// Open Modal form for edit entities
		/// </summary>
		/// <param name="sender">object sender</param>
		/// <param name="e">RoutedEventArgs e</param>
		private void EditClick(object sender, RoutedEventArgs e)
		{
			Button cmd = (Button)sender;
			int id = (int)cmd.Tag;
			switch (tabAppControl.SelectedIndex)
			{
				case 0:
					var editProjectForm = new Forms.NewProject(id);
					editProjectForm.ShowDialog();
					break;
				case 1:
					var editTaskForm = new Forms.NewTask(id, CreateNew);
					editTaskForm.ShowDialog();
					UpdateCollections();
					break;
				case 2:
					var editPersonForm = new Forms.NewPerson(id);
					editPersonForm.ShowDialog();
					UpdateCollections();
					break;
			}
		}

		/// <summary>
		/// Delete current record
		/// </summary>
		/// <param name="sender">object sender</param>
		/// <param name="e">RoutedEventArgs e</param>
		private void DeleteClick(object sender, RoutedEventArgs e)
		{
			Button cmd = (Button)sender;
			int id = (int)cmd.Tag;
			using (var client = new ServicesClient())
			{
				switch (tabAppControl.SelectedIndex)
				{
					case 0:
						client.DeleteProject(id);
						break;
					case 1:
						client.DeleteTask(id);
						break;
					case 2:
						client.DeletePerson(id);
						break;
				}
				UpdateCollections();
			}
		}

		#endregion

		private void BtnInfoClick(object sender, RoutedEventArgs e)
		{
			var infoForm = new Forms.Info();
			infoForm.ShowDialog();
		}
	}
}
