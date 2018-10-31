using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TaskManager.Data.Entities
{

	[DataContract]
	public class Task
	{
		#region Variables

		private string _taskName;
		private int _hours;
		private DateTime _beginTime;
		private DateTime _endTime;
		private string _status;
		private int _statusId;
		private string _prShortName;
		private int _projectId;
		private List<Person> _taskPersons;

		#endregion

		#region Properties

		public Task()
		{
			_taskPersons = new List<Person>();
		}

		[DataMember]
		public int TaskId { get; set; }

		[DataMember]
		public string TaskName
		{
			set { _taskName = value; }
			get { return _taskName; }
		}

		[DataMember]
		public int Hours
		{
			set { _hours = value; }
			get { return _hours; }
		}

		[DataMember]
		public DateTime BeginTime
		{
			set { _beginTime = value; }
			get { return _beginTime; }
		}

		[DataMember]
		public DateTime EndTime
		{
			set { _endTime = value; }
			get { return _endTime; }
		}

		[DataMember]
		public string Status
		{
			set { _status = value; }
			get { return _status; }
		}

		[DataMember]
		public int StatusId
		{
			get { return _statusId; }
			set { _statusId = value; }
		}

		[DataMember]
		public string PrShortName
		{
			set { _prShortName = value; }
			get { return _prShortName; }
		}

		[DataMember]
		public int ProjectId
		{
			get { return _projectId; }
			set { _projectId = value; }
		}

		[DataMember]
		public List<Person> TaskPersons
		{
			set
			{
				_taskPersons = value;
			}
			get
			{
				return _taskPersons;
			}
		}

		#endregion
	}
}
