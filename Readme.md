# JWT Login Web Service

Here I am demonstrating you how we can separate the login reponsability of our web app in another WS.

This WS have just the responsabilities of:

* Register Users.
* Validate Users (Log In).

## Requirements

1. We are going to need a tool to call the web service endpoints like **[Postman](https://www.getpostman.com/)**.
2. We have to create the SQL data base, name it _loginService_.
3. Download this project, and run the unique migrations that exists to create the User table.

```
dotnet ef database update
```

4. Run this project with `dotnet run`.
5. **Download** and **run** my other [web service]() which is going to validate the token.

## Endpoints

* /api/signup
* /api/signin

## Steps

Once you have running the two web services, then:

1. Register an user with JWT Login Web Service.
2. Use the previous email and password to call the **signin endpoint** on JWT Login Web Service.
3. Copy the token returned.
4. Call the following protected method `/api/values` of the second web service. We are going to get 200 status code means that the token generated on JWT Login Web Service is valid on it, otherwise we are going to get Unauthorize status code.

_Note 1: Don't forget to use Postman to make the calls._

_Note 2: The expiration of the token is of 2 minutes, so we have to do all quickly._
