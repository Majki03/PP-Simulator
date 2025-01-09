using System;
using System.Collections.Generic;
using System.Linq;
using Simulator;
using Simulator.Maps;

public class MapVisualizer
{
    private readonly Simulation _simulation;
    private readonly Map _map;

    public MapVisualizer(Map map, List<Point> positions, List<Creature> creatures, Simulation simulation)
    {
        _simulation = simulation;
        _map = map;
    }

    public void Draw()
    {
        Console.Clear();
        int size = (_map as SmallSquareMap)?.Size ?? 0;

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Rysowanie górnej krawędzi
        Console.Write(Box.TopLeft);
        for (int i = 0; i < size - 1; i++)
            Console.Write($"{Box.Horizontal}{Box.TopMid}");
        Console.WriteLine(Box.TopRight);

        // Rysowanie mapy
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Console.Write(Box.Vertical);

                Point point = new(x, y);

                // Pobieranie stworów znajdujących się na danym punkcie
                var creaturesAtPoint = _simulation.Positions
                    .Select((p, i) => new { Position = p, Creature = _simulation.Creatures[i] })
                    .Where(pc => pc.Position.X == point.X && pc.Position.Y == point.Y)
                    .Select(pc => pc.Creature)
                    .ToList();

                // Wybór symbolu w zależności od liczby stworów
                if (creaturesAtPoint.Count > 1)
                    Console.Write("X");
                else if (creaturesAtPoint.Count == 1)
                    Console.Write(creaturesAtPoint[0] is Orc ? "O" : "E");
                else
                    Console.Write(" ");
            }
            Console.WriteLine(Box.Vertical);
        }

        // Rysowanie dolnej krawędzi
        Console.Write(Box.BottomLeft);
        for (int i = 0; i < size - 1; i++)
            Console.Write($"{Box.Horizontal}{Box.BottomMid}");
        Console.WriteLine(Box.BottomRight);
    }
}