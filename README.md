
# Table of Contents

-   [Lecture info](#org1cc5e31)
1.  [Developing Summary](#org2547c69)
    1.  [Design](#orgc8f9d4a)
    2.  [UML](#orgecf8b77)
    3.  [Development order](#org35ffc5e)
2.  [Start the Project](#org0dac9ff)
3.  [Not Impemented](#orgbbaf03c)



<a id="org1cc5e31"></a>

# Lecture info

-   Date: <span class="timestamp-wrapper"><span class="timestamp">[2023-11-07 Tue]</span></span>


<a id="org2547c69"></a>

# Developing Summary


<a id="orgc8f9d4a"></a>

## Design

I decided to develop the project with Code First approach, so I created before the models of my application and after with Entiry Framework I generate the databases.
This way I focused on developing the Web APIs without having too many problems with data persistence.
I developed the Project how two independent microservices, with dedicated SQLite databases.
So we have:

-   **Login Web API**, this microservice is responsible to manage SignUp and SignIn of users.
    This API, when user is logged in, authorized the operations in Lab Manager with a token.
-   **Lab Manager Web API**, this microservice is main application, it&rsquo;s responsible to manage a Lab Computer Science.

The two microservices communicate with each other via the frontend with token, in this way:

1.  The **Login Web API** when a user is logged return a token.
2.  The **Login Frontend** save this token in local storage.
3.  The **Lab Web API** authenticate sigle HTTP methods with token retrieved from local storage with Lab Frontend.


<a id="orgecf8b77"></a>

## UML

This is the **UML** of project:

![img](../docs/img/project_8_1_uml.png "Project 8.1 UML")


<a id="org35ffc5e"></a>

## Development order

1.  Developing **Login Web API**
    1.  **Model** Creation
    2.  Migration and Seeding by Entiry Framework
    3.  **Controller** and **CRUD**
    4.  Test with Swagger
2.  Developing **Lab Web API**
    1.  **Model** Creation
    2.  Definition of table realtionships
    3.  Migration and Seeding by Entiry Framework
    4.  **Controllers** and **CRUD**
    5.  Test with Swagger
3.  Add password with Nuance in **Login Web API**
4.  Frontend
    1.  Login and SignUp View
    2.  Lab Admin CRUD Viewes
    3.  Lab Client CRUD View
5.  Auth by token for **Lab Web API CURD**


<a id="org0dac9ff"></a>

# Start the Project

To start the project, start the servers in this order:

1.  **Login Web API**
    -   Move in **LoginWebAPI**
    -   Run `dotnet restore` to install dependency.
    -   Run server with `dotnet watch` in terminal
2.  **Lab Web API**
    -   Move in **LabWebAPI**
    -   Run `dotnet restore` to install dependency.
    -   Run server with `dotnet watch` in terminal
3.  **Frontend**
    -   Install dependency with `npn install`
    -   Run server with `npm start`

> NOTE: If acces to the databases doesn&rsquo;t work, redo the Migrations and Seeding for each Web API, like this:
> 
> 1.  Remove **Migrations** folder
> 2.  Remove SQLite database from **db** folder
> 3.  Run `dotnet ef migrations add FirstCreate` in terminal
> 4.  Run `dotnet ef database update` in terminal
> 5.  Run `dotnet run -- seeddata` in terminal


<a id="orgbbaf03c"></a>

# Not Impemented

-   Test
-   Token expires
-   Booking by time slots (my Lab Web API booking by date form)
-   Logout (clean local storage)
-   Login Web API Frontend View (the frontend not work)
-   When I booking an item, I Have to change item status. Status come back available when the reservation expires.

