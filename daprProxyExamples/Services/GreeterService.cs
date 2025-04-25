using Grpc.Core;
using daprProxyExamples;

namespace daprProxyExamples.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override async Task SayHello(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
    {
        for (int i = 0; i < 5; i++)
        {
            await responseStream.WriteAsync(new HelloReply
            {
                Message = "Hello " + request.Name + " " + i
            });
            await Task.Delay(1000); // Simulate some delay
        }
    }

    public override async Task<HelloReply> SayHelloUnary(HelloRequest request, ServerCallContext context)
    {
        return new HelloReply
        {
            Message = "Hello " + request.Name
        };
    }
}
