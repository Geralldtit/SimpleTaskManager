using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TaskManager.Data.Entities;

namespace TaskManager.Data
{
	/// <summary>
	///  Data Access Object for working with Project Table
	/// </summary>
	public class ProjectDao : Dao
	{
		#region Variables

		private readonly string _connectionString;

		#endregion

		#region Methods

		/// <summary>
		/// Contructor for ProjectDao 
		/// </summary>
		/// <param name="connectionString">string connectionString</param>
		public ProjectDao(string connectionString)
		{
			_connectionString = connectionString;
		}

		/// <summary>
		///  Insert project into DataBase and all Tasks belonging to the project
		/// </summary>
		/// <param name="project">Project project: project to insert</param>
		/// <returns>projectId</returns>
		public int InsertProject(Project project)
		{
			TaskDao taskDao = new TaskDao(_connectionString);
			if (project.PrTasks != null)                                        //Insert all tasks
				foreach (Task ts in project.PrTasks)
					taskDao.InsertTask(ts);

			//Insert project
			const string sqlInsertProjectString = "Insert Into Projects(ProjectName,PrShortName,PrDescription) " +
												  "Values(@ProjectName, @PrShortName, @Description)";

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				var parameters = new Dictionary<object, object>
									 {
										 {"@ProjectName", project.PrName},
										 {"@PrShortName", project.PrShortName},
										 {"@Description", project.Description}
									 };
				ExecuteCommand(sqlInsertProjectString, parameters, con);

				var cmd = new SqlCommand("select @@IDENTITY", con);             //Get projectId
				project.ProjectID = Convert.ToInt32(cmd.ExecuteScalar());
			}

			return project.ProjectID;
		}

		/// <summary>
		/// Return all projects in DataBase
		/// </summary>
		/// <returns>all projects</returns>
		public IList<Project> GetAllProjects()
		{
			List<Project> allProjects = new List<Project>();
			List<Project> allPrjectsWithTasks = new List<Project>();

			string sqlSelectProjects = "SELECT * FROM Projects";
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand(sqlSelectProjects, con);
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader != null)                                         //Get all projects
				{
					while (reader.Read())
					{
						allProjects.Add(GetProjectFromReader(reader));
					}
					reader.Close();
				}
				foreach (Project pr in allProjects)                         //Fills all projects with tasks
				{
					allPrjectsWithTasks.Add(GetTasksByProjectId(pr, con));
				}
			}

			return allPrjectsWithTasks;
		}

		/// <summary>
		/// Fill project from DataReader
		/// </summary>
		/// <param name="reader">SqlDataReader reader</param>
		/// <returns>Project</returns>
		private Project GetProjectFromReader(SqlDataReader reader)
		{
			var project = new Project
			{
				ProjectID = reader.GetInt32(0),
				PrName = reader.GetString(1),
				PrShortName = reader.GetString(2)
			};
			if (reader["PrDescription"] != DBNull.Value)
				project.Description = reader.GetString(3);

			return project;
		}

		/// <summary>
		/// Get record using project id
		/// </summary>
		/// <param name="projectId">int projectId</param>
		/// <returns>found record or null</returns>
		public Project GetProjectByProjectId(int projectId)
		{
			const string sqlSelectProjects = "SELECT ProjectID, ProjectName, PrShortName, PrDescription " +
											 " FROM Projects " +
											 " WHERE ProjectID=@Id";

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();

				var readedProject = GetProjectFromObjects(GetById(sqlSelectProjects, projectId, con));

				return GetTasksByProjectId(readedProject, con);
			}
		}

		/// <summary>
		/// Update project with all his tasks
		/// </summary>
		/// <param name="project">Project project</param>
		public void UpdateProject(Project project)
		{
			TaskDao taskDao = new TaskDao(_connectionString);
			foreach (Task ts in project.PrTasks)                    //Update all project tasks
			{
				taskDao.UpdateTask(ts);
			}
			
			//Update project
			string sqlString = "UPDATE Projects " +
							   "SET ProjectName=@PrName, PrShortName=@PrShortName, PrDescription= @Description " +
							   "WHERE ProjectID=@ProjectID";

			var parameters = new Dictionary<object, object>
								 {
									 {"@PrName", project.PrName},
									 {"@PrShortName", project.PrShortName},
									 {"@Description", project.Description},
									 {"@ProjectID", project.ProjectID}
								 };

			ExecuteCommand(sqlString, parameters, _connectionString);
		}

		/// <summary>
		/// Delete project and all task beloing to the project
		/// </summary>
		/// <param name="projectId">int projectId</param>
		public void DeleteProject(int projectId)
		{
			string sqlString = "SELECT TaskID FROM Tasks WHERE ProjectID=@projectId";
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				List<int> taskIdlist = new List<int>();

				SqlCommand cmd = new SqlCommand(sqlString, con);
				cmd.Parameters.AddWithValue("@projectId", projectId);

				SqlDataReader reader = cmd.ExecuteReader();
				if (reader != null)
				{
					while (reader.Read())
					{
						taskIdlist.Add(reader.GetInt32(0));
					}
					reader.Close();
				}


				sqlString = "DELETE FROM TasksPersons WHERE TaskID=@i";             //Delete all persons belongs to task
				foreach (int i in taskIdlist)
				{
					var parameters = new Dictionary<object, object> { { "@i", i } };
					ExecuteCommand(sqlString, parameters, con);
				}

				sqlString = "DELETE FROM Tasks WHERE ProjectID=@Id";                //Delete all project tasks
				var param = new Dictionary<object, object> { { "@Id", projectId } };
				ExecuteCommand(sqlString, param, con);

				sqlString = "DELETE FROM Projects WHERE ProjectID=@Id";             //Delete project
				ExecuteCommand(sqlString, param, con);
			}
		}

		#region Supporting methods

		/// <summary>
		/// Get tasks for current project
		/// </summary>
		/// <param name="project">Project project</param>
		/// <param name="con">SqlConnection con</param>
		/// <returns></returns>
		private Project GetTasksByProjectId(Project project, SqlConnection con)
		{
			const string sqlSelectPersons = "SELECT TaskID FROM Tasks WHERE Tasks.ProjectID=@ProjectID";

			SqlCommand cmd = new SqlCommand(sqlSelectPersons, con);         //Fills a task of persons
			cmd.Parameters.AddWithValue("@ProjectID", project.ProjectID);
			SqlDataReader secReader = cmd.ExecuteReader();

			TaskDao taskDao = new TaskDao(_connectionString);
			if (secReader != null)
			{
				while (secReader.Read())
				{
					Task addTask = taskDao.GetTaskById(secReader.GetInt32(0));
					project.PrTasks.Add(addTask);
				}
				secReader.Close();
			}

			return project;
		}

		/// <summary>
		/// Convert array of objects into Project
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
		private Project GetProjectFromObjects(object[] values)
		{
			var project = new Project
			{
				ProjectID = Convert.ToInt32(values[0]),
				PrName = Convert.ToString(values[1]),
				PrShortName = Convert.ToString(values[2])
			};

			if (values[3] != DBNull.Value)
				project.Description = Convert.ToString(values[3]);

			return project;
		}

		#endregion Supporting methods

		#endregion Methods
	}
}
