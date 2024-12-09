namespace Simulator;

public static class Validator
{
    public static int Limiter(int value, int min, int max)
    {
        return Math.Clamp(value, min, max);
    }

    public static string Shortener(string value, int min, int max, char placeholder)
    {
        var trimmed = value.Trim();
        if (trimmed.Length > max)
        {
            trimmed = trimmed.Substring(0, max).TrimEnd();
        }
        if (trimmed.Length < min)
        {
            trimmed = trimmed.PadRight(min, placeholder);
        }
        return char.ToUpper(trimmed[0]) + trimmed.Substring(1);
    }
}