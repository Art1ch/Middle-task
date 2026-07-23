using DataProcessorService.Application.Queries.GetSensorsDataByFilterQuery;
using DataProcessorService.Application.Requests;
using DataProcessorService.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataProcessorService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class SensorDataController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<SensorDataController> _logger;

    public SensorDataController(ILogger<SensorDataController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet("data")]
    public async Task<ActionResult<GetSensorsDataResponse>> GetSensorsData([FromQuery] GetSensorsDataRequest request)
    {
        var query = new GetSensorsDataByFilterQuery(
            request.Type,
            request.PlacementName,
            request.From,
            request.To,
            request.Page,
            request.PageSize
        );

        var queryResult = await _sender.Send(query);

        var response = new GetSensorsDataResponse(
            queryResult.SensorsData
        );

        return Ok(response);
    }
}
