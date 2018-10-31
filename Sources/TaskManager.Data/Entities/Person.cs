using System.Runtime.Serialization;

namespace TaskManager.Data.Entities
{
	[DataContract]
	public class Person
	{
		#region Variables

		private int _personId;
		private string _soname;
		private string _name;
		private string _secondName;
		private string _position;

		#endregion

		#region Properties

		[DataMember]
		public int PersonID
		{
			set
			{
				_personId = value;
			}
			get { return _personId; }
		}

		[DataMember]
		public string Soname
		{
			set { _soname = value; }
			get { return _soname; }
		}

		[DataMember]
		public string Name
		{
			set { _name = value; }
			get { return _name; }
		}

		[DataMember]
		public string SecondName
		{
			set { _secondName = value; }
			get { return _secondName; }
		}

		[DataMember]
		public string Position
		{
			set { _position = value; }
			get { return _position; }
		}

		#endregion
	}
}
