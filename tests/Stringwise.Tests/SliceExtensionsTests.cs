namespace Stringwise.Tests;

public class SliceExtensionsTests
{
    [Theory]
    [InlineData("Hello", 3, "Hel")]
    [InlineData("Hello", 5, "Hello")]
    [InlineData("Hello", 50, "Hello")]
    [InlineData("Hello", 0, "")]
    [InlineData("", 3, "")]
    [InlineData(null, 3, null)]
    public void Left(string? input, int length, string? expected)
        => Assert.Equal(expected, input.Left(length));

    [Theory]
    [InlineData("Hello", 3, "llo")]
    [InlineData("Hello", 5, "Hello")]
    [InlineData("Hello", 50, "Hello")]
    [InlineData("Hello", 0, "")]
    [InlineData(null, 3, null)]
    public void Right(string? input, int length, string? expected)
        => Assert.Equal(expected, input.Right(length));

    [Fact]
    public void LeftRight_ThrowOnNegativeLength()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => "abc".Left(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => "abc".Right(-1));
    }

    [Theory]
    [InlineData("key=value=x", "=", "key", "key=value", "value=x", "x")]
    [InlineData("a::b::c", "::", "a", "a::b", "b::c", "c")]
    [InlineData("no delimiter", "=", "", "", "", "")]
    [InlineData("=start", "=", "", "", "start", "start")]
    [InlineData("end=", "=", "end", "end", "", "")]
    public void BeforeAfter(string input, string delimiter, string before, string beforeLast, string after, string afterLast)
    {
        Assert.Equal(before, input.Before(delimiter));
        Assert.Equal(beforeLast, input.BeforeLast(delimiter));
        Assert.Equal(after, input.After(delimiter));
        Assert.Equal(afterLast, input.AfterLast(delimiter));
    }

    [Fact]
    public void BeforeAfter_NullInput_ReturnsNull()
    {
        string? input = null;

        Assert.Null(input.Before("="));
        Assert.Null(input.BeforeLast("="));
        Assert.Null(input.After("="));
        Assert.Null(input.AfterLast("="));
        Assert.Null(input.Between("[", "]"));
    }

    [Fact]
    public void BeforeAfter_RespectComparison()
    {
        Assert.Equal("", "fooBARbaz".After("bar"));
        Assert.Equal("baz", "fooBARbaz".After("bar", StringComparison.OrdinalIgnoreCase));
        Assert.Equal("foo", "fooBARbaz".Before("bar", StringComparison.OrdinalIgnoreCase));
    }

    [Theory]
    [InlineData("a[b]c", "[", "]", "b")]
    [InlineData("[first] and [second]", "[", "]", "first")]
    [InlineData("<<x>>", "<<", ">>", "x")]
    [InlineData("[]", "[", "]", "")]
    [InlineData("no brackets", "[", "]", "")]
    [InlineData("[unclosed", "[", "]", "")]
    [InlineData("]backwards[", "[", "]", "")]
    [InlineData("\"quoted\"", "\"", "\"", "quoted")]
    public void Between(string input, string start, string end, string expected)
        => Assert.Equal(expected, input.Between(start, end));
}
