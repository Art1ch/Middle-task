using Shared.Abstractions.Models;

namespace DataProcessorService.Application.Responses;

public sealed record GetSensorsDataResponse(
    IEnumerable<SensorsDataItemModel> SensorsData    
);