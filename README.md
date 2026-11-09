# RaceDayPOEPart3

## RaceDay Race Event Management System

### Student Information

**Name:** Sandile Siyo  
**Student Number:** ST10476405  
**Project:** RaceDay POE Part 3  

---

# 1. Project Overview

RaceDay is a race event management system developed using ASP.NET Core.

The solution consists of two main applications:

- **RaceDay Web API** for data access and backend functionality.
- **RaceDay MVC** for the user interface.

The system allows race organisers and participants to interact with race information through a web-based interface.

Organisers can manage race events, registrations and results, while participants can browse available events, view registrations and access race results.

The project uses SQL Server for persistent data storage and Entity Framework Core for database access.

---

# 2. Technologies Used

The RaceDay solution uses the following technologies:

- ASP.NET Core
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- BCrypt.Net
- Bootstrap
- HTML
- CSS
- Razor Views
- C#
- xUnit
- Git
- GitHub
- GitHub Actions / CI
- Visual Studio 2022
- Azure Blob Storage

---

# 3. Solution Architecture

The RaceDay solution follows a separated frontend and backend architecture.

```text
RaceDay.MVC
       |
       | HTTP Requests
       v
RaceDay Web API
       |
       | Entity Framework Core
       v
SQL Server Database
```

The MVC application is responsible for displaying the user interface.

The Web API processes requests and communicates with the SQL Server database through Entity Framework Core.

---

# 4. Solution Structure

The solution contains the following main projects:

```text
RaceDayPOEPart3
│
├── RaceDay
│   └── ASP.NET Core Web API
│
├── RaceDay.MVC
│   └── ASP.NET Core MVC Web Application
│
├── RaceDay.Tests
│   └── xUnit Tests
│
├── .github
│   └── workflows
│
├── README.md
│
└── RaceDay.sln
```

---

# 5. RaceDay Web API

The RaceDay API provides backend functionality for the application.

The API contains controllers responsible for working with the system's data.

Examples include:

```text
Controllers
├── AuthController
├── EventsController
├── CategoryController
├── RegistrationController
├── ResultController
├── WeatherController
└── UserController
```

The API communicates with the RaceDay SQL Server database using Entity Framework Core.

---

# 6. RaceDay MVC Application

RaceDay.MVC provides the graphical user interface for the system.

The MVC project communicates with the RaceDay Web API using HTTP requests.

The MVC application contains:

- Home page
- Login page
- Registration page
- Participant dashboard
- Organiser dashboard
- Race event management
- Registration management
- Results management
- Create Event page
- Edit Event page
- Navigation and role-related pages

The interface uses Bootstrap and Razor Views to provide a responsive and consistent design.

---

# 7. User Authentication

RaceDay includes user authentication functionality.

Users can:

- Register an account
- Login
- Logout
- Access functionality based on their role

Passwords are protected using BCrypt password hashing.

The system supports different user roles, including:

- Participant
- Organiser

---

# 8. Organiser Functionality

The Organiser Dashboard provides access to management functionality.

Organisers can access:

### Manage Events

Organisers can:

- View events
- Create events
- Edit events
- Delete events
- View event information

### Registrations

Organisers can access participant registration information.

### Results

Organisers can access race result management functionality.

The Organiser Dashboard provides quick navigation to the main management areas of the application.

---

# 9. Participant Functionality

The Participant Dashboard provides participants with access to race-related functionality.

Participants can access:

### Browse Events

Participants can browse RaceDay events.

### My Registrations

Participants can access their registration-related functionality.

### My Results

Participants can access race result information.

The Participant Dashboard provides a clear interface for accessing the main RaceDay participant features.

---

# 10. Event Management

RaceDay provides event-management functionality through the MVC application and Web API.

Event information includes:

- Event name
- Event description
- Event date
- Location
- Distance
- Event type
- Event image URL

Organisers can create and edit race events through MVC forms.

The MVC application sends event information to the Web API, which saves the information in SQL Server.

---

# 11. Event Images

RaceDay includes event image support.

When creating an event, an organiser can select an image file.

Supported image formats include:

```text
JPG
JPEG
PNG
WEBP
```

The application also validates the image size before processing the upload.

A maximum image size of **5 MB** is used by the current implementation.

---

# 12. Azure Blob Storage

Azure Blob Storage was implemented as an additional RaceDay feature.

The image upload process is:

```text
Organiser selects event image
            |
            v
RaceDay MVC receives image
            |
            v
RaceDay API receives multipart upload
            |
            v
BlobStorageService
            |
            v
Azure Blob Storage
            |
            v
Image URL saved to Event record
```

Uploaded images are stored in the Azure Blob container:

```text
event-images
```

The application generates unique blob names for uploaded event images.

The resulting Azure Blob URL is stored in the event's `ImageUrl` field.

The RaceDay event interface also provides a fallback display where an event image is unavailable to the browser.

---

# 13. Database

RaceDay uses SQL Server as its database.

The current application database is:

```text
RaceDayDBs
```

The Entity Framework Core context is:

```text
RaceDayDbContext
```

The database stores the information required by the RaceDay system.

Major entities include:

- Users
- Events
- Categories
- Registrations
- Results
- Weather
- Routes

---

# 14. Database Connection

The project uses a SQL Server connection string named:

```text
RaceDayConnection
```

Example local configuration:

```json
{
  "ConnectionStrings": {
    "RaceDayConnection": "Server=(localdb)\\practical_assignment;Database=RaceDayDBs;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Developers should update the SQL Server information when necessary for their own development environment.

---

# 15. Azure Configuration

The Azure Blob Storage service uses the following connection-string configuration name:

```text
AzureBlobStorage
```

The actual Azure Storage credentials should be configured locally and should not be published publicly in source control.

---

# 16. Entity Framework Core

Entity Framework Core is used to communicate between the RaceDay API and SQL Server.

The application uses:

```text
RaceDayDbContext
```

to access the RaceDay database entities.

Database migrations are used to manage database structure changes where required.

---

# 17. Main API Endpoints

## Authentication

```text
POST /api/Auth/register
POST /api/Auth/login
```

## Events

```text
GET    /api/Events
GET    /api/Events/{id}
POST   /api/Events
PUT    /api/Events/{id}
DELETE /api/Events/{id}
```

## Event Image Upload

```text
POST /api/Events/{id}/image
```

This endpoint accepts an event image using multipart form data.

---

## Categories

```text
GET    /api/Category
GET    /api/Category/{id}
POST   /api/Category
PUT    /api/Category/{id}
DELETE /api/Category/{id}
```

---

## Registrations

```text
GET    /api/Registration
GET    /api/Registration/{id}
POST   /api/Registration
DELETE /api/Registration/{id}
```

---

## Results

```text
GET    /api/Result
GET    /api/Result/{id}
POST   /api/Result
PUT    /api/Result/{id}
DELETE /api/Result/{id}
```

---

## Weather

```text
GET    /api/Weather
GET    /api/Weather/{id}
POST   /api/Weather
DELETE /api/Weather/{id}
```

---

## Users

```text
GET /api/User
GET /api/User/{id}
PUT /api/User/{id}
```

---

# 18. Swagger / OpenAPI

Swagger is included in the RaceDay Web API.

Swagger makes it possible to inspect and test the API endpoints during development.

When the API project runs in the development environment, Swagger provides an interface containing the available RaceDay endpoints.

---

# 19. Data Validation

RaceDay uses validation to prevent incorrect or incomplete information from being submitted.

Validation used throughout the project includes:

- Required-field validation
- Email validation
- Data integrity validation
- Event image type validation
- Event image size validation
- Model validation

Validation messages are displayed to the user where appropriate.

---

# 20. Unit Testing

The project contains a separate test project:

```text
RaceDay.Tests
```

The xUnit testing framework is used.

Tests were created for important parts of the system, including authentication, event-related logic and registration-related logic.

Examples include:

### Authentication Tests

- Valid password test
- Invalid password test

### Event Tests

- Event name validation
- Event distance validation

### Registration Tests

- Registration status validation
- Registration Event ID validation

---

# 21. GitHub Version Control

Git is used for version control throughout development.

The final repository is:

```text
RaceDayPOEPart3
```

The repository maintains the development commit history from the RaceDay project.

GitHub is used to store the source code and version history.

---

# 22. GitHub Actions / CI

A GitHub Actions workflow is included under:

```text
.github/workflows
```

The RaceDay CI Pipeline is used to validate the repository after code is pushed.

The final Part 3 project was pushed to the `master` branch and the RaceDay CI Pipeline completed successfully.

---

# 23. User Interface

The RaceDay MVC interface was improved during Part 3 to provide a cleaner and more consistent user experience.

The interface includes:

- RaceDay branding
- Responsive Bootstrap navigation
- Home page
- Login and Register navigation
- Participant Dashboard
- Organiser Dashboard
- Event cards
- Create Event interface
- Edit Event interface
- Registration navigation
- Results navigation
- Success, warning and error messages
- Event image placeholders

The user interface uses a blue RaceDay colour theme across the application.

---

# 24. Home Page

The RaceDay Home page introduces the system and provides direct access to Login and Register.

The Home page highlights three key areas:

- Race Events
- Registrations
- Race Results

---

# 25. Participant Dashboard

The Participant Dashboard provides easy navigation to:

```text
Browse Events
My Registrations
