# .NET Light Boilerplate 
This Boilerplate is meant to speed up the build for new .NET Web Solutions based on a readable & scalable software architecture. Current version is beta version. 
An example is included in the current version of boilterplate which using SQLite as database.
## Backend
- Entity Framework 6 Code First (support Database migration)
- Autofac
- [Dynamic Linq](https://github.com/kahanu/System.Linq.Dynamic)
- AutoMapper
- Microsoft.Extensions.Logging and NLog
- NUnit
- WebAPI 2
- .Net Framework 4.5.2
- Visual Studio 2015 Community
## Frontend
- [Yeoman (AngularJS, Requirejs)](https://github.com/aaronallport/generator-angular-require)
- Bootstrap 3.X
- Angular Bootstrap UI
- Grunt
## Design 
- Domain Driven Design(DDD)
  - Presentation (WebAPI & WebAPP)

- Application (Application services & DTOs)
  - Domain (Commnads, Command Handlers, Events, Event Handlers, Entities & EventBus)
  - Infrasturcture (Repositories, ErrorHandler, Logger, DIManager & Database providers)
- Contract & Interface
  - Contract in boilerplate is the only interface used by Autofac by giving a readable configuration. Contract sometimes inherits interface otherwise it dosen't make any sense
  - Interface in boilerplate is the typical interface....
- CQRS (Command/Event/EventBus)
  - Each of database providers should have 2 Dbcontext refering to write side and read side, for instance, Light.SQLite in AgeRanger, AgeRangerDbContext is for read side and AgeRangerWriterDbContext is for write side. Write side and read side are using same database in current circumstance. It can be configured in Web.config/connectionString :
  ```
  <connectionStrings>
    <add name="AgeRangerDB" connectionString="data source=|DataDirectory|\sqlite\AgeRanger.db;foreign keys=true" providerName="System.Data.SQLite" />
    <add name="AgeRangerDBWriter" connectionString="data source=|DataDirectory|\sqlite\AgeRanger.db;foreign keys=true" providerName="System.Data.SQLite" />
  </connectionStrings>
  ```
  - CommandHandler is resposible for command handling and event registering. For instance, PersonCommnadHanlder registers PersonCreateEvent/PersonUpdatedEvent/PersonNotCreatedEvent/PersonNotUpdatedEvent and handle PersonCreateCommand/PersonUpdateCommand. The EventHandler you chose to register events is based on the business rules. In one CommandHandler, multiple Events are able to be registered with multiple EventHandlers for multiple Commands
  - EventHanler is resposible for dealing the stuffs after Event is triggered. For instatnce, if one person is created successfully, PersonCreateEvent is triggered and the related EventHanler is executed to send a email message, log some stuffs or integrate with MSMQ etc.
  - Application services supply two kinds of services. One is used to query, the other is used to send Command to CommandHandler. Query service is called directly to communicate with Repository. Command service is called to communicate with CommandHandler
- Dependence Injection
  - Autofac
  - DI is used in every laryer, so each layer is independent even WebAPI
  - DI is also used to inject instance of DbContext into instance of Repository. It means it is very easy to migrate to any database providers if the database provider was included in Infrasturcture
  - DI configurations consists of two parts. One part is in json file resgistering database providers and event handlers. The other part is in Global.asax/Application_Start resgistering others.
- ORM
  - EntityFramework 6 Code First with SQLite extensions
- Logger
  - Logger is only a controller to be used to register log providers on the basis of Microsoft.Extensions.Logging and NLog provider. Changing provider only need create inheritance from LoggerController or create new class implementing ILoggerController and register new controller in Global.asax/Application_Start using Autofac
- ErrorHandler
  - ErrorHandler provides a strategy transforming uncatchced exceptions from server to event and trigger the event, so the details of excption will not expose to client only in log files.
  - ErrorHandler provides global error handling for WebAPI using ActionExceptionFilter of WebAPI and return related HttpStautsCode to client
  - WebApp also has global error hander making efforts by being injected into AngularJS httpProvider
- DIManager
  - DIManager provide IoC container instance by implementing IDIProvider interface
  - If you want to change IoC container, create a new class and implement this IDIProvider doing your logic.
- API Help
  - WebAPI provides API help pages. "http://{Domain}/Help"
## Deploy
### WebApi (automatic way)
1. Locate to Tools\Deploy\Service\ and run "nuget restore Light.sln" to restore packages
2. Install [MSDeploy](https://www.iis.net/downloads/microsoft/web-deploy)
3. Create a new website "Light" and a empty physical folder mapping to "Light" website in IIS
4. Open cmd under Administrator and locate to "C:\Program Files (x86)\MSBuild\14.0\Bin"
5. Run "MSBuild Tools\Deploy\Service\Project.csproj"
6. If successful, a new folder "Light" would be created in C:\ including a folder for backup of the existing website and a folder for the undeployed package of Light
7. Check the "Light" website. All of files would be there
### WebApp
1. Install Nodejs
2. Intall npm, bower and grunt
3. Locate to "\Presentation\App"
4. Run "grunt build"
5. Check the folder dist
6. Don't forget to find file "config.js" in folder scripts and change the api base url
## TODO
- Authentication & Authorization is not there (e.g. Json Token).
- Expression Converter for orderBy
