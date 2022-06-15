using System;
using HomeTestCrisp.Configurations;
using HomeTestCrisp.Models;

namespace HomeTestCrisp.Interfaces
{
	public interface IOrderService
	{
		Task<List<string>> ReadFormFileAsync(IFormFile file) => throw new NotImplementedException();
		List<OrderTransformationConfiguration> ReadRawCsvData(List<string> csvInStringFromFile);
		List<Order> TransformRawData(List<OrderTransformationConfiguration> listOfRawOrderData);
	}
}

