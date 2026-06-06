namespace cli_table;

/// <summary>
/// This interface defines how the printer will know
/// how many characters it can print on the screen
/// in the cli it is working with
/// </summary>
public interface IWindowSpecifications
{
    /// <summary>
    /// This represents how many characters can fit within a line in the cli
    /// </summary>
    public int Width { get; }
    
    /// <summary>
    /// this represents how many lines can be printed in the cli (before paging)
    /// </summary>
    public int Height { get; }
}