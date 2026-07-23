using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Implementations.PipelineBehaviors;

namespace Shared.Implementations;

public static class Injection
{
    public static IServiceCollection AddSharedImplementations(this IServiceCollection services)
    {
        return services
            .AddPipelineBehaviors();
    }

    private static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
    {
        return services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }
}
