using static cli_table.SizingStrategy;

namespace cli_table;

public class TablePrint(IWindowSpecifications specifications)
{
    public void PrintData<T>(TableData<T> data) where T: class
    {
        if (data.Headers.Length == 0 || data.Data.Length == 0)
        {
            Console.WriteLine("There is no data");
            return;
        }

        var widths = CalculateWidths(data, Default);

        var lines = Math.Min(specifications.Height, data.Data.Length);
        for (var i = -1; i < lines; i++)
        {
            var t = data.GetDataString(i, widths);
            Console.WriteLine(t);
        }
    }

    private int[] CalculateWidths<T>(TableData<T> data, SizingStrategy strategy) where T : class
        => strategy switch
        {
            //with the default strategy we assume we want each column to have equal length regardless of content
            _ => Enumerable.Repeat(AverageColumnWidth(data), data.Headers.Length).ToArray(),
        };

    private int AverageColumnWidth<T>(TableData<T> data) where T : class
    {
        var perCol = (specifications.Width - (data.Headers.Length + 1)) / data.Headers.Length;
        return Convert.ToInt32(Math.Round((double)perCol, MidpointRounding.AwayFromZero));
    }

}

