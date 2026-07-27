using Microsoft.Extensions.DependencyInjection;

namespace cli_table;

public static class CliTableExtensions
{
    public static void AddPrintServices(this IServiceCollection services) => services.AddPrintServices(null);
    
    public static void AddPrintServices(this IServiceCollection services, Action<TablePrint>? configureTablePrint)
    {
        services.AddSingleton<IWindowSpecifications, ConsoleWidthSpecifications>();
        services.AddSingleton<TablePrint>(sp =>
        {
            var windowSpec = sp.GetRequiredService<IWindowSpecifications>();
            return new TablePrint(windowSpec, SizingStrategy.CompactHeaderLength);
        });
    }
}