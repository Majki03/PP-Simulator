namespace Simulator;

using System;

public class Creature
{
    private string _name = "Unknown";
    private int _level = 1;

    public string Name
    {
        get => _name;
        set
        {
            if (_name != "Unknown") return; // Można ustawić tylko raz
            var trimmed = value.Trim();
            if (trimmed.Length < 3) trimmed = trimmed.PadRight(3, '#');
            if (trimmed.Length > 25) trimmed = trimmed.Substring(0, 25).TrimEnd();
            if (trimmed.Length < 3) trimmed = trimmed.PadRight(3, '#');
            _name = char.ToUpper(trimmed[0]) + trimmed.Substring(1);
        }
    }

    public int Level
    {
        get => _level;
        set
        {
            if (_level != 1) return; // Można ustawić tylko raz
            _level = Math.Clamp(value, 1, 10);
        }
    }

    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature()
    {
    }

    public string Info => $"{Name}, Level {Level}";

    public void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name} at Level {Level}!");
    }

    public void Upgrade()
    {
        if (Level < 10)
            Level++;
    }

    public void Go(Direction direction)
    {
        Console.WriteLine($"{Name} goes {direction.ToString().ToLower()}.");
    }

    public void Go(Direction[] directions)
    {
        foreach (var direction in directions)
        {
            Go(direction);
        }
    }

    public void Go(string directionString)
    {
        var directions = DirectionParser.Parse(directionString);
        Go(directions);
    }
}