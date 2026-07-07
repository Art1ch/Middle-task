using MediatR;
using Shared.Abstractions.Models;

namespace DataProcessorService.Application.Queries.GetByFilterQuery;

public sealed record GetByFilterQueryResult(
    IEnumerable<SensorsDataItemModel> SensorsData
) : IRequest;