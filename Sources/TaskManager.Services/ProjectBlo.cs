using System.Collections.Generic;
using System.Configuration;
using TaskManager.Data;
using TaskManager.Data.Entities;

namespace TaskManager.Services
{
	/// <summary>
	///  Business Layer Object class for working with Projects entities
	/// </summary>
	public class ProjectBlo
	{
		#region Variables

		private readonly string _connectionString;
		private readonly ProjectDao _projectDao;

		#endregion

		#region Constructors

		/// <summary>
		/// Create object of ProjectBlo class with default connection
		/// </summary>
		public ProjectBlo()
			: this(ConfigurationManager.ConnectionStrings["TaskManagerDefaultConnectionString"].ConnectionString) { }

		/// <summary>
		/// Create object of ProjectBlo class 
		/// </summary>
		/// <param name="connectionString">string connectionString</param>
		public ProjectBlo(string connectionString)
		{
			_connectionString = connectionString;
			_projectDao = new ProjectDao(_connectionString);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Insert project
		/// </summary>
		/// <param name="project">Project project</param>
		/// <returns>int project Id</returns>
		public int InsertProject(Project project)
		{
			return _projectDao.InsertProject(project);
		}

		/// <summary>
		/// Returns all projects
		/// </summary>
		/// <returns>List</returns>
		public IList<Project> GetAllProjects()
		{
			return _projectDao.GetAllProjects();
		}

		/// <summary>
		/// Return Project with specific project id
		/// </summary>
		/// <param name="projectId">int projectId</param>
		/// <returns>Project</returns>
		public Project GetProjectByProjectId(int projectId)
		{
			return _projectDao.GetProjectByProjectId(projectId);
		}

		/// <summary>
		/// Update project
		/// </summary>
		/// <param name="project">Project project</param>
		public void UpdateProject(Project project)
		{
			if (project != null) _projectDao.UpdateProject(project);
		}

		/// <summary>
		/// Delete project with specific project id
		/// </summary>
		/// <param name="projectId">int projectId</param>
		public void DeleteProject(int projectId)
		{
			_projectDao.DeleteProject(projectId);
		}

		#endregion
	}
}
