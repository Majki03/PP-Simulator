using Simulator;
using Simulator.Maps;
using Xunit;

namespace TestSimulator;

public class SmallTorusMapTests
{
    [Fact]
    public void Constructor_ValidSize_ShouldSetSize()
    {
        // Arrange
        int size = 10;
        // Act
        var map = new SmallTorusMap(size);
        // Assert
        Assert.Equal(size, map.Size);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(21)]
    public void 
        Constructor_InvalidSize_ShouldThrowArgumentOutOfRangeException
        (int size)
    {
        // Act & Assert
        // The way to check if method throws anticipated exception:
        Assert.Throws<ArgumentOutOfRangeException>(() =>
             new SmallTorusMap(size));
    }

    [Theory]
    [InlineData(3, 4, 5, true)]
    [InlineData(6, 1, 5, false)]
    [InlineData(19, 19, 20, true)]
    [InlineData(20, 20, 20, false)]
    public void Exist_ShouldReturnCorrectValue(int x, int y, 
        int size, bool expected)
    {
        // Arrange
        var map = new SmallTorusMap(size);
        var point = new Point(x, y);
        // Act
        var result = map.Exist(point);
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(5, 10, Direction.Up, 5, 11)]
    [InlineData(0, 0, Direction.Down, 0, 19)]
    [InlineData(0, 8, Direction.Left, 19, 8)] 
    [InlineData(19, 10, Direction.Right, 0, 10)] 
    public void Next_ShouldReturnCorrectNextPoint(int x, int y, 
        Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var map = new SmallTorusMap(20);
        var point = new Point(x, y);
        // Act
        var nextPoint = map.Next(point, direction);
        // Assert
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }

    [Theory]
    [InlineData(5, 10, Direction.Up, 6, 11)]
    [InlineData(0, 0, Direction.Down, 19, 19)]
    [InlineData(0, 8, Direction.Left, 19, 9)]
    [InlineData(19, 10, Direction.Right, 0, 9)] 
    public void NextDiagonal_ShouldReturnCorrectNextPoint(int x, int y, 
        Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var map = new SmallTorusMap(20);
        var point = new Point(x, y);
        // Act
        var nextPoint = map.NextDiagonal(point, direction);
        // Assert
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }
}

public class SmallSquareMapTests
{
    [Theory]
    [InlineData(5, true)]  // Minimalny rozmiar
    [InlineData(20, true)] // Maksymalny rozmiar
    [InlineData(4, false)] // Za mały
    [InlineData(21, false)]// Za duży
    public void Constructor_Size_ShouldValidateSize(int size, bool isValid)
    {
        if (isValid)
        {
            var map = new SmallSquareMap(size);
            Assert.Equal(size, map.Size);
        }
        else
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new SmallSquareMap(size));
        }
    }

    [Theory]
    [InlineData(0, 0, 5, true)]
    [InlineData(4, 4, 5, true)]
    [InlineData(5, 5, 5, false)] // Poza mapą
    public void Exist_ShouldCheckIfPointExists(int x, int y, int size, bool expected)
    {
        var map = new SmallSquareMap(size);
        var point = new Point(x, y);
        Assert.Equal(expected, map.Exist(point));
    }
}

public class PointTests
{
    [Fact]
    public void Constructor_ShouldSetCoordinates()
    {
        var point = new Point(3, 4);
        Assert.Equal(3, point.X);
        Assert.Equal(4, point.Y);
    }

    [Theory]
    [InlineData(0, 0, Direction.Up, 0, 1)]
    [InlineData(0, 0, Direction.Right, 1, 0)]
    [InlineData(0, 0, Direction.Down, 0, -1)]
    [InlineData(0, 0, Direction.Left, -1, 0)]
    public void Next_ShouldMoveInCorrectDirection(int x, int y, Direction direction, int expectedX, int expectedY)
    {
        var point = new Point(x, y);
        var next = point.Next(direction);
        Assert.Equal(new Point(expectedX, expectedY), next);
    }
}

public class RectangleTests
{
    [Theory]
    [InlineData(0, 0, 5, 5)]
    [InlineData(2, 2, 6, 6)]
    public void Constructor_ShouldSetCoordinates(int x1, int y1, int x2, int y2)
    {
        var rect = new Rectangle(x1, y1, x2, y2);
        Assert.Equal(x1, rect.X1);
        Assert.Equal(y1, rect.Y1);
        Assert.Equal(x2, rect.X2);
        Assert.Equal(y2, rect.Y2);
    }

    [Fact]
    public void Constructor_CollinearPoints_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => new Rectangle(0, 0, 0, 5));
    }

    [Theory]
    [InlineData(1, 1, true)]
    [InlineData(6, 6, false)]
    public void Contains_ShouldCheckIfPointIsInsideRectangle(int px, int py, bool expected)
    {
        var rect = new Rectangle(0, 0, 5, 5);
        var point = new Point(px, py);
        Assert.Equal(expected, rect.Contains(point));
    }
}

public class ValidatorTests
{
    [Theory]
    [InlineData(5, 1, 10, 5)]  // W zakresie
    [InlineData(-5, 1, 10, 1)] // Za małe
    [InlineData(15, 1, 10, 10)]// Za duże
    public void Limiter_ShouldLimitValue(int value, int min, int max, int expected)
    {
        var result = Validator.Limiter(value, min, max);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("   abc   ", 3, 10, '#', "abc")]       // Poprawna długość
    [InlineData("a", 3, 5, '#', "a##")]               // Zbyt krótki
    [InlineData("abcdefghijklmnop", 3, 10, '#', "abcdefghi")] // Zbyt długi
    public void Shortener_ShouldAdjustStringLength(string input, int min, int max, char placeholder, string expected)
    {
        var result = Validator.Shortener(input, min, max, placeholder);
        Assert.Equal(expected, result);
    }
}