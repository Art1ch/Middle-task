
using DataProcessorService.Application;
using NotificationService.Api.Extensions;
using NotificationService.Api.Hubs;
using NotificationService.Api.Implementations;
using NotificationService.Application.Abstractions;

namespace NotificationService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSignalR();

        var kafkaSettings = builder.ConfigureKafkaSettings();
        var corsSettings = builder.ConfigureCorsSettings();

        builder.Services.AddApplicationLayer(kafkaSettings);
        builder.Services.AddScoped<INotificationPublisher, SignalRNotificationPublisher>();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(corsSettings.PolicyName, policy =>
            {
                policy
                    .WithOrigins(corsSettings.OriginAddress)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.UseCors(corsSettings.PolicyName);
        app.MapHub<SensorHub>("/hubs/sensors");
        app.Run();
    }
}
