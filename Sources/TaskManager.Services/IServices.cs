using System.Collections.Generic;
using System.ServiceModel;
using TaskManager.Data.Entities;

namespace TaskManager.Services
{
	[ServiceContract]
	public interface IServices
	{
		#region Task

		[OperationContract]
		void InsertTask(Task task);

		[OperationContract]
		void UpdateTask(Task task);

		[OperationContract]
		void DeleteTask(int taskId);

		[OperationContract]
		List<Task> GetAllTasks();

		[OperationContract]
		Task GetTaskById(int taskId);

		[OperationContract]
		IList<Status> GetAllStatus();

		#endregion Task

		#region Project

		[OperationContract]
		int InsertProject(Project project);

		[OperationContract]
		IList<Project> GetAllProjects();

		[OperationContract]
		Project GetProjectByProjectId(int projectId);

		[OperationContract]
		void UpdateProject(Project project);

		[OperationContract]
		void DeleteProject(int projectId);

		#endregion Project

		#region Person

		[OperationContract]
		void InsertPerson(Person person);

		[OperationContract]
		List<Person> GetAllPersons();

		[OperationContract]
		Person GetPersonById(int personId);

		[OperationContract]
		void UpdatePerson(Person person);

		[OperationContract]
		void DeletePerson(int personId);

		#endregion Person
	}

}
