using System;
namespace HomeTestCrisp.Models
{
	public class Order
	{

		//int orderId, DateTime orderDate, string productId, string productName, decimal quantity, string unit

		public Order()
		{
			//OrderID = orderId;
			//OrderDate = orderDate;
			//ProductId = productId;
			//ProductName = productName;
			//Quantity = quantity;
			//Unit = unit; 
		}


        public int OrderID { get; set; }
		public DateTime OrderDate { get; set; }
		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal Quantity { get; set; }
		public string Unit { get; set; } // KG fixed
	}
}

