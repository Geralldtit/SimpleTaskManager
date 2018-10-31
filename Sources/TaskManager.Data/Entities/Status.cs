using System.Runtime.Serialization;

namespace TaskManager.Data.Entities
{
	[DataContract]
	public class Status
	{
		#region Variables

		private int _statusId;
		private string _statusTitle;

		#endregion

		#region Properties

		[DataMember]
		public int StatusId
		{
			get { return _statusId; }
			set { _statusId = value; }
		}

		[DataMember]
		public string StatusTitle
		{
			get { return _statusTitle; }
			set { _statusTitle = value; }
		}

		#endregion
	}
}
