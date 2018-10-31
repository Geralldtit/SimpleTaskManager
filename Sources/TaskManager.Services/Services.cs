using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TaskManager.Data.Entities;

namespace TaskManager.Services
{
	public class Services : IServices
	{
		#region Variables

		private TaskBlo _task;
		private ProjectBlo _project;
		private PersonBlo _person;
		private ExceptionHandler _exceptionHandler;

		#endregion

		#region Constructors

		public Services()
		{
			_task = new TaskBlo();
			_project = new ProjectBlo();
			_person = new PersonBlo();
			_exceptionHandler = new ExceptionHandler();
			RegisterHandlers();
		}

		#endregion

		#region Methods

		#region Task

		public void InsertTask(Task task)
		{
			_exceptionHandler.Process(() => _task.InsertTask(task));
		}

		public void UpdateTask(Task task)
		{
			_exceptionHandler.Process(() => _task.UpdateTask(task));
		}

		public void DeleteTask(int taskId)
		{
			_exceptionHandler.Process(() => _task.DeleteTask(taskId));
		}

		public List<Task> GetAllTasks()
		{
			return _exceptionHandler.Process(() => _task.GetAllTasks());
		}

		public Task GetTaskById(int taskId)
		{
			return _exceptionHandler.Process(() => _task.GetTaskById(taskId));
		}

		public IList<Status> GetAllStatus()
		{
			return _exceptionHandler.Process(() => _task.GetAllStatus());
		}

		#endregion Task

		#region Project

		public int InsertProject(Project project)
		{
			return _exceptionHandler.Process(() => _project.InsertProject(project));
		}

		public IList<Project> GetAllProjects()
		{
			return _exceptionHandler.Process(() => _project.GetAllProjects());
		}

		public Project GetProjectByProjectId(int projectId)
		{
			return _exceptionHandler.Process(() => _project.GetProjectByProjectId(projectId));
		}

		public void UpdateProject(Project project)
		{
			_exceptionHandler.Process(() => _project.UpdateProject(project));
		}

		public void DeleteProject(int projectId)
		{
			_exceptionHandler.Process(() => _project.DeleteProject(projectId));
		}

		#endregion Project

		#region Person

		public void InsertPerson(Person person)
		{
			_exceptionHandler.Process(() => _person.InsertPerson(person));
		}

		public List<Person> GetAllPersons()
		{
			if (_person == null) _person = new PersonBlo();
			return _exceptionHandler.Process(() => _person.GetAllPersons());
		}

		public Person GetPersonById(int personId)
		{
			return _exceptionHandler.Process(() => _person.GetPersonById(personId));
		}

		public void UpdatePerson(Person person)
		{
			_exceptionHandler.Process(() => _person.UpdatePerson(person));
		}

		public void DeletePerson(int personId)
		{
			_exceptionHandler.Process(() => _person.DeletePerson(personId));
		}

		#endregion Person

		#endregion

		#region Supporting methods

		private void RegisterHandlers()
		{
			_exceptionHandler.RegisterHandler<SqlException>(e => Loger.WriteLog("Services: Server is unavailable: " + e.ToString()));
			_exceptionHandler.RegisterHandler<FormatException>(
				e => Loger.WriteLog("Services: Input string was not in a correct format:" + e.ToString()));
			_exceptionHandler.RegisterDefaultHandler(
				e => Loger.WriteLog("Services: Unhandled exceprion:" + e.ToString()));
		}

		#endregion Supporting methods
	}
}
