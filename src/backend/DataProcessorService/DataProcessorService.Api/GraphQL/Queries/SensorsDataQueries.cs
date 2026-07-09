using DataProcessorService.Application.Queries.GetSensorsDataByFilterQuery;
using DataProcessorService.Application.Requests;
using DataProcessorService.Application.Responses;
using MediatR;

namespace DataProcessorService.Api.GraphQL.Queries;

public sealed class SensorsDataQueries
{
    public async Task<GetSensorsDataResponse> GetSensorsData(
        GetSensorsDataRequest request,
        [Service] ISender sender 
    )
    {
        var query = new GetSensorsDataByFilterQuery(
            request.Type,
            request.PlacementName,
            request.From,
            request.To,
            request.Page,
            request.PageSize
        );

        var queryResult = await sender.Send(query);

        var response = new GetSensorsDataResponse(
            queryResult.SensorsData
        );

        return response;
    }
}
