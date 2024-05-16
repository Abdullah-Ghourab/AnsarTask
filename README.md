# AnsarTask
This is a .NET Web API application that provides endpoints for managing `Person` objects. The application uses JWT authentication with claims or roles to secure the POST and DELETE endpoints. It also includes SQL Server as the data storage and Identity for user management.

## Prerequisites

- .NET 8 
- SQL Server (or SQL Server Express)
- Visual Studio (or any other compatible IDE)

## Setup

1. Clone the repository or open the project in Visual Studio. git clone https://github.com/Abdullah-Ghourab/AnsarTask.git

2. You have to change token issuer url in tokenservice.cs file based on the api running url
For example:https//localhost:1234

3. Build and run the application in Visual Studio.

## Usage

1. To access the secured `POST` and `DELETE` endpoints, you need to obtain a JWT token by authenticating as the admin user.

2. Use the login endpoint (`/api/auth/login`) to authenticate with the following admin credentials:
  - Username: `Admin@Ansar.com`
  - Password: `P@ssword123`

3. The login endpoint will return a JWT token. Copy the token value.

4. Go to authorize at the top right of the swagger page, and paste "Bearer followed by the Generated Token Value" ex:
  "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c".

5. You can now make requests to the following API endpoints:
- `GET /api/Persons`: Retrieve all persons
- `GET /api/Persons/{id}`: Retrieve a person by ID
- `POST /api/Persons`: Create a new person (requires JWT token with "Admin" role)
- `DELETE /api/Persons/{id}`: Delete a person by ID (requires JWT token with "Admin" role)

6. For the `POST` and `DELETE` endpoints, include the JWT token in the `Authorization` header as shown in step 4.

