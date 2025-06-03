# ProductAPI

A .NET Web API project for managing users, products, and checkout processes.

## Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/)
- [DB Browser for SQLite](https://sqlitebrowser.org/) or [DBeaver](https://dbeaver.io/)
- [Postman](https://www.postman.com/) for API testing

************************************************************************************************
** Setup Instructions **
************************************************************************************************

1. **Clone the repository**
   ```bash
   git clone https://github.com/LivhuwaniKM/ProductAPI.git

2. Run the Project

- Open the project in Visual Studio or your preferred IDE.
- PM> Update-database => to run migration [Package Manager]
- Run the project; it should automatically open Swagger UI in your browser.

3. Export Swagger JSON

- Change the Swagger URL in your browser from:

https://localhost:7290/swagger/index.html => https://localhost:7290/swagger/v1/swagger.json

- Copy the entire JSON content shown in the browser.

*************************************************************************************************
**Postman Integration**
*************************************************************************************************

4. Import API in Postman
   
4.1 Open Postman.

4.2 Click "Import" → "Raw Text".

4.3 Paste the copied OpenAPI JSON.

4.4 Click "Continue", then "Import".

*************************************************************************************************
** Postman Setup & API Usage **
*************************************************************************************************

5. Create Environment
   
5.1 In Postman, go to Environments.

5.2 Create a new environment named ProductEnv.

5.3 Add a variable:

- Key: product-token
- Initial Value: (leave blank for now)

![image](https://github.com/user-attachments/assets/dade95e3-a312-4e4a-a262-07b514bde31b)

6. Configure Collection Authorization

6.1 Go to the imported ProductAPI collection.

6.2 Under the Authorization tab:

- Type: Bearer Token
- Token: {{product-token}}

Ensure ProductEnv is the selected environment (top right corner in Postman).

![image](https://github.com/user-attachments/assets/e2c02915-7ed1-49d7-97a8-4e7cd73f3fda)

7. Set the Base URL

- In the Variables tab of the ProductAPI collection, set the base URL (example):

![image](https://github.com/user-attachments/assets/41e2bb55-25a1-4b8b-8fef-3bb243ab055c)

8. Register a New User and Get Token

8.1 Go to POST /api/User/register.

8.2 Register a new user with valid JSON body.

8.3 On a successful response, copy the returned token.

8.4 Right-click the token => Set as variable => choose product-token.

9. Create Products

9.1 Go to POST /api/Product/create.

9.2 Use valid data to create product(s).

10. View Products in SQLite

10.1 Open DB Browser for SQLite.

10.2 Load the database file located in the main project directory.

10.3 Run the query: SELECT * FROM Products

![image](https://github.com/user-attachments/assets/4bbab4a5-3a55-4351-a711-d7ab42604b75)

11. Start and Complete Checkout

11.1 Go to POST /api/Checkout/start to begin checkout.

11.2 Use valid ProductId, Quantity, and related data matching your DB records.

11.3 Then go to POST /api/Checkout/complete to finish the checkout process.
