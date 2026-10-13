namespace Stringwise.Tests;

public class CheckExtensionsTests
{
    [Theory]
    [InlineData(null, true, true, false)]
    [InlineData("", true, true, false)]
    [InlineData("   ", false, true, false)]
    [InlineData("\t\n", false, true, false)]
    [InlineData("a", false, false, true)]
    [InlineData(" a ", false, false, true)]
    public void EmptinessChecks(string? input, bool nullOrEmpty, bool nullOrWhiteSpace, bool hasValue)
    {
        Assert.Equal(nullOrEmpty, input.IsNullOrEmpty());
        Assert.Equal(nullOrWhiteSpace, input.IsNullOrWhiteSpace());
        Assert.Equal(hasValue, input.HasValue());
    }

    [Theory]
    [InlineData("0123456789", true)]
    [InlineData("7", true)]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("12a", false)]
    [InlineData("-12", false)]
    [InlineData("1.2", false)]
    [InlineData(" 12", false)]
    [InlineData("١٢٣", false)] // Arabic-Indic digits are not ASCII digits
    public void IsDigitsOnly(string? input, bool expected)
        => Assert.Equal(expected, input.IsDigitsOnly());

    [Theory]
    [InlineData("abc", true)]
    [InlineData("Ünïcødé", true)]
    [InlineData("abc1", false)]
    [InlineData("a b", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsLettersOnly(string? input, bool expected)
        => Assert.Equal(expected, input.IsLettersOnly());

    [Theory]
    [InlineData("42", true)]
    [InlineData("-3.5", true)]
    [InlineData("+0.5", true)]
    [InlineData("1e3", true)]
    [InlineData(" 7 ", true)]
    [InlineData("abc", false)]
    [InlineData("1,5", false)]
    [InlineData("NaN", false)]
    [InlineData("Infinity", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsNumeric(string? input, bool expected)
        => Assert.Equal(expected, input.IsNumeric());

    [Fact]
    public void IsNumeric_IsCultureInvariant_ByDefault()
    {
        using var _ = new CultureScope("de-DE");

        Assert.True("1.5".IsNumeric());
    }

    [Fact]
    public void IgnoreCaseComparisons()
    {
        Assert.True("Hello World".ContainsIgnoreCase("WORLD"));
        Assert.False("Hello".ContainsIgnoreCase("bye"));
        Assert.False(((string?)null).ContainsIgnoreCase("a"));
        Assert.False("a".ContainsIgnoreCase(null));

        Assert.True("ABC".EqualsIgnoreCase("abc"));
        Assert.True(((string?)null).EqualsIgnoreCase(null));
        Assert.False("abc".EqualsIgnoreCase(null));

        Assert.True("FooBar".StartsWithIgnoreCase("foo"));
        Assert.False(((string?)null).StartsWithIgnoreCase("foo"));
        Assert.True("FooBar".EndsWithIgnoreCase("BAR"));
        Assert.False("FooBar".EndsWithIgnoreCase(null));
    }

    [Fact]
    public void IgnoreCaseComparisons_AreNotAffectedByTurkishI()
    {
        using var _ = new CultureScope("tr-TR");

        Assert.True("FILE".EqualsIgnoreCase("file"));
        Assert.True("config.INI".EndsWithIgnoreCase(".ini"));
    }
}
