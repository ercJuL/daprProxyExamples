using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<daprProxyExamples>("grpc-server").WithDaprSidecar();
builder.AddProject<WebServer>("web").WithDaprSidecar();

builder.Build().Run();
