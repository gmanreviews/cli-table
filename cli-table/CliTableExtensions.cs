using Microsoft.Extensions.DependencyInjection;

namespace cli_table;

public static class CliTableExtensions
{
    public static void AddPrintServices(this IServiceCollection services)
    {
        services.AddSingleton<IWindowSpecifications, ConsoleWidthSpecifications>();
        services.AddSingleton<TablePrint>();
    }
}