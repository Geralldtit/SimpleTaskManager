using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TaskManager.Data
{
	public class Dao
	{
		#region Methods

		/// <summary>
		/// Execute non query command with parameters form dictionary
		/// </summary>
		/// <param name="connectionString">string connectionString</param>
		/// <param name="sqlString">string sqlString</param>
		/// <param name="parameters">Dictionary parameters</param>
		protected void ExecuteCommand(string sqlString, Dictionary<object, object> parameters, string connectionString)
		{
			using (var con = new SqlConnection(connectionString))
			{
				if (con.State != ConnectionState.Open) con.Open();
				ExecuteCommand(sqlString, parameters, con);
			}
		}

		/// <summary>
		/// Execute non query command with parameters form dictionary
		/// </summary>
		/// <param name="sqlString">string sqlString</param>
		/// <param name="parameters">Dictionary parameters</param>
		/// <param name="con">SqlConnection con</param>
		protected void ExecuteCommand(string sqlString, Dictionary<object, object> parameters, SqlConnection con)
		{
			var cmd = new SqlCommand(sqlString, con);
			foreach (var de in parameters)
			{
				cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
			}

			cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Get record from DB with unique id and return array of values
		/// </summary>
		/// <param name="sqlString">string sqlString</param>
		/// <param name="id">int id</param>
		/// <param name="connectionString">string connectionString</param>
		/// <returns>object[] values</returns>
		protected object[] GetById(string sqlString, int id, string connectionString)
		{
			using (var con = new SqlConnection(connectionString))
			{
				if (con.State != ConnectionState.Open) con.Open();
				return GetById(sqlString, id, con);
			}
		}

		/// <summary>
		/// Get record from DB with unique id and return array of values
		/// </summary>
		/// <param name="sqlString">string sqlString</param>
		/// <param name="id"> int id</param>
		/// <param name="con">SqlConnection con</param>
		/// <returns>object[] values</returns>
		protected object[] GetById(string sqlString, int id, SqlConnection con)
		{
			var cmd = new SqlCommand(sqlString, con);
			cmd.Parameters.AddWithValue("@Id", id);
			var reader = cmd.ExecuteReader();

			if (reader == null) return null;

			reader.Read();
			var values = new object[reader.FieldCount];
			reader.GetValues(values);
			reader.Close();

			return values;
		}

		#endregion
	}
}
