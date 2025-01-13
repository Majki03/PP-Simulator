namespace Simulator.Maps
{
    public class SmallTorusMap : Map
    {
        private readonly int _size;

        public SmallTorusMap(int size)
        {
            if (size < 5 || size > 20)
                throw new ArgumentOutOfRangeException(nameof(size), "Size must be between 5 and 20.");

            _size = size;
        }

        public override bool Exist(Point p) => true;

        public override Point Next(Point p, Direction d)
        {
            Point next = p.Next(d);
            return new Point(
                (next.X + _size) % _size, // Tworzenie nowego obiektu z poprawionymi wartościami
                (next.Y + _size) % _size
            );
        }

        public override Point NextDiagonal(Point p, Direction d)
        {
            Point next = p.NextDiagonal(d);
            return new Point(
                (next.X + _size) % _size, // Tworzenie nowego obiektu z poprawionymi wartościami
                (next.Y + _size) % _size
            );
        }


        public override int SizeX => _size;
        public override int SizeY => _size;
        public override int Size => _size; // Przesłania właściwość bazową
    }
}