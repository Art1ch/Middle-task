using MediatR;

namespace DataProcessorService.Application.Queries.GetSensorsDataByFilterQuery;

public sealed record GetSensorsDataByFilterQuery(
    string? Type,
    string? PlacementName,
    DateTime? From,
    DateTime? To,
    int Page,
    int PageSize
) : IRequest<GetSensorsDataByFilterQueryResult>;