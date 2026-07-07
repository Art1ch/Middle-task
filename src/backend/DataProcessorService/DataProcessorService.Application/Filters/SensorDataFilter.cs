namespace DataProcessorService.Application.Filters;

public sealed record SensorDataFilter(  
    string Type,
    string PlacementName,
    DateTime? From,
    DateTime? To,
    int? Page,
    int? PageSize
);