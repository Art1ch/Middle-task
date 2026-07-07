using MediatR;
using Shared.Abstractions.Models;

namespace DataIngestorService.Application.Queries.GetSensorData;

public sealed record GetSensorsDataQueryResult(
    IEnumerable<SensorsDataItemModel> SensorsData
) : IRequest;
