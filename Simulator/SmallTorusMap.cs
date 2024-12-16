namespace Simulator.Maps;

/// <summary>
/// Small square torus map.
/// </summary>
public class SmallTorusMap : Map
{
    /// <summary>
    /// Size of the torus map.
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SmallTorusMap"/> class.
    /// </summary>
    /// <param name="size">Size of the map (must be between 5 and 20).</param>
    public SmallTorusMap(int size)
    {
        if (size < 5 || size > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "Size must be between 5 and 20.");
        }
        Size = size;
    }

    /// <inheritdoc />
    public override bool Exist(Point p)
    {
        return p.X >= 0 && p.X < Size && p.Y >= 0 && p.Y < Size;
    }

    /// <inheritdoc />
    public override Point Next(Point p, Direction d)
    {
        return d switch
        {
            Direction.Up => new Point(p.X, (p.Y + 1) % Size),
            Direction.Right => new Point((p.X + 1) % Size, p.Y),
            Direction.Down => new Point(p.X, (p.Y - 1 + Size) % Size),
            Direction.Left => new Point((p.X - 1 + Size) % Size, p.Y),
            _ => p
        };
    }

    /// <inheritdoc />
    public override Point NextDiagonal(Point p, Direction d)
    {
        return d switch
        {
            Direction.Up => new Point((p.X + 1) % Size, (p.Y + 1) % Size),           // 45° from Up
            Direction.Right => new Point((p.X + 1) % Size, (p.Y - 1 + Size) % Size), // 45° from Right
            Direction.Down => new Point((p.X - 1 + Size) % Size, (p.Y - 1 + Size) % Size), // 45° from Down
            Direction.Left => new Point((p.X - 1 + Size) % Size, (p.Y + 1) % Size), // 45° from Left
            _ => p
        };
    }
}