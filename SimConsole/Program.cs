using System;
using System.Collections.Generic;
using System.Threading;
using Simulator;
using Simulator.Maps;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Inicjalizacja mapy, stworów, pozycji i ruchów
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
            MapVisualizer mapVisualizer = new(simulation.Map, simulation.Positions, simulation.Creatures, simulation);

            while (!simulation.Finished)
            {
                // Wyczyszczenie konsoli przed rysowaniem mapy
                Console.Clear();

                // Rysowanie aktualnego stanu mapy
                mapVisualizer.Draw();

                // Wyświetlenie informacji o obecnym ruchu i stworze
                Console.WriteLine($"Current Turn: {simulation.CurrentMoveName}");
                Console.WriteLine($"Current Creature: {simulation.CurrentCreature.Name}");
                Console.WriteLine($"Creature Position: {simulation.Positions[simulation.Creatures.IndexOf(simulation.CurrentCreature)]}");

                // Próba wykonania ruchu
                try
                {
                    simulation.Turn();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during turn: {ex.Message}");
                    break; // Przerwij symulację w przypadku błędu
                }

                // Pauza dla wizualizacji
                Thread.Sleep(500);
            }

            // Ostateczne rysowanie mapy i zakończenie symulacji
            Console.WriteLine("Simulation completed successfully!");
            mapVisualizer.Draw();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}