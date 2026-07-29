using DataIngestorService.Infrastructure.Models;
using Mapster;
using Shared.Abstractions.Models;

namespace DataIngestorService.Infrastructure.Mapping;

public sealed class MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SensorDataItemJsonModel, SensorsDataItemModel>()
            .Map(dest => dest.DataType, src => src.Type)
            .Map(dest => dest.PlacementName, src => src.Name)
            .Map(dest => dest.Timestamp, src => DateTime.UtcNow)
            .Map(dest => dest.Payload, src => src.Payload);
    }
}
