namespace Simulator;

/// <summary>
/// Represents a point on the map with X and Y coordinates.
/// </summary>
public readonly struct Point
{
    public readonly int X, Y;

    /// <summary>
    /// Initializes a new instance of the Point struct.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    public Point(int x, int y) => (X, Y) = (x, y);

    /// <summary>
    /// Provides a string representation of the point.
    /// </summary>
    /// <returns>A string in the format "(X, Y)".</returns>
    public override string ToString() => $"({X}, {Y})";

    /// <summary>
    /// Returns the next point in the given direction.
    /// </summary>
    /// <param name="direction">The direction to move.</param>
    /// <returns>The new point after moving in the specified direction.</returns>
    public Point Next(Direction direction)
    {
        return direction switch
        {
            Direction.Up => new Point(X, Y + 1),
            Direction.Right => new Point(X + 1, Y),
            Direction.Down => new Point(X, Y - 1),
            Direction.Left => new Point(X - 1, Y),
            _ => this // If the direction is invalid, return the same point.
        };
    }

    /// <summary>
    /// Returns the next diagonal point based on the given direction.
    /// Diagonal directions assume a 45-degree rotation.
    /// </summary>
    /// <param name="direction">The direction to move diagonally.</param>
    /// <returns>The new point after moving diagonally in the specified direction.</returns>
    public Point NextDiagonal(Direction direction)
    {
        return direction switch
        {
            Direction.Up => new Point(X + 1, Y - 1), // Move up-right diagonally.
            Direction.Right => new Point(X + 1, Y + 1), // Move down-right diagonally.
            Direction.Down => new Point(X - 1, Y + 1), // Move down-left diagonally.
            Direction.Left => new Point(X - 1, Y - 1), // Move up-left diagonally.
            _ => this // If the direction is invalid, return the same point.
        };
    }

    /// <summary>
    /// Checks if this point is equal to another point.
    /// </summary>
    /// <param name="obj">The other object to compare.</param>
    /// <returns>True if the points are equal, otherwise false.</re
}