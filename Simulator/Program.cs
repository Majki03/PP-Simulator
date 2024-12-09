using System;

namespace Simulator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting Simulator!\n");
            Lab3a();
        }

        private static void Lab3a()
        {
            Creature c = new("Shrek", 7);
            c.SayHi();

            Console.WriteLine("\n* Up");
            c.Go(Direction.Up);

            Console.WriteLine("\n* Right, Left, Left, Down");
            Direction[] directions = { 
                Direction.Right, Direction.Left, Direction.Left, Direction.Down
            };
            c.Go(directions);

            Console.WriteLine("\n* LRL");
            c.Go("LRL");
    
            Console.WriteLine("\n* xxxdR lyyLTyu");
            c.Go("xxxdR lyyLTyu");
        }
    }
}