using System;
using System.Globalization;
using System.Text.RegularExpressions;
using HomeTestCrisp.Configurations;
using HomeTestCrisp.Interfaces;
using HomeTestCrisp.Models;
using Microsoft.VisualBasic.FileIO;

namespace HomeTestCrisp.Services
{
    public class OrderService : IOrderService
    {

        private readonly ILogger<OrderService> _logger;

        public OrderService(ILogger<OrderService> logger)
        {
            _logger = logger;
        }

        public OrderService()
        {
            
        }


        public async Task<List<string>> ReadFormFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            List<string> lineValues = new List<string>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string headerLine = reader.ReadLine();
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    lineValues.Add(line);

                    _logger.LogInformation($"Adding line {line} to list.");
                }
                return lineValues;
            }
        }



        public List<OrderTransformationConfiguration> ReadRawCsvData(List<string> csvInStringFromFile)
        {
            List<OrderTransformationConfiguration> listOfRawOrderData = new List<OrderTransformationConfiguration>();
            
            foreach (var line in csvInStringFromFile)
            {
                TextFieldParser parser = new TextFieldParser(new StringReader(line));

                parser.HasFieldsEnclosedInQuotes = true;

                // Delimiters might be "," and ";", depending on what platform the CSV-file was created on.
                parser.SetDelimiters(",", ";");


                var i = 0;
                while (!parser.EndOfData)
                {

                    string[] values;
                    values = parser.ReadFields();


                    try
                    {
                        OrderTransformationConfiguration rawOrder = new OrderTransformationConfiguration(values);
                        listOfRawOrderData.Add(rawOrder);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"Row {values[i]} has invalid values. With exception {e}");
                    }

                    i++; 
                }   

                parser.Close();

            }
            return listOfRawOrderData;

        }




        public List<Order> TransformRawData(List<OrderTransformationConfiguration> listOfRawOrderData)
        {
            List<Order> listOfTransformedOrders = new List<Order>();
            foreach (var data in listOfRawOrderData)
            {
                Order order = new Order();

                order.OrderID = data.OrderNumber;
                order.OrderDate = CreateDateObject(data.Day, data.Month, data.Year);
                order.ProductId = data.ProductNumber;
                order.ProductName = data.ProductName;
                order.Quantity = decimal.Parse(data.Count, CultureInfo.InvariantCulture);
                order.Unit = "kg";

                listOfTransformedOrders.Add(order);
            }


            return listOfTransformedOrders;
        }


        public DateTime CreateDateObject(int day, int month, int year)
        {
            DateTime date = new DateTime(year, month, day);

            return date;
        }
    }
}

