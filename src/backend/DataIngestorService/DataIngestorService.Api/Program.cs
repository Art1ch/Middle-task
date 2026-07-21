using DataIngestorService.Api.BackgroundWorkers;
using DataIngestorService.Api.Extensions;
using DataIngestorService.Application;
using DataIngestorService.Infrastructure;
using Shared.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sensorDataFetcherSettings = builder.ConfigureHttpDataSensorFetcherSettings();
var kafkaSettings = builder.ConfigureKafkaSettings();

builder.Services.AddApplicationLayer(kafkaSettings);
builder.Services.AddInfrastructureLayer(sensorDataFetcherSettings);
builder.Services.AddSharedImplementations();

builder.Services.AddHostedService<SensorDataPublisherBackgroundWorker>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
