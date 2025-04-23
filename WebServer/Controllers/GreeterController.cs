using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers;

using daprProxyExamples;
using Grpc.Net.ClientFactory;

[ApiController]
[Route("[controller]")]
public class GreeterController(GrpcClientFactory grpcClientFactory) : ControllerBase
{

    [HttpGet(Name = "Greeter")]
    public string Get()
    {
        var client1 = grpcClientFactory.CreateClient<Greeter.GreeterClient>("client1");
        var client2 = grpcClientFactory.CreateClient<Greeter.GreeterClient>("client2");
        var reply1 = client1.SayHello(new HelloRequest { Name = "GreeterClient1" });
        var reply2 = client2.SayHello(new HelloRequest { Name = "GreeterClient2" });

        return $"{reply1.Message} <--> {reply2.Message}";
    }
}
