using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.Maps;

namespace Simulator
{
    public class Simulation
    {
        private int _currentTurnIndex = 0;

        /// <summary>
        /// Simulation's map.
        /// </summary>
        public Map Map { get; }

        /// <summary>
        /// Creatures moving on the map.
        /// </summary>
        public List<Creature> Creatures { get; }

        /// <summary>
        /// Starting positions of creatures.
        /// </summary>
        public List<Point> Positions { get; }

        /// <summary>
        /// Cyclic list of creatures moves. 
        /// Bad moves are ignored - use DirectionParser.
        /// First move is for first creature, second for second and so on.
        /// When all creatures make moves, 
        /// next move is again for first creature and so on.
        /// </summary>
        public string Moves { get; }

        /// <summary>
        /// Has all moves been done?
        /// </summary>
        public bool Finished { get; private set; } = false;

        /// <summary>
        /// Creature which will be moving current turn.
        /// </summary>
        public Creature CurrentCreature => Creatures[_currentTurnIndex % Creatures.Count];

        /// <summary>
        /// Lowercase name of direction which will be used in current turn.
        /// </summary>
        public string CurrentMoveName
        {
            get
            {
                var validMoves = DirectionParser.Parse(Moves);
                var currentMove = validMoves[_currentTurnIndex % validMoves.Length];
                return currentMove.ToString().ToLower();
            }
        }

        /// <summary>
        /// Simulation constructor.
        /// Throw errors:
        /// if creatures' list is empty,
        /// if number of creatures differs from 
        /// number of starting positions.
        /// </summary>
        public Simulation(Map map, List<Creature> creatures, List<Point> positions, string moves)
        {
            if (creatures == null || creatures.Count == 0)
            {
                throw new ArgumentException("Creatures list cannot be empty.");
            }

            if (positions == null || positions.Count != creatures.Count)
            {
                throw new ArgumentException("Number of starting positions must match the number of creatures.");
            }

            Map = map ?? throw new ArgumentNullException(nameof(map));
            Creatures = creatures;
            Positions = positions;
            Moves = moves ?? string.Empty; // Handle null input by assigning an empty string

            // Filter moves using DirectionParser to ignore bad moves
            var validMoves = DirectionParser.Parse(Moves);
            if (validMoves.Length == 0)
            {
                throw new ArgumentException("No valid moves found in the moves string.");
            }
        }

        /// <summary>
        /// Makes one move of current creature in current direction.
        /// Throw error if simulation is finished.
        /// </summary>
        public void Turn()
        {
            if (Finished)
            {
                throw new InvalidOperationException("Simulation is finished.");
            }

            // Get current creature and move
            var creature = CurrentCreature;
            var validMoves = DirectionParser.Parse(Moves);
            var currentMove = validMoves[_currentTurnIndex % validMoves.Length];

            // Get current position and calculate new position
            var currentPosition = Positions[_currentTurnIndex % Positions.Count];
            var newPosition = Map.Next(currentPosition, currentMove);

            // If new position exists on the map, update the position
            if (Map.Exist(newPosition))
            {
                Positions[_currentTurnIndex % Positions.Count] = newPosition;
                Console.WriteLine($"{creature.Name} moved {currentMove.ToString().ToLower()} to {newPosition}");
            }
            else
            {
                Console.WriteLine($"{creature.Name} tried to move {currentMove.ToString().ToLower()} but hit the edge.");
            }

            // Increment turn index
            _currentTurnIndex++;

            // Check if all moves are completed
            if (_currentTurnIndex >= validMoves.Length)
            {
                Finished = true;
            }
        }
    }
}