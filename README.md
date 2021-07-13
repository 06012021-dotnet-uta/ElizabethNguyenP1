# ElizabethNguyenP1

## Project Description
This is a ASP.NET Core MVC project utilizing Entity Framework Core to create a Web Store Application that allows a user to create an account then view orders by user and store location. The user can create an order, view their order history, and view the order history of a store location.

## Enviroments & Technologies Used

*  C# Programming, HTML5, CSS3, SQL
*  Microsoft SQL Server Management Studio (SSMS)
*  Visual Studio (ASP.NET Core MVC Project)
*  ADO.NET Entity Framework
*  GitHub

## Features

* Provides Sign up, Login, and Purchasing functionality 
* Allows users to dynamically browse products, view location inventories, and edit current order information
* Implements inventory cards to populate products with actual images
* Issues closable warning labels for potentially dangerous actions such changing locations with items still in cart
* Provides order history information by user purchases or by location purchases

To-do list:
* Provide additional order history information such as items purchased.
* Encrypt passwords within the database.
* Provide Unit Tests to test functionality

## Getting Started & Usage
This application is currently hosted locally and will require a local database and Visual Studio to operate. 

In order to view & interact with the application, 
1: Clone the repository 
2: Create a database using the seed SQL file in the root folder - "P0_DB.sql"
3: Edit the connection string in Visual Studio to access your local database
4: Run the application locally via Visual Studio
