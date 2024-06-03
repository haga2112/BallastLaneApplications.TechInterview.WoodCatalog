# BallastLaneApplications.TechInterview.WoodCatalog
Tech interview exercise for Ballast Lane Applications company.

## The project Wood Catalog

Wood Catalog API project is a web api developed mainly with .NET 8, Docker, Redis.

It was developed as a Proof of Concept (POC) following good practices of modern software development (as per june 2024).

Some key aspects:
 - easy to run using docker
 - the API itself is RESTful as much as possible
 - Swagger was used for documentation and presentation layer
 - JWT bearer token for authentication 

### User stories

This project was developed keeping in mind the following user stories:

- As not a user of the system, I want to register myself, so that I have a login and password
- As an user already registered in the system, I want to login, so that I have a token that allow me to access restricted endpoints
- As a logged in user with a token, I want to add, get, update and delete wood types in the system, so that I can manage timbers

### Architecture

This Web API solution was conceived keeping in mind a Domain Driven Design (also known as "Hexagonal Architecture").

In practice, this means there is a "central project" that provides all the entities, C# interfaces and of course encapsulate the main business logic.

Here are all the projects provided in this solution:

- WoodCatalog.API
- WoodCatalog.Domain
- WoodCatalog.Redis
- WoodCatalog.Tests

This strategy should be enough to keep a good separation of concerns (isolation of components), make this API easy to maintain and easy to expand.

#### WoodCatalog.Domain

The so called "central project", WoodCatalog.Domain that holds all the entities models (POCO classes), contracts (interfaces for services and repositories) and business logic of the application (actual implementation of the service interfaces).

Every project should be referencing the **WoodCatalog.Domain** project.

#### WoodCatalog.API

WoodCatalog.API is a .NET 8 ASP.NET Core Web API with AuthController and WoodController providing basic endpoints. 

This project also has a Dockerfile with enough configuration to run it using Docker easily (more on this later).

##### Authentication

This project was configured to use a JWT token that would eventually allow the use of claims to check for permissions (authorization). 

#### WoodCatalog.Redis

This is the database implementation of the repository interfaces. 

I choose Redis as a primary database because it is reliable and easy to ship using Docker, specially for this POC scenario.

#### WoodCatalog.Tests

Basic unit tests project with xUnit, Moq and Bogus dependencies.

The unit tests was segregated per folder (API and Domain).

## How to run

### Requisites

You will need:

- Git
- .NET 8
- Visual Studio 2022
- Docker Desktop

### How to run on docker

1. After configured the dependencies above, download this project using _git_ and please run the script located at folder **_WoodCatalog_** called **_run.cmd_**
2. You can also go that folder using command prompt and then run **_docker compose up -d_** yourself
   * The _docker compose_ command should download and set up the redis database, build the .NET solution and publish it 
4. Open Docker Desktop and check if **woodcatalog** container is up and running with "web-1" API and "redis-1" database
5. That's it, now go the browser and open the web api that should be running locally at **http://localhost:5000/swagger/index.html**

### How to debug

Make sure **redis** is up and running in Docker Desktop using the steps above as the docker-compose.yaml file has the instructions set it up.

1. Using Visual Studio 2022, open the **WoodCatalog solution**.
2. Make sure **WoodCatalog.API** is the Start Project
3. Rebuild the solution
4. Now the easiest way is to run it using the **https profile**
   * Make sure at **_Program.cs_** you are using the IP address **"127.0.0.1:6379"** to connect to the local Redis container
     ```
     builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
        ConnectionMultiplexer.Connect("127.0.0.1:6379,abortConnect=false"));
     ```
  
#### Container (Dockerfile) debug

When using this debug profile please note that you should adjust the redis configuration, so go to the **_Program.cs_** file and change the _ConnectionMultiplexer.Connect_ parameter to **"host.docker.internal:6379"**.

```
  builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
      ConnectionMultiplexer.Connect("host.docker.internal:6379,abortConnect=false"));
```

## How to use the API

The following instructions will utilize the Swagger of the locally run API on port 5000.

You will learn how to register, login and use the **_/wood-catalog_** endpoints.

### Login

After setting up and running the project, you should have the API locally at _**http://localhost:5000/swagger/index.html**_.

The only endpoints that allow anonymous requests are the two **_/auth_** endpoints.

So first, register yourself using the **POST /auth/register** endpoint sending the request like:

```
{
  "name": "my user",
  "password": "strong password"
}
```
You will receive your user id (something like **_user:1b83e1c4-a367-4a25-871c-946290689316_**), so save this value for later.

Now you can login using the **POST /auth/login** endpoint using your **id and password**.

After this as a response you will get a JWT token like:

![image](https://github.com/haga2112/BallastLaneApplications.TechInterview.WoodCatalog/assets/6050706/d690773a-0100-42ca-b591-c92c15aabd1e)

Authenticate in swagger using the token you got using the _Authorize_ button:

![image](https://github.com/haga2112/BallastLaneApplications.TechInterview.WoodCatalog/assets/6050706/f2e920c9-f616-42f5-9533-74cb02507aa2)

Congratulations, now you are logged in.

### Wood Catalog endpoints

After you logged in, now you can add wood using **POST /wood-catalog** endpoint with the example request:

```
{
  "name": "oak",
  "quality": "hard"
}
```

Using the wood id returned by the POST endpoint you can try the _GET_, _PUT_ and _Delete_ endpoints. 

The **GET /wood-catalog/get-all** is the only endpoint that doesn't require a parameter.

## Next steps

Some ideas for the next steps to make this project production ready:

- ensure id prefix is used only server-side
- Serverside pagination (get rid of /get-all)
- Postman flow
- Middleware for error handling
- Viewmodel to allow sanitization of inputs
- CQRS and Mediator Pattern
