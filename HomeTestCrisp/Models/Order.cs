using System;
namespace HomeTestCrisp.Models
{
	public class Order
	{
		public Order()
		{
		}


        public int OrderID { get; set; }
		public DateTime OrderDate { get; set; }
		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal Quantity { get; set; }
		public string Unit { get; set; } // KG fixed
	}
}

