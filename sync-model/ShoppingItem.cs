/**
 * Copyright Red Hat, Inc, and individual contributors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using FHSDK.Sync;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace sync.model
{
	public class ShoppingItem : IFHSyncModel
	{
		public ShoppingItem() { }
		public ShoppingItem(string name)
		{
			Name = name;
			Created = DateTime.Now;
		}

		[JsonProperty("name")]
		public string Name { set; get; }

		[JsonProperty("created")]
        [JsonConverter(typeof(MillisecondEpochConverter))]
		public DateTime Created { set; get; }

		[JsonIgnore]
		public string UID { set; get; }

		public override string ToString()
		{
			return $"[ShoppingItem: UID={UID}, Name={Name}, Created={Created}]";
		}

		private bool Equals(IFHSyncModel other)
		{
			return string.Equals(UID, other.UID);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((ShoppingItem)obj);
		}

		public override int GetHashCode()
		{
			return UID?.GetHashCode() ?? 0;
		}

		public string GetCreatedTime()
		{
			return Created.ToString("MMM dd, yyyy, H:mm:ss tt");
		}

		public class MillisecondEpochConverter : Newtonsoft.Json.Converters.DateTimeConverterBase
		{
			private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				writer.WriteRawValue(Convert.ToInt64(((DateTime)value - _epoch).TotalMilliseconds).ToString());
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				if (reader.Value == null) { return null; }
				return _epoch.AddMilliseconds(Int64.Parse(reader.Value.ToString()));
			}
		}
	}
}
