using System;
using Simulator.Maps;

namespace Simulator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting Simulator!\n");
            Lab5b();
        }

        private static void Lab5b()
        {
            try
            {
                var map = new SmallSquareMap(10);
                Console.WriteLine($"Map created with size {map.Size}.");

                var point1 = new Point(5, 5);
                var point2 = new Point(10, 10);

                Console.WriteLine($"Point {point1} exists: {map.Exist(point1)}");
                Console.WriteLine($"Point {point2} exists: {map.Exist(point2)}");

                var nextPoint = map.Next(point1, Direction.Right);
                Console.WriteLine($"Next point from {point1} to the Right: {nextPoint}");

                var nextDiagonalPoint = map.NextDiagonal(point1, Direction.Up);
                Console.WriteLine($"Next diagonal point from {point1} to the Up: {nextDiagonalPoint}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }
    }
}