# RaceDay
RaceDay API
Student Information
Name Sandile Siyo
Student Number: ST10476405

Project Overview
RaceDay API is a RESTful Web API developed using ASP.NET Core and Entity Framework Core.

The system is designed to manage sporting events such as marathons, cycling races, and other athletic competitions. The API allows organisers to create and manage events while participants can register, view events, and access race results.

Technologies Used
ASP.NET Core Web API
Entity Framework Core
SQL Server
Swagger (OpenAPI)
BCrypt.Net
xUnit Testing Framework
Visual Studio 2022
Database Structure
The database contains the following entities:

Users
Stores participant and organiser information.

Events
Stores race event details

Categories
Stores event categories and age groups

Registrations
Stores participant registrations for events

Results
Stores race performance results

Weather
Stores weather forecast information for events.

Routes
Stores race route information

Main Features
Authentication
Register User
Login User
Password Hashing using BCrypt
Event Management
Create Event
View Events
Update Event
Delete Event
Category Management
Create Category
View Categories
Update Category
Delete Category
Registration Management
Register Participants
View Registrations
Delete Registrations
Results Management
Create Results
View Results
Update Results
Delete Results
Weather Management
Create Weather Forecast
View Weather Information
Delete Weather Information
API Endpoints
Authentication
POST /api/Auth/register
POST /api/Auth/login
Events
GET    /api/Events
GET    /api/Events/{id}
POST   /api/Events
PUT    /api/Events/{id}
DELETE /api/Events/{id}
Categories
GET    /api/Category
GET    /api/Category/{id}
POST   /api/Category
PUT    /api/Category/{id}
DELETE /api/Category/{id}
Registrations
GET    /api/Registration
GET    /api/Registration/{id}
POST   /api/Registration
DELETE /api/Registration/{id}
Results
GET    /api/Result
GET    /api/Result/{id}
POST   /api/Result
PUT    /api/Result/{id}
DELETE /api/Result/{id}
Weather
GET    /api/Weather
GET    /api/Weather/{id}
POST   /api/Weather
DELETE /api/Weather/{id}
Users
GET    /api/User
GET    /api/User/{id}
PUT    /api/User/{id}
Validation
The project uses Data Annotation Validation including:

Required Fields
Email Validation
Data Integrity Validation
Unit Testing
Unit tests were created using the xUnit framework.

Implemented Tests
Authentication Tests
Valid Password Test
Invalid Password Test
Event Tests
Event Name Validation
Event Distance Validation
Registration Tests
Registration Status Validation
Registration Event ID Validation
Test Results
Passed: 6
Failed: 0
Database Configuration
Connection String:

"ConnectionStrings": {
  "RaceDayConnection": "Server=(localdb)\\practical_assignment;Database=RaceDayDBs;Trusted_Connection=True;TrustServerCertificate=True;"
}
Running the Application
Step 1
Open the solution in Visual Studio 2022

Step 2
Restore NuGet Packages.

Step 3
Update the database:

Update-Database
Step 4
Run the application:

F5
Step 5
Swagger will open automatically

Project Structure
RaceDay
│
├── Controllers
│   ├── AuthController
│   ├── EventsController
│   ├── CategoryController
│   ├── RegistrationController
│   ├── ResultController
│   ├── WeatherController
│   └── UserController
│
├── Models
│   ├── User
│   ├── Event
│   ├── Category
│   ├── Registration
│   ├── Result
│   ├── Weather
│   └── Route
│
├── Data
│   └── RaceDayDbContext
│
├── DTOs
│   ├── RegisterDto
│   └── LoginDto
│
└── Tests
    ├── AuthTests
    ├── EventTests
    └── RegistrationTests
Conclusion
The RaceDay API successfully demonstrates the implementation of a RESTful Web API using ASP.NET Core, Entity Framework Core, and SQL Server. The project includes secure user authentication, event management functionality, data validation, unit testing, and comprehensive API documentation using Swagger.
