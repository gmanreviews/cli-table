using static cli_table.SizingStrategy;

namespace cli_table;

public class TablePrint(IWindowSpecifications specifications, SizingStrategy sizingStrategy)
{
    public void PrintData<T>(TableData<T> data) where T: class
    {
        if (data.Headers.Length == 0 || data.Data.Length == 0)
        {
            Console.WriteLine("There is no data");
            return;
        }

        var widths = CalculateWidths(data, sizingStrategy);

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
            CompactHeaderLength => HeaderLength(data),
            _ => Enumerable.Repeat(AverageColumnWidth(data), data.Headers.Length).ToArray(),
        };

    private int[] HeaderLength<T>(TableData<T> data) where T : class
    {
        var lengths = data.Headers.Select(h => h.Length).ToArray();
        var total = lengths.Sum();
        var diff = total - specifications.Width;

        if (diff <= 0)
        {
            return lengths;
        }

        //in this case the console is smaller than the headers text combined
        var perCol = diff / data.Headers.Length;
        var eachDiff = RoundingUp(perCol);
        return lengths.Select(l => l - eachDiff).ToArray();
    }
    
    private int AverageColumnWidth<T>(TableData<T> data) where T : class
    {
        var perCol = (specifications.Width - (data.Headers.Length + 1)) / data.Headers.Length;
        return RoundingUp(perCol);
    }

    private static int RoundingUp(double v) => Convert.ToInt32(Math.Round(v, MidpointRounding.AwayFromZero));

}

