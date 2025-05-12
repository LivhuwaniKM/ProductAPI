# ProductAPI

A .NET Web API project for managing users, products, and checkout processes.

## Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/)
- [DB Browser for SQLite](https://sqlitebrowser.org/) **or** [DBeaver](https://dbeaver.io/) for managing the database
- [Postman](https://www.postman.com/) for testing API endpoints

## Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/LivhuwaniKM/ProductAPI.git

3. Run the Project and Oepn Swagger

4. Copy OpenAPI JSON

Change the Swagger URL in your browser from:

https://localhost:7290/swagger/index.html

to:

https://localhost:7290/swagger/v1/swagger.json

then:

Copy the entire JSON content shown in the browser.

**Postman Integration**

6. Import to Postman

6.1 Open Postman.

6.2 Click Import.

6.3 Select Raw Text.

6.4 Paste the copied JSON.

6.5 Click Continue, then Import.

7. Update Base URL in Postman

NB! Ensure the base URL for all requests in Postman matches the URL where your API is running,
