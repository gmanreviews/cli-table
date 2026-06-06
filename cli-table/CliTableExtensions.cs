using Microsoft.Extensions.DependencyInjection;

namespace cli_table;

public static class CliTableExtensions
{
    public static IServiceCollection AddPrintServices(this IServiceCollection services)
    {
        services.AddSingleton<IWindowSpecifications, ConsoleWidthSpecifications>();
        services.AddSingleton<TablePrint>();
        return services;
    }
}