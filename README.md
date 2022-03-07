# LondonStockApi


<br />
<p align="center">

  <h3 align="center">London Stock API</h3>

  <p align="center">
    A brief description about the API, project structure and code.
   
    
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#database-schema">Database Schema</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
     </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

The project contains implementation of a REST API called LondonStockApi and its underlying database. The API was built with below high level functional requirements in mind-
1. Receive trade notifications from brokers(update stock prices).
2. Publish prices of all the stocks.
3. Publish price of a single stock.
4. Publish price of a range of stocks.

Out of scope:
1. Buy-Sell order matching- The API does not perform any sort of order purchase and sale order matching. It assumes that this will be done by the brokers.
2. Validations on stock quantity- The API does not perform any validations on the stock quantity/availability. It again assumes that brokers will send only valid trades. 
3. Loggging of requests - For the purpose of MVP, request logging has not been enabled.

### High Level System Design
The system comprises of a REST API layer that connects to an underlying database. The REST API is written in ASP.Net and the database has been chosen to be an Sqlite database for the sake of MVP. EntityFramework core is being used to connect to the database however the Sqlite DB can  be switched to any preferred database and the API should still work as long as relevant database handler is supplied in the dependency injection. The project also uses Swagger CodeGen and UI for testing purposes.
In its current MVP form, there is no load balancing involved but this is something that can be implemented on a separate layer for both API as well as the DB so as to allow high volume of traffic and icnrease resillience. 


### Built With

* The API was build on Visual Studio 2022 using .NET 6.0. 
* It uses sqlite database which can be downloaded for free at <a href="https://www.sqlite.org/">SQLITE</a>. 
* It utilizes Entity Framework for managing database queries. 
* Swagger for testing and documenting
* AutoMapper for  mapping between different class objects.


<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Database Schema

In its very minimal form, the database consists of 4 tables. Below is a simple DB diagram showing the database structure-

![picture alt](https://github.com/dhawaldhingra/LondonStockApi/blob/master/LondonStockApi/Database/Database%20structure.png "Database Schema")

### Project Structure

![picture alt](https://github.com/dhawaldhingra/LondonStockApi/blob/master/LondonStockApi/Project%20Structure.png "Project Structure")

The code has been maintained under a folder structure. Below is a brief description of what can be found in each of the folders-
* BindingModels : This contains custom classes that allow ASP.Net engine to the mapping between incoming request data and controller model. For the MVP, we've only one custom model binder that converts a comma seprated ticker strings into IEnumerable<string>.
* Contollers : Contains the APIs/Controllers for the project. For MVP, we've only one API called Stocks.
* Database : Contains the SQLITE DB containing the data for the project. Although it isn't strictly a part of the API, but it has been bundled with the project with some dummy data in it for ease of review and testing purpose.
* DataModels : This folder contains data/class objects representing the tables in the database.
* Models : This folder contains the class objects representing the Input/Outputs of the API.
* Profiles : Contains AutoMapper profiles for mapping between Models and DataModels.
* Repository : Contains the code for getting stocks data from and updating data in the database.



<!-- USAGE EXAMPLES -->
## API Working
The controller allows for dependency injection of database handler and automapper. This takes place when the constructor for the API is invoked. The constructor of the API also initializes a simple implementation of cache for BrokerIds and Tickers. This is because the Tickers and BrokerIds are expected to remain static where as the stock prices will continously keep changing. Moreover, as we've very limited tickers and broker in live (aprox 5k), we can store them in memory without taking too much space. This list of tickers and broker ids is later on used for validation of any new trade information that is recevied by the API.
As the database calls are expensive, caching of prices of various stocks was also considered however not pursued for MVP. This is because it poses additional challenges if the API were to run in a load balanced environment.
The controller has three GET methods and one POST method. The details of these methods such as their routes, signatures etc can be found in the swagger spec and therefore has not been included in the Readme file.


