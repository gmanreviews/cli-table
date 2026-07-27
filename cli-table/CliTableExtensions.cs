using Microsoft.Extensions.DependencyInjection;

namespace cli_table;

public static class CliTableExtensions
{
    extension(IServiceCollection services)
    {
        public void AddPrintServices() => services.AddPrintServices(null);

        public void AddPrintServices(Action<TablePrint>? configureTablePrint)
        {
            services.AddSingleton<IWindowSpecifications, ConsoleWidthSpecifications>();
            services.AddSingleton<TablePrint>(sp =>
            {
                var windowSpec = sp.GetRequiredService<IWindowSpecifications>();
                return new TablePrint(windowSpec, SizingStrategy.CompactHeaderLength);
            });
        }
    }
}