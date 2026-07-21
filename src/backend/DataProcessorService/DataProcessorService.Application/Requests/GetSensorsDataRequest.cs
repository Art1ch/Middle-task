namespace DataProcessorService.Application.Requests;

public sealed record GetSensorsDataRequest(
    string Type,
    string PlacementName,
    DateTime? From,
    DateTime? To,
    int Page,
    int PageSize
);