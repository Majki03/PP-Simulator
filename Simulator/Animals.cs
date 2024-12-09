namespace Simulator;

public class Animals
{
    private string _description = "";

    public string Description
    {
        get => _description;
        init
        {
            var trimmed = value.Trim();
            if (trimmed.Length < 3) trimmed = trimmed.PadRight(3, '#');
            if (trimmed.Length > 15) trimmed = trimmed.Substring(0, 15).TrimEnd();
            if (trimmed.Length < 3) trimmed = trimmed.PadRight(3, '#');
            _description = char.ToUpper(trimmed[0]) + trimmed.Substring(1);
        }
    }

    public uint Size { get; set; } = 3;

    public string Info => $"{Description} <{Size}>";
}