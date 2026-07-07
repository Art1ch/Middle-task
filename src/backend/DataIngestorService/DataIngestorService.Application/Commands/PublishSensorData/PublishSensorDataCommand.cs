using MediatR;
using Shared.Abstractions.Models;

namespace DataIngestorService.Application.Commands.PublishSensorData;

public sealed record PublishSensorsDataCommand(
    IEnumerable<SensorsDataItemModel> SensorsData
) : IRequest<PublishSensorsDataCommandResult>;
