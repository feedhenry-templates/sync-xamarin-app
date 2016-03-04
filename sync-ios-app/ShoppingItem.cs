using System;
using FHSDK.Sync;
using Newtonsoft.Json;

namespace sync_xamarin_app
{
	public class ShoppingItem: IFHSyncModel
	{
		public ShoppingItem(string name)
		{
			Name = name;
			Created = (long) (DateTime.Now - GetEpoch()).TotalMilliseconds;
		}

		[JsonProperty("name")]
		public string Name { set; get; }

		[JsonProperty("created")]
		public long Created { set; get; }

		[JsonIgnore]
		public string UID { set; get; }

		public override string ToString()
		{
			return string.Format("[ShoppingItem: UID={0}, Name={1}, Created={2}]", UID, Name, Created);
		}

		private DateTime GetEpoch() 
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		}

		public string GetCreatedTime() {
			return GetEpoch().AddMilliseconds(Created).ToString ("MMM dd, yyyy, H:mm:ss tt");
		}

		private bool Equals(IFHSyncModel other)
		{
			return string.Equals(UID, other.UID);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((ShoppingItem) obj);
		}

		public override int GetHashCode()
		{
			return (UID != null ? UID.GetHashCode() : 0);
		}
	}
}

