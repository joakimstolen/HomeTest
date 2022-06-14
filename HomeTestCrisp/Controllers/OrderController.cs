using HomeTestCrisp.Interfaces;
using HomeTestCrisp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace HomeTestCrisp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{


    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;

    public OrderController(ILogger<OrderController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    [HttpGet(Name = "GetOrders")]
    public string Get()
    {
        return "";
    }




    [HttpPost("UploadCsvFile")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {

        _logger.LogInformation(file.FileName);

        var csvDataAsString = await _orderService.ReadFormFileAsync(file);


        var convertedData = _orderService.ReadRawCsvData(csvDataAsString);

        var transformedData = _orderService.TransformRawData(convertedData);

        return Ok(transformedData);
    }
}

