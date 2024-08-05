# HealthTech
An App designed to automate patient appointment creation.

## Implemented
- Built using Aspire for the Orchestrator and ServiceDefaults
- Blazor for the frontend
- Net 8 and Microservice Architecture
- DDD & Clean Architecture for the Booking.Api
- Mediatr to implement CQRS
- MSSQL as the Database accessed by by EF Core for the Aggregates & Dapper for the Queries / ViewModels
- RabbitMQ via Masstransit for inter-service communication
- Duende IdentityServer to handle logins and access to the Admin area.

C2 Model can be found presented in C2_HealthTechApp.plantuml and viewed in either VsCode with the PlantUML add-on or any online puml viewer

## Getting Started

1. You will need the latest version 8.1.0 of Aspire, follow this [guide](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/setup-tooling?tabs=windows&pivots=visual-studio) to update the workload.
1. Clone the repo
1. In the root of the repo open the solution with the following command
``` 
ii .\src\*sln
```
4. When in Visual Studio set the HealthTechApp.AppHost as the Start-up project
1. Once the app has spun up you will be presented with the Aspire dashboard where you can see and access each of the components, use the Https links.
   1. Booking.Api - https://localhost:7064/swagger/index.html
	1. Identity.Api - https://localhost:5243/
	1. HealthTechApp - https://localhost:7057/
1. To access the SQLServer databases or RabbitMq you will need to inspect the environment variables for the passwords on each run within the Aspire dashboard, alternatively you can set User-Secrets within the AppHost project and the credentials will persist through each run
1. When viewing the HealthTechApp you are presented with a public form for creating Appointments
1. when viewing the HealthTechApp a login button is located at the top-right (credentials provided on the login page) once logged in you will be able to access the Admin area via the NavBar


## TODO
1. A styling pass to get some of the rough edges sorted
1. Convert the process for pulling back the AppointmentSummary[] into a gRPC call and add pagination and filtering to the results
1. Fix bug in the redirect from IdentityServer back to App where the NavigationManager throws and exception and doesn't redirect
1. Get the DistributedApplicationTestingBuilder to work with the Identity.Api
1. Implement Domain Events within the Booking.Api
1. Figure out a better solution to update the AppointmentList page within the Admin area as the forced refresh of the whole page is jarring
1. Implement something using a Redis cache, potentially system notifications with a set TTL to be used by admin staff to put out updates and have them display on App
1. Implement BlazoredToast to improve feedback on actions


# Initial hand written notes
included as they are 90% accurate, some minor naming, additional files and folders added during development
![Notes](6BTechTestNotes.jpg)

