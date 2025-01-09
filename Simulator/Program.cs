using System;
using System.Collections.Generic;
using Simulator.Maps;

namespace Simulator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RunSimulation();
        }

        private static void RunSimulation()
        {
            try
            {
                var map = new SmallSquareMap(10);
                var creatures = new List<Creature>
                {
                    new Elf("Legolas"),
                    new Orc("Gorbag")
                };
                var positions = new List<Point>
                {
                    new Point(0, 0),
                    new Point(9, 9)
                };
                var moves = "URDL";

                var simulation = new Simulation(map, creatures, positions, moves);

                while (!simulation.Finished)
                {
                    simulation.Turn();
                }

                Console.WriteLine("Simulation completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}