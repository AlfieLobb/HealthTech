var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Booking_Api>("booking-api");

builder.Build().Run();
