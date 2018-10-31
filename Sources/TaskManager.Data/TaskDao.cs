using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TaskManager.Data.Entities;

namespace TaskManager.Data
{
	/// <summary>
	/// Data Access Object for working with Task Table
	/// </summary>
	public class TaskDao : Dao
	{
		#region Variables

		private readonly string _connectionString;

		#endregion

		#region Methods

		/// <summary>
		/// Constructor for TaskDao
		/// </summary>
		/// <param name="connectionString">String for connecting to DataBase</param>
		public TaskDao(string connectionString)
		{
			_connectionString = connectionString;
		}

		/// <summary> 
		/// Insert new recrod in Tasks table and new records in TasksPerson table if it need
		/// </summary>
		/// <param name="task">Task task</param>
		/// <returns>int taskId</returns>
		public int InsertTask(Task task)
		{
			string sqlInsertTask = "Insert into Tasks(TaskName,BeginTime,EndTime,StatusID,ProjectID,Hours) " +
								   "Values (@TaskName, @BeginTime, @EndTime, @StatusId, @ProjectId, @Hours)";

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				var parameters = new Dictionary<object, object>
									 {
										 {"@TaskName", task.TaskName},
										 {"@BeginTime", task.BeginTime},
										 {"@EndTime", task.EndTime},
										 {"@StatusId", task.StatusId},
										 {"@ProjectId", task.ProjectId},
										 {"@Hours", task.Hours}
									 };
				ExecuteCommand(sqlInsertTask, parameters, con);

				var cmd = new SqlCommand("select @@IDENTITY", con);             //Get autoincrement value
				task.TaskId = Convert.ToInt32(cmd.ExecuteScalar());


				sqlInsertTask = "Insert into TasksPersons(TaskID, PersonID)" +
								"Values (@TaskID, @PersonID)";                  //Add persons for current task
				foreach (Person pr in task.TaskPersons)
				{
					parameters = new Dictionary<object, object>
									 {
										 {"@TaskID", task.TaskId},
										 {"@PersonID", pr.PersonID}
									 };
					ExecuteCommand(sqlInsertTask, parameters, con);
				}
			}
			return task.TaskId;
		}

		/// <summary>
		/// Update task with all task_person relations
		/// </summary>
		/// <param name="task">task for update </param>
		public void UpdateTask(Task task)
		{
			Task oldTask = GetTaskById(task.TaskId);

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();

				string sqlString = " DELETE FROM TasksPersons " +
									"WHERE TaskID=@TaskID AND PersonID=@PersonID";          //Delete old TasksPerson records

				foreach (Person pr in oldTask.TaskPersons)
				{
					var parameters = new Dictionary<object, object>
										 {
											 {"@TaskID", task.TaskId},
											 {"@PersonID", pr.PersonID}
										 };
					ExecuteCommand(sqlString, parameters, con);
				}

				sqlString = "Update Tasks " +
							"Set TaskName=@TaskName, BeginTime=@BeginTime, EndTime=@EndTime, " +
							"StatusID=@StatusID, ProjectID=@ProjectID, Hours=@Hours " +
							"Where TaskID=@TaskID";                                          //Update task

				var param = new Dictionary<object, object>
									 {
										 {"@TaskName", task.TaskName},
										 {"@BeginTime", task.BeginTime},
										 {"@EndTime", task.EndTime},
										 {"@StatusID", task.StatusId},
										 {"@ProjectID", task.ProjectId},
										 {"@Hours", task.Hours},
										 {"@TaskID", task.TaskId}
									 };
				ExecuteCommand(sqlString, param, con);

				foreach (Person pr in task.TaskPersons)                                     //Insert new TasksPersons
				{
					sqlString = "Insert into TasksPersons(TaskID,PersonID) Values(@TaskID, @PersonID)";
					param = new Dictionary<object, object>
								{
									{"@TaskID", task.TaskId},
									{"@PersonID", pr.PersonID}
								};
					ExecuteCommand(sqlString, param, con);
				}
			}
		}

		/// <summary>
		/// Delete task with task_person relations
		/// </summary>
		/// <param name="taskId">unique task id</param>
		public void DeleteTask(int taskId)
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();

				string sqlString = "DELETE FROM TasksPersons WHERE TaskID=@TaskID";     //Delete task_person relation
				SqlCommand cmd = new SqlCommand(sqlString, con);
				cmd.Parameters.AddWithValue("@TaskID", taskId);
				cmd.ExecuteNonQuery();

				sqlString = "DELETE FROM Tasks WHERE TaskID=@TaskID";                   //Delete task
				cmd = new SqlCommand(sqlString, con);
				cmd.Parameters.AddWithValue("@TaskID", taskId);
				cmd.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Get all tasks from DataBase
		/// </summary>
		/// <returns> all tasks </returns>
		public List<Task> GetAllTasks()
		{
			List<Task> allTasks = new List<Task>();
			List<Task> allTasksWithPersons = new List<Task>();

			string sqlSelectPersons = "Select TaskID, TaskName, BeginTime, EndTime, " +
									 "StatusTitle, PrShortName, Hours, Tasks.ProjectID, Tasks.StatusID " +
									 "from Tasks, Status, Projects " +
									 "where Tasks.ProjectID=Projects.ProjectID AND Tasks.StatusID=Status.StatusID";

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand(sqlSelectPersons, con);

				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader != null)
				{
					while (reader.Read())
					{
						allTasks.Add(GetTaskFromReader(reader));
					}
					reader.Close();
				}

				foreach (Task ts in allTasks)                                   //Fills a tasks of persons
				{
					allTasksWithPersons.Add(GetPersonsByTaskId(ts, con));
				}
			}

			return allTasksWithPersons;
		}

		/// <summary>
		/// Get record from Task table using task id
		/// </summary>
		/// <param name="taskId">int taskId</param>
		/// <returns>Found task or null</returns>
		public Task GetTaskById(int taskId)
		{
			Task readedTask;
			string sqlSelectTasks = "Select TaskID, TaskName, BeginTime, EndTime, " +
									 "StatusTitle, PrShortName, Tasks.ProjectID, Tasks.StatusID, Hours " +
									 "from Tasks, Status, Projects " +
									 "where Tasks.ProjectID=Projects.ProjectID AND " +
									 "Tasks.StatusID=Status.StatusID AND TaskID=@Id";

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				readedTask = GetTaskFromObjects(GetById(sqlSelectTasks, taskId, con));
				readedTask = GetPersonsByTaskId(readedTask, con);
			}

			return readedTask;
		}

		/// <summary>
		/// Get all possible status
		/// </summary>
		/// <returns>All status</returns>
		public IList<Status> GetAllStatus()
		{
			string sqlString = "Select * from Status";

			List<Status> allStatus = new List<Status>();

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand(sqlString, con);
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader != null)
				{
					while (reader.Read())
					{
						var readedStatus = new Status()
						{
							StatusId = reader.GetInt32(0),
							StatusTitle = reader.GetString(1)
						};
						allStatus.Add(readedStatus);
					}
					reader.Close();
				}
			}

			return allStatus;
		}

		#region Supporting methods

		/// <summary>
		/// Fills Task from array of objects
		/// </summary>
		/// <param name="values">object[] values</param>
		/// <returns>Task</returns>
		private Task GetTaskFromObjects(object[] values)
		{
			var readedTask = new Task
			{
				TaskId = Convert.ToInt32(values[0]),
				TaskName = Convert.ToString(values[1]),
				BeginTime = Convert.ToDateTime(values[2]),
				EndTime = Convert.ToDateTime(values[3]),
				Status = Convert.ToString(values[4]),
				PrShortName = Convert.ToString(values[5]),
				ProjectId = Convert.ToInt32(values[6]),
				StatusId = Convert.ToInt32(values[7])
			};
			if (values[8] != DBNull.Value) readedTask.Hours = Convert.ToInt32(values[8]);
			return readedTask;
		}

		/// <summary>
		/// Fills a  task of persons
		/// </summary>
		/// <param name="task">Task task: task to fill</param>
		/// <param name="con">SqlConnection con: open connection</param>
		/// <returns>Task with her persons</returns>
		private Task GetPersonsByTaskId(Task task, SqlConnection con)
		{
			string sqlSelectPersons = "SELECT TasksPersons.PersonID FROM TasksPersons WHERE TaskID=@TaskID";

			SqlCommand cmd = new SqlCommand(sqlSelectPersons, con);         //Fills a tasks of persons
			cmd.Parameters.AddWithValue("@TaskID", task.TaskId);
			SqlDataReader secReader = cmd.ExecuteReader();
			PersonDao personDao = new PersonDao(_connectionString);
			if (secReader != null)
			{
				while (secReader.Read())
				{
					Person addPerson = personDao.GetPersonById(secReader.GetInt32(0));
					task.TaskPersons.Add(addPerson);
				}
				secReader.Close();
			}

			return task;
		}

		/// <summary>
		/// Get Task from reader
		/// </summary>
		/// <param name="reader">SqlDataReader reader</param>
		/// <returns>Task</returns>
		private Task GetTaskFromReader(SqlDataReader reader)
		{
			Task readedTask = new Task
			{
				TaskId = reader.GetInt32(0),
				TaskName = reader.GetString(1),
				BeginTime = reader.GetDateTime(2),
				EndTime = reader.GetDateTime(3),
				Status = reader.GetString(4),
				PrShortName = reader.GetString(5),
				ProjectId = reader.GetInt32(7),
				StatusId = reader.GetInt32(8)
			};
			if (reader["Hours"] != DBNull.Value)
				readedTask.Hours = reader.GetInt32(6);

			return readedTask;
		}

		#endregion Supporting methods

		#endregion Methods
	}
}
