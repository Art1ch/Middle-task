namespace DataProcessorService.Application.Filters;

public sealed record SensorDataFilter(  
    string? Type,
    string? PlacementName,
    int Page,
    int PageSize,
    DateTime? From,
    DateTime? To
);