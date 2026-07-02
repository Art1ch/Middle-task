using DataIngestorService.Core.Models;
using MediatR;

namespace DataIngestorService.Application.Queries.GetSensorData;

public sealed record GetSensorDataQueryResult(
    IEnumerable<SensorDataItemModel> SensorData
) : IRequest;
