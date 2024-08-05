var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Booking_Api>("booking-api");

builder.AddProject<Projects.HealthTechApp_Web>("healthtechapp-web");

builder.Build().Run();
