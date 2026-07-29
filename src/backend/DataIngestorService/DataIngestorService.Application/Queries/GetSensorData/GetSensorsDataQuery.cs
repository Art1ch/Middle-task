using MediatR;

namespace DataIngestorService.Application.Queries.GetSensorData;

public sealed record GetSensorsDataQuery() : IRequest<GetSensorsDataQueryResult>;
