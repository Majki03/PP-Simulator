using System;
using System.Collections.Generic;
using Simulator;

namespace Simulator;

public class SimulationHistory
{
    private readonly Simulation _simulation;
    public int SizeX { get; }
    public int SizeY { get; }
    public List<SimulationTurnLog> TurnLogs { get; } = new();

    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation ?? throw new ArgumentNullException(nameof(simulation));
        SizeX = _simulation.Map.SizeX;
        SizeY = _simulation.Map.SizeY;
        StoreInitialState();
        Run();
    }

    private void StoreInitialState()
    {
        var initialSymbols = new Dictionary<Point, char>();
        for (int i = 0; i < _simulation.Creatures.Count; i++)
        {
            var position = _simulation.Positions[i];
            var creature = _simulation.Creatures[i];
            char symbol = creature is Elf ? 'E' : 'O';

            if (initialSymbols.ContainsKey(position))
                initialSymbols[position] = 'X';
            else
                initialSymbols[position] = symbol;
        }

        TurnLogs.Add(new SimulationTurnLog
        {
            Mappable = "Initial State",
            Move = "None",
            Symbols = initialSymbols
        });
    }

    private void Run()
    {
        while (!_simulation.Finished)
        {
            RecordCurrentState();
            _simulation.Turn();
        }
        RecordCurrentState();
    }

    private void RecordCurrentState()
    {
        var currentSymbols = new Dictionary<Point, char>();
        for (int i = 0; i < _simulation.Creatures.Count; i++)
        {
            var position = _simulation.Positions[i];
            var creature = _simulation.Creatures[i];
            char symbol = creature is Elf ? 'E' : 'O';

            if (currentSymbols.ContainsKey(position))
                currentSymbols[position] = 'X';
            else
                currentSymbols[position] = symbol;
        }

        TurnLogs.Add(new SimulationTurnLog
        {
            Mappable = _simulation.CurrentCreature.ToString(),
            Move = _simulation.CurrentMoveName,
            Symbols = currentSymbols
        });
    }
}