using System.Collections.Generic;
using System.Configuration;
using TaskManager.Data;
using TaskManager.Data.Entities;

namespace TaskManager.Services
{
	/// <summary>
	///  Business Layer Object class for working with Tasks entities
	/// </summary>
	public class TaskBlo
	{
		#region Variables

		private readonly string _connectionString;
		private readonly TaskDao _taskDao;

		#endregion

		#region Constructors

		/// <summary>
		/// Create object of TaskBlo class with default connection
		/// </summary>
		public TaskBlo()
			: this(ConfigurationManager.ConnectionStrings["TaskManagerDefaultConnectionString"].ConnectionString) { }

		/// <summary>
		/// Create object of TaskBlo class
		/// </summary>
		/// <param name="connectionString">string connectionString</param>
		public TaskBlo(string connectionString)
		{
			_connectionString = connectionString;
			_taskDao = new TaskDao(_connectionString);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Insert Task
		/// </summary>
		/// <param name="task">Task task</param>
		public void InsertTask(Task task)
		{
			if (task != null) _taskDao.InsertTask(task);
		}

		/// <summary>
		/// Update task
		/// </summary>
		/// <param name="task">Task task</param>
		public void UpdateTask(Task task)
		{
			if (task != null) _taskDao.UpdateTask(task);
		}

		/// <summary>
		/// Delete Task with specific task id
		/// </summary>
		/// <param name="taskId">int taskId</param>
		public void DeleteTask(int taskId)
		{
			_taskDao.DeleteTask(taskId);
		}

		/// <summary>
		/// Returns all Tasks
		/// </summary>
		/// <returns>List</returns>
		public List<Task> GetAllTasks()
		{
			return _taskDao.GetAllTasks();
		}

		/// <summary>
		/// Return Task with specific taskId
		/// </summary>
		/// <param name="taskId">int taskId</param>
		/// <returns>Task</returns>
		public Task GetTaskById(int taskId)
		{
			return _taskDao.GetTaskById(taskId);
		}

		/// <summary>
		/// Returns all Status
		/// </summary>
		/// <returns>IList</returns>
		public IList<Status> GetAllStatus()
		{
			return _taskDao.GetAllStatus();
		}

		#endregion
	}
}
