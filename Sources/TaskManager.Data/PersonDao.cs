using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TaskManager.Data.Entities;

namespace TaskManager.Data
{
	/// <summary>
	/// Data Access Object for working with Person Table
	/// </summary>
	public class PersonDao : Dao
	{
		#region Variables

		private readonly string _connectionString;

		#endregion

		#region Methods

		/// <summary>
		/// Constructor for class PersonDao
		/// </summary>
		/// <param name="connectionString">connection string</param>
		public PersonDao(string connectionString)
		{
			_connectionString = connectionString;
		}

		/// <summary>
		/// Insert Person record into table 
		/// </summary>
		/// <param name="person">Person person</param>
		public void InsertPerson(Person person)
		{
			const string sqlInsert = "Insert into Persons(Soname, Name, SecondName, Position) " +
									 "Values (@Soname, @Name, @SecondName, @Position)";

			var parameters = new Dictionary<object, object>
								 {
									 {"@Soname", person.Soname},
									 {"@Name", person.Name},
									 {"@SecondName", person.SecondName},
									 {"@Position", person.Position}
								 };

			ExecuteCommand(sqlInsert, parameters, _connectionString);
		}

		/// <summary>
		/// function for gets all persons in DataBasw
		/// </summary>
		/// <returns>all persons</returns>
		public List<Person> GetAllPersons()
		{
			List<Person> allPersons = new List<Person>();
			const string sqlSelectPersons = "Select * from Persons";

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				SqlCommand cmd = new SqlCommand(sqlSelectPersons, con);
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader != null)
				{
					while (reader.Read())
					{
						var readedPerson = PersonFromReader(reader);
						allPersons.Add(readedPerson);
					}
					reader.Close();
				}
			}

			return allPersons;
		}

		/// <summary>
		/// Looking person by person id
		/// </summary>
		/// <param name="personId">unique id</param>
		/// <returns>person</returns>
		public Person GetPersonById(int personId)
		{
			const string sqlFoundPerson = "Select * from Persons where Persons.PersonID=@Id";

			var foundPerson = GetPersonFromObjects(GetById(sqlFoundPerson, personId, _connectionString));
			return foundPerson;
		}

		/// <summary>
		/// Update record
		/// </summary>
		/// <param name="updPerson">Person for update</param>
		public void UpdatePerson(Person updPerson)
		{
			if (updPerson == null) return;

			const string sqlUpdate = "UPDATE Persons " +
									 "SET Soname = @Soname, Name = @Name, SecondName = @SecondName, Position = @Position " +
									 "Where PersonID = @PersonID";

			var parameters = new Dictionary<object, object>
								 {
									 {"@Soname", updPerson.Soname},
									 {"@Name", updPerson.Name},
									 {"@SecondName", updPerson.SecondName},
									 {"@Position", updPerson.Position},
									 {"@PersonId", updPerson.PersonID}
								 };

			ExecuteCommand(sqlUpdate, parameters, _connectionString);
		}

		/// <summary>
		/// Delete record using id
		/// </summary>
		/// <param name="personId">unique person id</param>
		public void DeletePersonById(int personId)
		{
			string sqlFoundPerson = "DELETE FROM TasksPersons	WHERE PersonID = @Id";

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				var parameters = new Dictionary<object, object>
									 {
										 {"@Id", personId}
									 };
				ExecuteCommand(sqlFoundPerson, parameters, con);

				sqlFoundPerson = "DELETE FROM Persons WHERE PersonID = @Id";
				ExecuteCommand(sqlFoundPerson, parameters, con);
			}
		}

		#region Supporting methods

		/// <summary>
		/// Fills Person from DataReader
		/// </summary>
		/// <param name="reader">SqlDataReader reader</param>
		/// <returns>Person</returns>
		private Person PersonFromReader(SqlDataReader reader)
		{
			return new Person
			{
				PersonID = reader.GetInt32(0),
				Soname = reader.GetString(1),
				Name = reader.GetString(2),
				SecondName = reader.GetString(3),
				Position = reader.GetString(4)
			};
		}

		/// <summary>
		/// Get Person values from array of objects
		/// </summary>
		/// <param name="values"></param>
		/// <returns>Person</returns>
		private Person GetPersonFromObjects(object[] values)
		{
			return new Person
			{
				PersonID = Convert.ToInt32(values[0]),
				Soname = Convert.ToString(values[1]),
				Name = Convert.ToString(values[2]),
				SecondName = Convert.ToString(values[3]),
				Position = Convert.ToString(values[4])
			};
		}

		#endregion

		#endregion Methods
	}
}
