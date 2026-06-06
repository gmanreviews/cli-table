using System.Text;
using JetBrains.Annotations;

namespace cli_table;

[UsedImplicitly]
public record TableData<T> where T: class
{
    public required string[] Headers { get; init; }
    public required T[] Data { get; init; }

    private T GetData(int index) => Data[index];

    public string GetDataString(int index, int[] widths)
    {
        var s = new StringBuilder();
        
        for (var i = 0; i < Headers.Length; i++)
        {
            var val = 
                index < 0 
                ? PrintVal(Headers[i], widths[i])
                : GetDataString(index, i, widths[i]);
            s.Append(val);
            s.Append('|');
        }
        return s.ToString();
    }
    
    private string GetDataString(int index, int headerIndex, int width)
    {
        
        
        var data = GetData(index);
        var headerName = Headers[headerIndex];
        var type = typeof(T);
        var propertyInfo = type.GetProperty(headerName);
        var value = propertyInfo?.GetValue(data);
        var valStr = value?.ToString() ?? string.Empty;
        return PrintVal(valStr, width);
    }

    private static string PrintVal(string val, int width)
    {
        if (width < val.Length)
        {
            return val[..width];
        }

        if (width > val.Length)
        {
            //pad with whitespace
            return val.PadRight(width);
        }
        
        return val;
    }
}