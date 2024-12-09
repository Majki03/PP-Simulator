using System;

namespace Simulator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting Simulator!\n");
            Lab4b();
        }

        private static void Lab4b()
        {
            object[] myObjects = {
                new Animals() { Description = "dogs"},
                new Birds { Description = "  eagles ", Size = 10 },
                new Elf("e", 15, -3),
                new Orc("morgash", 6, 4)
            };
            Console.WriteLine("\nMy objects:");
            foreach (var o in myObjects) Console.WriteLine(o);
        }
    }
}