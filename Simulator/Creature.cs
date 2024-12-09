namespace Simulator;

using System;

public abstract class Creature
{
    private string _name = "Unknown";
    private int _level = 1;

    public string Name
    {
        get => _name;
        set
        {
            if (_name != "Unknown") return;
            _name = Validator.Shortener(value, 3, 25, '#');
        }
    }

    public int Level
    {
        get => _level;
        set
        {
            if (_level != 1) return;
            _level = Validator.Limiter(value, 1, 10);
        }
    }

    protected Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    protected Creature()
    {
    }

    public abstract void SayHi();

    public abstract int Power { get; }

    public abstract string Info { get; }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}

public class Elf : Creature
{
    private int _agility;
    private int _singCount;

    public int Agility
    {
        get => _agility;
        private set => _agility = Validator.Limiter(value, 0, 10);
    }

    public Elf(string name, int level = 1, int agility = 0) : base(name, level)
    {
        Agility = agility;
    }

    public Elf()
    {
    }

    public override void SayHi()
    {
        Console.WriteLine($"I'm {Name}, an Elf at Level {Level} with {Agility} agility.");
    }

    public void Sing()
    {
        _singCount++;
        if (_singCount % 3 == 0)
        {
            Agility++;
        }
    }

    public override int Power => (Level * 8) + (Agility * 2);

    public override string Info => $"{Name} [{Level}][{Agility}]";
}

public class Orc : Creature
{
    private int _rage;
    private int _huntCount;

    public int Rage
    {
        get => _rage;
        private set => _rage = Validator.Limiter(value, 0, 10);
    }

    public Orc(string name, int level = 1, int rage = 0) : base(name, level)
    {
        Rage = rage;
    }

    public Orc()
    {
    }

    public override void SayHi()
    {
        Console.WriteLine($"I'm {Name}, an Orc at Level {Level} with {Rage} rage.");
    }

    public void Hunt()
    {
        _huntCount++;
        if (_huntCount % 2 == 0)
        {
            Rage++;
        }
    }

    public override int Power => (Level * 7) + (Rage * 3);

    public override string Info => $"{Name} [{Level}][{Rage}]";
}