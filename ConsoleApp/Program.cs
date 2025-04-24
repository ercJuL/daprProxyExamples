using daprProxyExamples;
using Grpc.Core;
using Grpc.Net.Client;

Console.WriteLine("request grpc server with direct....");
var channel1 = GrpcChannel.ForAddress("http://localhost:8080"); // grpc-server port
var client1 = new Greeter.GreeterClient(channel1);
var reply1 = await client1.SayHelloAsync(new HelloRequest { Name = "direct" });
Console.WriteLine(" " + reply1.Message);

Console.WriteLine("request grpc server with dapr proxy....");
var channel2 = GrpcChannel.ForAddress("http://localhost:50001"); // dapr grpc port
var client2 = new Greeter.GreeterClient(channel2);
var reply2 = await client2.SayHelloAsync(new HelloRequest { Name = "proxy from dapr" }, new Metadata(){{"dapr-app-id", "grpc-server"}});
Console.WriteLine(" " + reply2.Message);
