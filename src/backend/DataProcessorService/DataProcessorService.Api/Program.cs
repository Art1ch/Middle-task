using DataProcessorService.Api.Extensions;
using DataProcessorService.Api.GraphQL.Queries;
using DataProcessorService.Application;
using DataProcessorService.Infrastructure;
using Shared.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var kafkaSettings = builder.ConfigureKafkaSettings();
var sensorsDatabaseSettings = builder.ConfigureSensorsDatabaseSettings();

builder.Services.AddApplicationLayer(kafkaSettings);
builder.Services.AddInfrastructureLayer(sensorsDatabaseSettings);
builder.Services.AddSharedImplementations();

builder.Services.AddGraphQLServer()
    .AddQueryType<SensorsDataQueries>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGraphQL();
app.Run();
