using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace QDMarketPlace.Areas.Admin.Models
{
    public class DataPoint
    {
		public DataPoint(string label, double y)
		{
			this.Label = label;
			this.Y = y;
		}

		[DataMember(Name = "label")]
		public string Label = "";

		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
	}
}
