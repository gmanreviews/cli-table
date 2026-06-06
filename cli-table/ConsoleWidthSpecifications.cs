namespace cli_table;

public class ConsoleWidthSpecifications : IWindowSpecifications
{
    public int Width => Console.WindowWidth;
    public int Height => Console.WindowHeight;
}