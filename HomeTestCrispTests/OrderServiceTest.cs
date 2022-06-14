using System.Text;
using HomeTestCrisp.Interfaces;
using HomeTestCrisp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace HomeTestCrispTests;

public class OrderServiceTest 
{
	
		//private readonly IOrderService _orderService;

		public OrderServiceTest()
		{
			//_orderService = orderService;
		}


		[Fact]
		public async Task ReadsIFormFile()
		{
		var logger = new LoggerFactory().CreateLogger<OrderService>();

		var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
		IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt");

		var service = new OrderService(logger);

		var t = await service.ReadFormFileAsync(file);
		Console.WriteLine(t);

			Assert.NotNull(t);

		}

	
}
