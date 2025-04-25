using Dapr.Client;
using daprProxyExamples;
using Grpc.Core;
using Grpc.Net.Client;

Console.WriteLine("request grpc server with direct....");
var channel1 = GrpcChannel.ForAddress("http://localhost:8080"); // grpc-server port
var client1 = new Greeter.GreeterClient(channel1);
var reply1 = client1.SayHello(new HelloRequest { Name = "direct" });
while (await reply1.ResponseStream.MoveNext())
{
    Console.WriteLine("Greeting: " + reply1.ResponseStream.Current.Message);
}


// Console.WriteLine("request grpc server with dapr proxy....");
// var channel2 = GrpcChannel.ForAddress("http://localhost:50001"); // dapr grpc port
// var client2 = new Greeter.GreeterClient(channel2);
// var reply2 = client2.SayHello(new HelloRequest { Name = "proxy from dapr" }, new Metadata(){{"dapr-app-id", "grpc-server"}});
// while (await reply1.ResponseStream.MoveNext())
// {
//     Console.WriteLine("Greeting: " + reply2.ResponseStream.Current.Message);
// }

var invoker = DaprClient.CreateInvocationInvoker("grpc-server", "http://localhost:50001");
var client3 = new Greeter.GreeterClient(invoker);
var reply3 = client3.SayHello(new HelloRequest { Name = "proxy from dapr invoker" });
while (await reply3.ResponseStream.MoveNext())
{
    Console.WriteLine("Greeting: " + reply3.ResponseStream.Current.Message);
}

var client4 = new Greeter.GreeterClient(invoker);
var reply4 = client3.SayHelloUnary(new HelloRequest { Name = "proxy unary from dapr invoker" },new Metadata
{
    { "dapr-app-id", "server" },
    { "dapr-stream", "false" },
});
while (await reply3.ResponseStream.MoveNext())
{
    Console.WriteLine("Greeting: " + reply3.ResponseStream.Current.Message);
}