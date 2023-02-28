using Azure.Messaging.ServiceBus;
using MessagePublisher;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new Publisher().PublishMessage());

app.Run();
