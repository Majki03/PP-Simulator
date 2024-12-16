using System;

namespace Simulator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting Simulator!\n");
            Lab5a();
        }

        private static void Lab5a()
        {
            try
            {
                var rect1 = new Rectangle(2, 3, 5, 7);
                Console.WriteLine(rect1);

                var rect2 = new Rectangle(new Point(10, 15), new Point(5, 20));
                Console.WriteLine(rect2);

                var rect3 = new Rectangle(5, 5, 5, 10); // Exception expected
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }

            var rect = new Rectangle(0, 0, 10, 10);
            var insidePoint = new Point(5, 5);
            var outsidePoint = new Point(15, 15);

            Console.WriteLine($"Rectangle: {rect}");
            Console.WriteLine($"Contains {insidePoint}: {rect.Contains(insidePoint)}");
            Console.WriteLine($"Contains {outsidePoint}: {rect.Contains(outsidePoint)}");
        }
    }
}