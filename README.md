
### Continue working on project using Gitpod

Click the button below to start a new development environment:

[![Open in Gitpod](https://gitpod.io/button/open-in-gitpod.svg)](https://gitpod.io/#https://github.com/elhuk/clean-architecture-proj)

# Clean Architecture Sample Project

The purpose of this project is to build a reference architecture demonstrating how to implement the Clean Architecture Structure when working with .Net solutions.

Clean Architecture is a layered architecture that splits into four layers:

Domain
Application
Infrastructure
Presentation

Here's a visual representation of the Clean Architecture:

{// add clean architecture image here //}


## Domain Layer
The Domain layer sits at the core of the Clean Architecture. Here we define things like: entities, value objects, aggregates, domain events, exceptions, repository interfaces, etc.

Here is the folder structure I like to use:
```
📁 Domain
|__ 📁 DomainEvents
|__ 📁 Entities
|__ 📁 Exceptions
|__ 📁 Repositories
|__ 📁 Shared
|__ 📁 ValueObjects
```
You can introduce more things here if you think it's required.

One thing to note is that the Domain layer is not allowed to reference other projects in your solution.

## Application Layer
The Application layer sits right above the Domain layer. It acts as an orchestrator for the Domain layer, containing the most important use cases in your application.

You can structure your use cases using services or using commands and queries.

I'm a big fan of the CQRS pattern, so I like to use the command and query approach.

Here is the folder structure I like to use:

```
📁 Application
|__ 📁 Abstractions
    |__ 📁 Data
    |__ 📁 Email
    |__ 📁 Messaging
|__ 📁 Behaviors
|__ 📁 Contracts
|__ 📁 Entity1
    |__ 📁 Commands
    |__ 📁 Events
    |__ 📁 Queries
|__ 📁 Entity2
    |__ 📁 Commands
    |__ 📁 Events
    |__ 📁 Queries
``` 
In the Abstractions folder, I define the interfaces required for the Application layer. The implementations for these interfaces are in one of the upper layers.

For every entity in the Domain layer, I create one folder with the commands, queries, and events definitions.

## Infrastructure Layer
The Infrastructure layer contains implementations for external-facing services.

What would fall into this category?

Databases - PostgreSQL, MongoDB
Identity providers - Auth0, Keycloak
Emails providers
Storage services - AWS S3, Azure Blob Storage
Message queues - Rabbit MQ
Here is the folder structure I like to use:
```
📁 Infrastructure
|__ 📁 BackgroundJobs
|__ 📁 Services
    |__ 📁 Email
    |__ 📁 Messaging
|__ 📁 Persistence
    |__ 📁 EntityConfigurations
    |__ 📁 Migrations
    |__ 📁 Repositories
    |__ #️⃣ ApplicationDbContext.cs
|__ 📁 ...
```
I place my DbContext implementation here if I'm using EF Core.

It's not uncommon to make the Persistence folder its project. I frequently do this to have all database facing-code inside of one project.

## Presentation Layer
The Presentation layer is the entry point to our system. Typically, you would implement this as a Web API project.

The most important part of the Presentation layer is the Controllers, which define the API endpoints in our system.

Here is the folder structure I like to use:
```
📁 Presentation
|__ 📁 Controllers
|__ 📁 Middlewares
|__ 📁 ViewModels
|__ 📁 ...
|__ #️⃣ Program.cs
```
Sometimes, I will move the Presentation layer away from the actual Web API project. I do this to isolate the Controllers and enforce stricter constraints. You don't have to do this if it is too complicated for you.

[Source](https://www.milanjovanovic.tech/blog/clean-architecture-folder-structure)
