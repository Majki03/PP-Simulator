using Simulator;

namespace Simulator.Maps

{
    public abstract class Map
    {
        public abstract bool Exist(Point p);
        public abstract Point Next(Point p, Direction d);
        public abstract Point NextDiagonal(Point p, Direction d);

        public abstract int SizeX { get; }
        public abstract int SizeY { get; }
        public virtual int Size => Math.Max(SizeX, SizeY); // Właściwość wirtualna
    }
}


namespace Simulator.Maps
{
    public class SmallSquareMap : Map
    {
        private readonly int _size;

        public SmallSquareMap(int size)
        {
            if (size < 5 || size > 20)
                throw new ArgumentOutOfRangeException(nameof(size), "Size must be between 5 and 20.");

            _size = size;
        }

        public override bool Exist(Point p) => p.X >= 0 && p.X < _size && p.Y >= 0 && p.Y < _size;

        public override Point Next(Point p, Direction d) => Exist(p.Next(d)) ? p.Next(d) : p;

        public override Point NextDiagonal(Point p, Direction d) => Exist(p.NextDiagonal(d)) ? p.NextDiagonal(d) : p;

        public override int SizeX => _size;
        public override int SizeY => _size;
        public override int Size => _size; // Przesłania właściwość bazową
    }
}