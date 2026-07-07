using MediatR;

namespace DataProcessorService.Application.Queries.GetByFilterQuery;

public sealed record GetByFilterQuery(
    string Type,
    string PlacementName,
    DateTime? From,
    DateTime? To,
    int? Page,
    int? PageSize
) : IRequest<GetByFilterQueryResult>;