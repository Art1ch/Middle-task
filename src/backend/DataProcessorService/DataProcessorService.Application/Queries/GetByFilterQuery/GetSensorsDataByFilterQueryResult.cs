using MediatR;
using Shared.Abstractions.Models;

namespace DataProcessorService.Application.Queries.GetSensorsDataByFilterQuery;

public sealed record GetSensorsDataByFilterQueryResult(
    IEnumerable<SensorsDataItemModel> SensorsData
) : IRequest;