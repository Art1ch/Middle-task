using MediatR;

namespace DataIngestorService.Application.Queries.GetSensorData;

public sealed record GetSensorDataQuery() : IRequest<GetSensorDataQueryResult>;
