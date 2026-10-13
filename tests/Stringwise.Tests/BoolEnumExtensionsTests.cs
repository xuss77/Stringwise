namespace Stringwise.Tests;

public class BoolEnumExtensionsTests
{
    public enum Color
    {
        Red = 1,
        Green = 2,
        Blue = 3,
    }

    [Flags]
    public enum Access
    {
        None = 0,
        Read = 1,
        Write = 2,
    }

    [Theory]
    [InlineData("true")]
    [InlineData("TRUE")]
    [InlineData("True")]
    [InlineData("yes")]
    [InlineData("Y")]
    [InlineData("on")]
    [InlineData("1")]
    [InlineData("t")]
    [InlineData("  yes  ")]
    public void ToBool_RecognizesTruthyValues(string input)
    {
        Assert.True(input.ToBool());
        Assert.True(input.ToBoolOrNull());
    }

    [Theory]
    [InlineData("false")]
    [InlineData("FALSE")]
    [InlineData("no")]
    [InlineData("N")]
    [InlineData("off")]
    [InlineData("0")]
    [InlineData("f")]
    public void ToBool_RecognizesFalsyValues(string input)
    {
        Assert.False(input.ToBool());
        Assert.False(input.ToBoolOrNull());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("maybe")]
    [InlineData("2")]
    [InlineData("yess")]
    [InlineData(null)]
    public void ToBoolOrNull_ReturnsNullForUnrecognized(string? input)
    {
        Assert.Null(input.ToBoolOrNull());
        Assert.True(input.ToBoolOrDefault(true));
        Assert.False(input.ToBoolOrDefault());
    }

    [Fact]
    public void ToBool_Throws_ForInvalidInput()
    {
        Assert.Throws<FormatException>(() => "maybe".ToBool());
        Assert.Throws<ArgumentNullException>(() => ((string?)null).ToBool());
    }

    [Theory]
    [InlineData("Red", Color.Red)]
    [InlineData("red", Color.Red)]
    [InlineData(" BLUE ", Color.Blue)]
    [InlineData("2", Color.Green)]
    public void ToEnum_ParsesNamesAndDefinedNumbers(string input, Color expected)
    {
        Assert.Equal(expected, input.ToEnum<Color>());
        Assert.Equal(expected, input.ToEnumOrNull<Color>());
    }

    [Theory]
    [InlineData("Purple")]
    [InlineData("42")]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void ToEnumOrNull_RejectsUndefinedValues(string? input)
    {
        Assert.Null(input.ToEnumOrNull<Color>());
        Assert.Equal(Color.Green, input.ToEnumOrDefault(Color.Green));
    }

    [Fact]
    public void ToEnum_CaseSensitive_WhenRequested()
    {
        Assert.Null("red".ToEnumOrNull<Color>(ignoreCase: false));
        Assert.Equal(Color.Red, "Red".ToEnumOrNull<Color>(ignoreCase: false));
    }

    [Fact]
    public void ToEnum_SupportsFlagCombinations()
        => Assert.Equal(Access.Read | Access.Write, "Read, Write".ToEnum<Access>());

    [Fact]
    public void ToEnum_Throws_ForUndefinedValue()
    {
        var ex = Assert.Throws<FormatException>(() => "Purple".ToEnum<Color>());
        Assert.Contains("Color", ex.Message, StringComparison.Ordinal);
    }
}
