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

6.2 Click Import Workspace

6.3 Paste cURL, Raw Text.

6.5 Import (Save button).

7. Update Base URL in Postman

**API Postman Usage:**

Run api and open postman

1 Create environment variable called ProductEnv and give provide token varable of 'product-token'
![image](https://github.com/user-attachments/assets/dade95e3-a312-4e4a-a262-07b514bde31b)

2. Go to collections, select main ProductAPI collection

- Under authorization panel on main panel, choose 'Auth Type' of 'Bearer Token'.
- Create value of '{{product-token}}' which is same as environment variable name.
- Make sure ProductEnv is the one that is active selected near top right left.
![image](https://github.com/user-attachments/assets/e2c02915-7ed1-49d7-97a8-4e7cd73f3fda)

3. Set correct base url as swagger api url on browser under variables section on main panel
![image](https://github.com/user-attachments/assets/41e2bb55-25a1-4b8b-8fef-3bb243ab055c)

4.
- Go to ProductAPI/api/User/register and create new user, on response data there will be a token
- select token, right click on it and select Set as variable and select 'property-token' option

5. Go to ProductAPI/api/Product/create url and create product(s)

6. Open DB Beaver or DB Browser for SQlite and open database file which should be in the main project file structure.

7. Go to ProductAPI/api/Checkout/start url and create new checkout
