var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ApiGateway>("apigateway");

builder.AddProject<Projects.Auth_Api>("auth-api");

builder.AddProject<Projects.Posts_Api>("posts-api");

builder.AddProject<Projects.Users_Api>("users-api");

builder.Build().Run();
