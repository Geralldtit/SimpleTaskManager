using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TaskManager.Data.Entities
{
	[DataContract]
	public class Project
	{
		#region Variables

		private int _projectId;
		private string _prName;
		private string _prShortName;
		private string _description;
		private IList<Task> _prTasks;

		#endregion

		#region Properties

		public Project()
		{
			_prTasks = new List<Task>();
		}

		[DataMember]
		public int ProjectID
		{
			set { _projectId = value; }
			get { return _projectId; }
		}

		[DataMember]
		public string PrName
		{
			set { _prName = value; }
			get { return _prName; }
		}

		[DataMember]
		public string PrShortName
		{
			set { _prShortName = value; }
			get { return _prShortName; }
		}

		[DataMember]
		public string Description
		{
			set { _description = value; }
			get { return _description; }
		}

		[DataMember]
		public IList<Task> PrTasks
		{
			set
			{
				//removed to constructor
				_prTasks = value;
			}
			get
			{
				return _prTasks;
			}
		}

		#endregion
	}
}
