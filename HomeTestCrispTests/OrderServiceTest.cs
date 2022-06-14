using System.Text;
using HomeTestCrisp.Configurations;
using HomeTestCrisp.Interfaces;
using HomeTestCrisp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace HomeTestCrispTests;

public class OrderServiceTest 
{


	public OrderServiceTest()
	{
	}
	

	[Fact]
	public async Task Test_ReadsIFormFile()
	{

        var logger = new LoggerFactory().CreateLogger<OrderService>();
        IOrderService myService = new OrderService(logger);


        var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
		IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.csv");

		

		var t = await myService.ReadFormFileAsync(file);


	    Assert.Equal(typeof(List<string>), t.GetType());

	}


	[Fact]
    public async Task Test_ReadRawCsvData_Will_Fail()
    {


        var logger = new LoggerFactory().CreateLogger<OrderService>();
        IOrderService myService = new OrderService(logger);


        List<string> sampleList = new List<string> { "test1", "test2", "test3", "test4" };


        var t = myService.ReadRawCsvData(sampleList);

        Assert.Empty(t);
        Assert.False(t.Count > 0);


    }


    [Fact]
    public async Task Test_ReadRawCsvData_Will_Succeed()
    {
        var logger = new LoggerFactory().CreateLogger<OrderService>();
        IOrderService myService = new OrderService(logger);

        List<string> sampleList = new List<string> { "Order Number,Year,Month,Day,Product Number,Product Name,Count,Extra Col1,Extra Col2,Empty Column", "1000,2018,1,1,P-10001,Arugola,2, Lorem,Ipsum,", "1001,2017,12,12,P-10002,Iceberg lettuce,500.00,Lorem,Ipsum," };

        var t = myService.ReadRawCsvData(sampleList);

        Assert.NotEmpty(t);
        Assert.True(t.Count > 0);

    }


    [Fact]
    public async Task Test_OrderTransformationConfiguration()
    {
        var logger = new LoggerFactory().CreateLogger<OrderService>();

        string[] sampleList = new string[] {  "1000", "2018","1","1","P-10001","Arugola","2" };

        OrderTransformationConfiguration orderTransformation = new OrderTransformationConfiguration(sampleList);

        Assert.NotNull(orderTransformation);
        Assert.Equal(orderTransformation.ProductName, "Arugola");

    }




    [Fact]
    public async Task Test_TransformRawData()
    {
        var logger = new LoggerFactory().CreateLogger<OrderService>();
        IOrderService myService = new OrderService(logger);

        string[] sampleList = new string[] { "1000", "2018", "1", "1", "P-10001", "Arugola", "2" };

        OrderTransformationConfiguration orderTransformation = new OrderTransformationConfiguration(sampleList);
        List<OrderTransformationConfiguration> listOfOrder = new List<OrderTransformationConfiguration>();
        listOfOrder.Add(orderTransformation);

        var t = myService.TransformRawData(listOfOrder);

        Assert.NotEmpty(t);
        Assert.True(t.Count > 0);
        //Assert.Equal(t[0], );


    }






}
