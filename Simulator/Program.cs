using System;

namespace Simulator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting Simulator!\n");
            Lab4a();
        }

        private static void Lab4a()
        {
            Creature c = new Elf("Elandor", 5, 3);
            Console.WriteLine(c);  // ELF: Elandor [5][3]
        }
    }
}
