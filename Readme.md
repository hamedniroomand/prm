# PRM (Project Resource Management)

A .NET 8.0 based Project Resource Management with JWT authentication and role-based authorization.

## Features

- JWT-based Authentication
- Role-based Authorization (SuperAdmin, Admin, Employee)
- Project Management
- Task Management
- User Management
- Swagger API Documentation
- SQL Server Database

## Prerequisites

- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 or VS Code with C# extensions

## Project Structure

- `PRM.API`: Main API project containing controllers and API configurations
- `PRM.Application`: Core business logic and data access layer
- `PRM.Contracts`: Shared DTOs and interfaces

## Environment Variables

Create a `.env` file in the root directory with the following variables:

```env
JWT_SECRET=your_jwt_secret_key_here
DATABASE_CONNECTION=Server=your_server;Database=your_database;User Id=your_username;Password=your_password;TrustServerCertificate=True;
```

### Environment Variables Description

- `JWT_SECRET`: Secret key used for JWT token generation and validation
- `DATABASE_CONNECTION`: SQL Server connection string

## Getting Started

1. Clone the repository
2. Create the `.env` file with required environment variables
3. Navigate to the API project directory:
   ```bash
   cd PRM.API
   ```
4. Run database migrations:
   ```bash
   dotnet ef database update
   ```
5. Run the application:
   ```bash
   dotnet run
   ```

## API Documentation

Once the application is running, you can access the Swagger UI documentation at:
```
https://localhost:7001/swagger
```

## Authentication

The API uses JWT Bearer token authentication. Include the token in the Authorization header:
```
Authorization: Bearer your_jwt_token
```

## User Roles

The system supports three user roles:

- `SuperAdmin`: Full system access
- `Admin`: Administrative access
- `Employee`: Basic user access

## Development

### Adding New Migrations

To add a new migration:
```bash
dotnet ef migrations add MigrationName
```

### Updating Database

To apply migrations to the database:
```bash
dotnet ef database update
```

## Dependencies

### Main Dependencies
- Microsoft.AspNetCore.Authentication.JwtBearer (8.0.14)
- Microsoft.EntityFrameworkCore (8.0.14)
- Microsoft.EntityFrameworkCore.SqlServer (8.0.14)
- Microsoft.AspNetCore.Identity (2.3.1)
- Swashbuckle.AspNetCore (6.4.0)
