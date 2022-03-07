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
* BindingModels : This contains custom classes that allow ASP.Net engine to  

### Installation

1. Get a free API Key at [https://example.com](https://example.com)
2. Clone the repo
   ```sh
   git clone https://github.com/your_username_/Project-Name.git
   ```
3. Install NPM packages
   ```sh
   npm install
   ```
4. Enter your API in `config.js`
   ```JS
   const API_KEY = 'ENTER YOUR API';
   ```



<!-- USAGE EXAMPLES -->
## Usage

Use this space to show useful examples of how a project can be used. Additional screenshots, code examples and demos work well in this space. You may also link to more resources.

_For more examples, please refer to the [Documentation](https://example.com)_



<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/othneildrew/Best-README-Template/issues) for a list of proposed features (and known issues).



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.



<!-- CONTACT -->
## Contact

Your Name - [@your_twitter](https://twitter.com/your_username) - email@example.com

Project Link: [https://github.com/your_username/repo_name](https://github.com/your_username/repo_name)


