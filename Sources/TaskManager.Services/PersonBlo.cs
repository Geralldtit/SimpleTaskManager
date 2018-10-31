using System.Collections.Generic;
using System.Configuration;
using TaskManager.Data;
using TaskManager.Data.Entities;

namespace TaskManager.Services
{
	/// <summary>
	/// Business Layer Object class for working with Persons entities
	/// </summary>
	public class PersonBlo
	{
		#region Variables

		private readonly string _connectionString;
		private readonly PersonDao _personDao;

		#endregion

		#region Constructors

		/// <summary>
		/// Create new PersonBlo object with default connection
		/// </summary>
		public PersonBlo()
			: this(ConfigurationManager.ConnectionStrings["TaskManagerDefaultConnectionString"].ConnectionString) { }

		/// <summary>
		/// Create new PersonBlo object
		/// </summary>
		/// <param name="connectionString">string connectionString</param>
		public PersonBlo(string connectionString)
		{
			_connectionString = connectionString;
			_personDao = new PersonDao(_connectionString);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Insert Person object
		/// </summary>
		/// <param name="person"></param>
		public void InsertPerson(Person person)
		{
			_personDao.InsertPerson(person);
		}

		/// <summary>
		/// Return all Persons
		/// </summary>
		/// <returns> List</returns>
		public List<Person> GetAllPersons()
		{
			return _personDao.GetAllPersons();
		}

		/// <summary>
		/// Return Person with current person id
		/// </summary>
		/// <param name="personId">int personId</param>
		/// <returns>Found Person</returns>
		public Person GetPersonById(int personId)
		{
			return _personDao.GetPersonById(personId);
		}

		/// <summary>
		/// Update person
		/// </summary>
		/// <param name="person">Person person</param>
		public void UpdatePerson(Person person)
		{
			_personDao.UpdatePerson(person);
		}

		/// <summary>
		/// Delete Person with specific id
		/// </summary>
		/// <param name="personId">int personId</param>
		public void DeletePerson(int personId)
		{
			_personDao.DeletePersonById(personId);
		}

		#endregion
	}

}
