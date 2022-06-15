# CSV-Transformer - Technical HomeTest

<!-- ABOUT THE PROJECT -->
## About The Project
This project is the technical home test for the position as a backend developer. The application is a small API-system for transforming delimited CSV files into a standardized schema.
The transformations are configurable, and invalid rows/fields will be collected. I chose to use C# with .NET / ASP.NET to build the API. This is because i am used to writing code with this language and framework.


### Built With
This application/API is built with C# .NET (ASP.NET), with Visual Studio as the IDE. I also used Nuget.org as a packet manager for external packages. External packages include xUnit for unit-tests and Swashbuckle.AspNetCore for Swagger.

* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [.NET](https://dotnet.microsoft.com/en-us/apps/aspnet/apis)
  * [Nuget](https://www.nuget.org/)
    * [xUnit](https://xunit.net/)
    * [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)


<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

To start the project you need to have an IDE installed. For example, Visual Studio or Visual Studio Code.

### Prerequisites

Download [Visual Studio](https://visualstudio.microsoft.com/downloads/) or [Visual Studio Code](https://code.visualstudio.com/download). Make sure to choose the correct platform (Mac/Windows).

### Installation

1. Unzip the attatched zip-file or download the project in zip format from the [GitHub repository](https://github.com/joakimstolen/HomeTest), and then unzip the file.
2. Open Visual Studio (or VS Code) and select open solution (or open project)
3. Select the HomeTestCrisp.sln file (or the HomeTest folder) 
4. When the project has loaded, click the play button in the toolbar of the IDE <img width="38" alt="image" src="https://user-images.githubusercontent.com/42611632/173849775-2c0c444d-d1c5-468d-a330-f20d11b4f3e6.png">
5. The project should now build and run and you will be prompted by a browser-window, displaying a Swagger GUI.



<!-- USAGE EXAMPLES -->
## Usage

To use the API, launch the project as described above. And when the Swagger GUI has launched, you can now do a POST-request to the endpoint /api/Orders/UploadCsvFile.
The endpoint takes a .csv file as a parameter. To add a .csv file, expand the endpoint in Swagger, select "Try it out" and then click "Select a file". When you have selected a file, you can execute the command. 
To test the API you can download [this sample .csv file](https://gist.github.com/daggerrz/99e766b4660e3c0ed26517beaea6449a).

The API will recieve your .csv file, transform the data into the desired state and return the data as a list of Order objects. If the csv file contains invalid data, it will not be returned and this will be catched and logged.

## How it works
<img width="698" alt="image" src="https://user-images.githubusercontent.com/42611632/173854124-9b5dee37-4077-4a13-9966-667b95885383.png">

* Firstly, the OrderController recieves the .csv file as a IFormFile, which represents a file sent with the HttpRequest.
* The controller then passes the file to the OrderService, where the content of the file is read through a StreamReader, reading line by line. 
* The StreamReader reads each line of the file, until the end of the file, adding each line in an empty list of strings, before returning. 
* The list of strings containing each line in the .csv file (in string format) is then passed to the next method, where the list of strings is turned into a list of RawOrderData, through the OrderTransformationConfiguration class constructor.
* Each string in the list of strings gets delimited by either a comma (,) or a semicolon (;). Although commas are the most usual delimitor for .csv files, i found that semicolons might also be used as a delimitor, in some cases.
* The transformation of the data in the .csv file is configurable in the OrderTransformationConfiguration class constructor. It takes a string array as parameter, and turns the string at a given position into a property of a given type. This can be changed to match your needs.
* <img width="440" alt="image" src="https://user-images.githubusercontent.com/42611632/173856838-ea95a202-3259-4518-98ed-6792985a11be.png">
* After the first transformation is complete, the list of raw order data is passed on to the next method, where the raw property data is turned into an Order object.
* The Order objects are the desired state the data should be returned as. For each field in the OrderTransformationConfiguration, the data is now renamed and transformed into the correct type. Example: Count => Quantity (BigDecimal / decimal in C#).
* When the method is done with the transformations, it is returned to the user as a list of Orders.

## Tests
I wrote unit tests for all the service-methods, achieving a test coverage of 74,5%. All tests run. For unit testing i used the testing framework xUnit, which is one of the reccomended testing frameworks to use with .NET / ASP.NET.

* To run the tests: 
  * In Visual Studio toolbar: click the "Test" tab, then click "Run All Tests".

## Assumptions / Simplifications

* I chose not to implement a database for this project. This is because i did not feel it was neccesary to provide, as the API was meant to only be used as a transformer of .csv data, and to do this i did not need a database.
* I chose to use Swagger as the main API documentation. I could've used tools such as Postman, but i am more comfortable with using Swagger. 
* I assumed the data that sent in the .csv file would be of the same structure every time (Order Number, Year, Month etc..). I log the error if it is not in the correct format, and i have a configuration file (OrderTransformationConfiguration), where you can edit the configurations. 

## Next steps

* I would like to implement a database for storing transformed csv-data. Here i would follow API-design principles and add a Repository with a DatabaseContext, and fully utilize the .NET Entity Framework, and following the [Repository-Service pattern](https://exceptionnotfound.net/the-repository-service-pattern-with-dependency-injection-and-asp-net-core/). 
* I would like to complete the CRUD-design (Create, Read, Update, Delete) for the API.
* I would like to create a more thorough and detailed transformation. Taking more unknown variables into consideration. 

<img width="938" alt="image" src="https://user-images.githubusercontent.com/42611632/173863382-f4e86442-4a48-44a4-8912-da85418e45f4.png">
A draft of what the application could look like with database implementation.
