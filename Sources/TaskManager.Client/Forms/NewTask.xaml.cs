using System;
using System.Collections.Generic;
using System.Windows;
using TaskManager.Client.TaskManagerServices;

namespace TaskManager.Client.Forms
{
	/// <summary>
	/// Interaction logic for NewTask.xaml
	/// </summary>
	public partial class NewTask : Window
	{
		#region Variables

		private int _id = -1;
		private int _pr = -1;
		private Task _task;
		List<CheckedPerson> checkedPersons = new List<CheckedPerson>();

		#endregion

		#region Methods

		public NewTask(int id, int pr)
		{
			InitializeComponent();
			_id = id;
			_pr = pr;

			using (var client = new ServicesClient())
			{
				foreach (var per in client.GetAllPersons())
					checkedPersons.Add(new CheckedPerson(per));

				lsbPersons.ItemsSource = checkedPersons;                //Fill form controls
				cmbPrShortName.ItemsSource = client.GetAllProjects();
				cmbStatus.ItemsSource = client.GetAllStatus();

				_task = GetTaskWithDefaultData();

				if (_id > 0)
					_task = client.GetTaskById(_id);

				gridTask.DataContext = _task;
				SetBindingValues();
				if (pr > 0) cmbPrShortName.IsEnabled = false;
			}
		}

		/// <summary>
		/// Fills default data for task
		/// </summary>
		/// <returns>Task</returns>
		private Task GetTaskWithDefaultData()
		{
			return new Task
			{
				BeginTime = DateTime.Parse("1999.01.01"),
				EndTime = DateTime.Parse("2000.01.01"),
				Hours = 10,
				TaskName = "Task Name"
			};
		}

		/// <summary>
		/// Set ComboBoxes SelectedIndex right values for current task
		/// </summary>
		private void SetBindingValues()
		{
			if (_id >= 0)
			{
				for (int i = 0; i < cmbPrShortName.Items.Count; i++)
					if (((Project)cmbPrShortName.Items[i]).ProjectID == _task.ProjectId)
						cmbPrShortName.SelectedIndex = i;                                   //Set PrShortName


				for (int i = 0; i < cmbStatus.Items.Count; i++)
					if (((Status)cmbStatus.Items[i]).StatusId == _task.StatusId)
						cmbStatus.SelectedIndex = i;                                        //Set Status

				foreach (Person pr in _task.TaskPersons)
				{                                                                           //Set Persons
					if (checkedPersons.Find(x => x.PersonSource.PersonID == pr.PersonID) != null)
						checkedPersons.Find(x => x.PersonSource.PersonID == pr.PersonID).IsChecked = true;
				}
			}
			else
				if (_pr >= 0)
				for (int i = 0; i < cmbPrShortName.Items.Count; i++)
					if (((Project)cmbPrShortName.Items[i]).ProjectID == _pr)
						cmbPrShortName.SelectedIndex = i;
		}

		/// <summary>
		/// Accept task changes and save or update task
		/// </summary>
		/// <param name="sender">object sender</param>
		/// <param name="e">RoutedEventArgs e</param>
		private void BtnSubmitClick(object sender, RoutedEventArgs e)
		{
			if (IsValid())
			{
				_task = GetTaskFromForm();
				using (var client = new ServicesClient())
				{
					if (_id >= 0)
					{
						int taskId;
						int.TryParse(tblTaskId.Text, out taskId);
						if (taskId > 0)
						{
							_task.TaskId = taskId;
							client.UpdateTask(_task);
						}
					}
					else
						client.InsertTask(_task);
				}
				this.Close();
			}
		}

		/// <summary>
		/// Fills Task from form
		/// </summary>
		/// <returns></returns>
		private Task GetTaskFromForm()
		{
			List<Person> performers = new List<Person>();
			foreach (var person in checkedPersons)
			{
				if (person.IsChecked) performers.Add(person.PersonSource);
			}

			var task = new Task();
			DateTime beginTime = DateTime.Parse("1999.01.01");
			DateTime.TryParse(txbBeginTime.Text, out beginTime);
			task.BeginTime = beginTime;
			DateTime endTime = DateTime.Parse("2000.01.01");
			DateTime.TryParse(txbEndTime.Text, out endTime);
			task.EndTime = endTime;
			int hour;
			int.TryParse(txbHours.Text, out hour);
			task.Hours = hour;

			task.ProjectId = ((Project)cmbPrShortName.SelectedItem).ProjectID;
			task.PrShortName = ((Project)cmbPrShortName.SelectedItem).PrShortName;
			task.Status = ((Status)cmbStatus.SelectedItem).StatusTitle;
			task.StatusId = ((Status)cmbStatus.SelectedItem).StatusId;
			task.TaskName = txbTaskName.Text;
			task.TaskPersons = performers.ToArray();

			return task;
		}

		/// <summary>
		/// Closeform
		/// </summary>
		/// <param name="sender">object sender</param>
		/// <param name="e">RoutedEventArgs e</param>
		private void BtnCancelClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Check form data for valid
		/// </summary>
		/// <returns>bool is data valid</returns>
		private bool IsValid()
		{
			string message;
			if (!Validator.FormHasError(out message, gridTask))
			{
				return true;
			}
			return false;
		}

		#endregion
	}

	/// <summary>
	/// Class for adding new property to Person class: IsChecked for binding to checkboxlist
	/// </summary>
	public class CheckedPerson
	{
		private Person _person;

		public CheckedPerson(Person person)
		{
			_person = person;
			IsChecked = false;
		}

		public Person PersonSource
		{
			get { return _person; }
		}

		public bool IsChecked { get; set; }
	}
}
