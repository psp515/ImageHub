<div align="center">
  
  <h1> Image Hub </h1>
  <p> Let's find IT 😎 </p>

  <div>
    <a href="https://github.com/psp515/ImageHub/network/members">
      <img src="https://img.shields.io/github/forks/psp515/ImageHub" alt="forks" />
    </a>
    <a href="https://github.com/psp515/ImageHub/stargazers">
      <img src="https://img.shields.io/github/stars/psp515/ImageHub" alt="stars" />
    </a>
    <a href="https://github.com/psp515/ImageHub/issues/">
      <img src="https://img.shields.io/github/issues/psp515/ImageHub" alt="open issues" />
    </a>
    <a href="https://github.com/psp515/ImageHub/blob/master/LICENSE">
      <img src="https://img.shields.io/github/license/psp515/ImageHub" alt="license" />
    </a>
    <a href="https://codecov.io/gh/psp515/ImageHub">
      <img src="https://codecov.io/gh/psp515/ImageHub/graph/badge.svg" alt="coverage" />
    </a>
    <a href="https://github.com/psp515/ImageHub/actions/workflows/BuildDotnet.yml">
      <img src="https://github.com/psp515/ImageHub/actions/workflows/BuildDotnet.yml/badge.svg" alt="build" />
    </a>
    <a href="https://github.com/psp515/ImageHub/actions/workflows/TestDotnet.yml">
      <img src="https://github.com/psp515/ImageHub/actions/workflows/TestDotnet.yml/badge.svg" alt="test" />
    </a>
  </div>
</div>

<br/>

### Build With

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white&style=flat)
![.NET Web API](https://img.shields.io/badge/.NET_Web_API-0089D6?style=for-the-badge&logo=dotnet&logoColor=white&style=flat)
![.NET 8](https://img.shields.io/badge/.NET_8-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white&style=flat)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-0089D6?style=for-the-badge&logo=dotnet&logoColor=white&style=flat)

### Projct Idea

The project is dedicated to delivering a robust API for image management, introducing three pivotal entities: Image, Thumbnail (automatically generated from the original image), and ImagePack, a compact grouping mechanism for related images. The Image entity acts as the fundamental representation of visual content, accompanied by its Thumbnail counterpart, optimized for efficient retrieval. The addition of ImagePack facilitates organized storage and retrieval of similar images, enhancing the overall management capabilities of the API. By combining these entities, the project aims to provide a versatile and user-friendly solution for developers to seamlessly manage, organize, and retrieve images within their applications or systems.

<b>Features:</b>
- Image management
- ImagePack management
- Thumbnail management
- Image processing into thumbnail

### Project Aim

The project's primary objective is to gain a fundamental understanding of Vertical Slice Architecture (VSA) within the context of C#. VSA is a software development approach that emphasizes the creation of complete, end-to-end slices of functionality across all layers of an application. By implementing VSA in C#, the goal is to enhance modularity, maintainability, and scalability, ultimately allowing for the efficient development of fully functional vertical slices that encapsulate specific features or components of the application. This approach facilitates iterative development and enables teams to deliver tangible and demonstrable results at each stage of the development process.

Project secondary goal is to get familiar with usefull nuget packages like MediatR and SignalR.

Project aim is not to create personal accounts.

### How to run project

#### Development Environment
In order to run this api you need to start required services like postgres and rabbitmq. To do that run:
- ```docker compose up -d``` (you can remove api container)
- then migrate database for example from Visual Studio
- now you run it in visial studio

### Project Conclusions

#### Vertical Slice Architecture

Vertical Slice Architecture (VSA) in handling small-scale projects is great. The convenience of having all crucial classes neatly organized within a single folder greatly streamlines the code and test development process. It's a notable advantage that simplifies navigation and enhances overall project management.

However, as projects expand in scope, the potential emergence of cross-cutting concerns poses a challenge. The sheer growth in the Infrastructure folder may lead to a mix of elements from various features, potentially diminishing the initial organizational benefits. In such cases, careful consideration and perhaps a more scalable architecture may be necessary to maintain the project's clarity and efficiency.

Usefull Resources:
- [Vertical Slice by Milan Jovanović](https://www.milanjovanovic.tech/blog/vertical-slice-architecture)
- [Vertical Slice discussion](https://www.linkedin.com/posts/davidcallan_dotnet-softwarengineering-cleanarchitecture-activity-7157374290630205440-zOMb/)

#### SignalR

SignalR configuration is easy and intuitive. Worth noting is that SignalR uses websockets.
It is great when we want to notify user that something just happend and applications can respond to that.

Usefull Resources:
- [Backend Communication Examples by kova98](https://github.com/kova98/BackendCommunicationPatterns.NET?fbclid=IwAR32fMvYalIR55mac3CgMjAaUPl7GqMBn3_ZAHi5gmxkoQypg9hMoKodRcs)
- [Real-Time Notifications with SignalR by Milan Jovanović](https://www.youtube.com/watch?v=O7oaxFgNuYo&t=429s)

#### CQRS

CQRS stands for Command and Query Responsibility Segregation, a pattern that aims to separate read and update operations for a data store. 
We can implement it on single database and also on separate read / write databases (in this solution I use single database). 

In combination with MediatR it gives us nice way of divisin into tasks like:
- Handlers - Here is main domain logic
- Queries - we are passing this data in order to retrive information from database (just getting data)
- Commands - commands have different task than queries they are responsible for updating, removing and adding new objects.

With splitting into separate databases this combination gives us improved performance because read database can focus on retriving data and write database can focus on longer actions when we are saving data to database.

Usefull Resources:
- [Microsoft Documentation](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [CQRS + MediatR by Milan Jovanović](https://www.youtube.com/watch?v=vdi-p9StmG0&t=1017s)

#### Mediator Pattern

MediatR is really nice implementation of Mediator Pattern. I find Pipelines really usfull. Thanks to them we can simplify Handlers by pulling out validation to single file common for all features. Also it allows to nicely struct the project and makes it more clear which elements are responsible for what.

Usefull Resources:
- [MediatR Repo](https://github.com/jbogard/MediatR)

#### Serilog

Serilog as a logging library is really nice it is easy to use. However we need to remember of preparing appropriate configuration for logging. Also worth noting is that it fits nicely with MediatR pipeline behaviors.

Usefull Resources:
- [Logging as a Cross-Cutting Concern with MediatR by Milan Jovanović](https://www.youtube.com/watch?v=JVX9MMpO6pE)

#### Test Containers

Test Containers are great for testing purposes. Combining it with xunit makes teststing easy even in CI/CD pipeline and thanks to that we can test real behavior on actual database like PostgreSQL. Key to note is that configuration is really important in order to use proper containers (when I started with test containers my tests were using my postgres database and not database created just for testing purposes the result of that was tests didn't work in Github Actions and I spent a lot of time figuring what is the problem).

Usefull Resources:
- [The Best Way To Use Docker For Integration Testing In .NET by Milan Jovanović](https://www.youtube.com/watch?v=tj5ZCtvgXKY&t=445s)s