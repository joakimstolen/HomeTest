using System;
namespace HomeTestCrisp.Models
{
	public class RawOrder
	{
		public RawOrder(string[] values)
		{
			this.OrderNumber = Convert.ToInt32(values[0]);
			this.Year = Convert.ToInt32(values[1]);
			this.Month = Convert.ToInt32(values[2]);
			this.Day = Convert.ToInt32(values[3]);
			this.ProductNumber = values[4];
			this.ProductName = values[5];
			this.Count = values[6];

		}

        public int OrderNumber { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public int Day { get; set; }
		public string ProductNumber { get; set; }
		public string ProductName { get; set; }
		public string Count { get; set; }




        
    }
}

