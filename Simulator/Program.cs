using System;
using System.Collections.Generic;
using Simulator;
using Simulator.Maps;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            // Przygotowanie symulacji
            SmallSquareMap map = new(5);
            List<Creature> creatures = new()
            {
                new Orc("Gorbag"),
                new Elf("Elandor")
            };
            List<Point> points = new()
            {
                new Point(2, 2),
                new Point(3, 1)
            };
            string moves = "dlrludl";

            Simulation simulation = new(map, creatures, points, moves);
            SimulationHistory simulationHistory = new(simulation);

            // Uruchomienie symulacji
            while (!simulation.Finished)
            {
                simulation.Turn();
            }

            // Wyświetlenie zapisanej historii symulacji
            Console.WriteLine("Simulation History:");
            foreach (var turnLog in simulationHistory.TurnLogs)
            {
                Console.WriteLine($"Turn: {turnLog.Mappable} moved {turnLog.Move}");
                foreach (var entry in turnLog.Symbols)
                {
                    Console.WriteLine($"  Position {entry.Key}: {entry.Value}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}