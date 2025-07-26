# Library Automation API

## Overview

LibraryAutomationAPI is an ASP.NET Core Web API application developed to digitally manage library resources. Users can register, log in, and perform book and category operations securely using JWT-based authentication. The system runs on a SQL Server database and offers developers an easy way to test endpoints via Swagger UI.

## Features

- User registration and login with JWT authentication  
- Book management (add, list, update, delete)  
- Category management (add and list)  
- API testing support through Swagger UI  
- Authorization-controlled endpoints  
- SQL Server database integration  
- Data management via Entity Framework Core  

## Technologies Used

- ASP.NET Core 6 Web API  
- Entity Framework Core  
- SQL Server  
- JWT (JSON Web Token) Authentication  
- Swagger (Swashbuckle)  
- LINQ, Auto Mapping, Middleware architecture  

## Requirements

To run this project, you must have the following installed on your system:

- .NET 6 SDK  
- SQL Server (Express or full version)

## Key API Endpoints

- `POST /api/auth/register` → Register a new user  
- `POST /api/auth/login` → Log in and receive a token  
- `GET /api/books` → List all books  
- `POST /api/books` → Add a new book  
- `PUT /api/books/{id}` → Update book details  
- `DELETE /api/books/{id}` → Delete a book  
- `GET /api/categories` → List all categories  
- `POST /api/categories` → Add a new category  

## License

This project is licensed under the MIT License. Anyone is free to copy, distribute, and modify it.
