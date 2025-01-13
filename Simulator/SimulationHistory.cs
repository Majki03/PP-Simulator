using System;
using System.Collections.Generic;
using Simulator;
using Simulator.Maps;

namespace Simulator
{
    public class SimulationHistory
    {
        private Simulation _simulation { get; }
        public int SizeX { get; }
        public int SizeY { get; }
        public List<SimulationTurnLog> TurnLogs { get; } = new();

        // Constructor
        public SimulationHistory(Simulation simulation)
        {
            _simulation = simulation ?? throw new ArgumentNullException(nameof(simulation));
            SizeX = _simulation.Map.SizeX;
            SizeY = _simulation.Map.SizeY;

            // Store the initial positions as the first log
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

                // Handle overlapping creatures
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
                // Record the state before the turn
                RecordCurrentState();

                // Perform the next simulation turn
                _simulation.Turn();
            }

            // Record the final state after the last move
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

                // Handle overlapping creatures
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
}