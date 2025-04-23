using Dapr.Client;
using daprProxyExamples;
using Grpc.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddGrpcClient<Greeter.GreeterClient>("client1", options => {
    options.CallOptionsActions.Add(o =>
        o.CallOptions = o.CallOptions.WithHeaders(new Metadata { { "dapr-app-id", "grpc-server" } }));
    var daprPort = Environment.GetEnvironmentVariable("DAPR_GRPC_PORT");
    options.Address = new Uri($"http://localhost:{daprPort}");
});

builder.Services.AddGrpcClient<Greeter.GreeterClient>("client2", options => {
    options.Creator = _ => new Greeter.GreeterClient(DaprClient.CreateInvocationInvoker("grpc-server"));
    options.Address = new Uri("http://localhost");
});

var app = builder.Build();

app.MapControllers();

app.Run();

