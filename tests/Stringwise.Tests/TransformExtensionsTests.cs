namespace Stringwise.Tests;

public class TransformExtensionsTests
{
    [Theory]
    [InlineData(null, null, null)]
    [InlineData("", null, null)]
    [InlineData("  ", "  ", null)]
    [InlineData("a", "a", "a")]
    public void NullIfEmpty_And_NullIfWhiteSpace(string? input, string? ifEmpty, string? ifWhiteSpace)
    {
        Assert.Equal(ifEmpty, input.NullIfEmpty());
        Assert.Equal(ifWhiteSpace, input.NullIfWhiteSpace());
    }

    [Theory]
    [InlineData("Hello world", 8, "", "Hello wo")]
    [InlineData("Hello world", 8, "…", "Hello w…")]
    [InlineData("Hello world", 8, "...", "Hello...")]
    [InlineData("Hello world", 11, "…", "Hello world")]
    [InlineData("Hello world", 50, "…", "Hello world")]
    [InlineData("Hello", 3, "...", "...")]
    [InlineData("Hello", 0, "", "")]
    [InlineData("", 5, "…", "")]
    public void Truncate(string input, int maxLength, string ellipsis, string expected)
    {
        var result = input.Truncate(maxLength, ellipsis);

        Assert.Equal(expected, result);
        Assert.True(result.Length <= maxLength);
    }

    [Fact]
    public void Truncate_DoesNotSplitSurrogatePairs()
    {
        // "ab😀cd": the emoji occupies indexes 2 and 3.
        Assert.Equal("ab", "ab😀cd".Truncate(3));
        Assert.Equal("ab😀", "ab😀cd".Truncate(4));
    }

    [Fact]
    public void Truncate_ValidatesArguments()
    {
        Assert.Null(((string?)null).Truncate(5));
        Assert.Throws<ArgumentOutOfRangeException>(() => "abc".Truncate(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => "abcdef".Truncate(2, "..."));
    }

    [Theory]
    [InlineData("  hello   big \t\n world  ", "hello big world")]
    [InlineData("single", "single")]
    [InlineData("   ", "")]
    [InlineData("", "")]
    public void CollapseWhitespace(string input, string expected)
        => Assert.Equal(expected, input.CollapseWhitespace());

    [Theory]
    [InlineData(" a b\tc\nd ", "abcd")]
    [InlineData("none", "none")]
    [InlineData("   ", "")]
    public void RemoveWhitespace(string input, string expected)
        => Assert.Equal(expected, input.RemoveWhitespace());

    [Theory]
    [InlineData("Crème brûlée", "Creme brulee")]
    [InlineData("São Paulo", "Sao Paulo")]
    [InlineData("naïve façade", "naive facade")]
    [InlineData("Ångström", "Angstrom")]
    [InlineData("plain", "plain")]
    public void RemoveDiacritics(string input, string expected)
        => Assert.Equal(expected, input.RemoveDiacritics());

    [Theory]
    [InlineData("Hello World", "hello-world")]
    [InlineData("  Hello,   World!  ", "hello-world")]
    [InlineData("Crème Brûlée & Café!", "creme-brulee-cafe")]
    [InlineData("Straße in Øslo", "strasse-in-oslo")]
    [InlineData("Æsir Œuvre Łódź Đorđe Þór Ðað", "aesir-oeuvre-lodz-dorde-thor-dad")]
    [InlineData("C# 10 is out", "c-10-is-out")]
    [InlineData("already-a-slug", "already-a-slug")]
    [InlineData("under_score", "under-score")]
    [InlineData("---", "")]
    [InlineData("", "")]
    public void ToSlug(string input, string expected)
        => Assert.Equal(expected, input.ToSlug());

    [Fact]
    public void ToSlug_CustomSeparator_And_Null()
    {
        Assert.Equal("hello_world", "Hello World".ToSlug('_'));
        Assert.Null(((string?)null).ToSlug());
    }

    [Fact]
    public void ToSlug_IsUnaffectedByTurkishCulture()
    {
        using var _ = new CultureScope("tr-TR");

        Assert.Equal("title-index", "TITLE INDEX".ToSlug());
    }

    [Theory]
    [InlineData("abc", "cba")]
    [InlineData("a", "a")]
    [InlineData("", "")]
    [InlineData("ab😀", "😀ba")]
    [InlineData("éa", "aé")] // "é" built from e + combining accent stays together
    public void Reverse(string input, string expected)
        => Assert.Equal(expected, input.Reverse());

    [Theory]
    [InlineData("ab", 3, "ababab")]
    [InlineData("ab", 1, "ab")]
    [InlineData("ab", 0, "")]
    [InlineData("", 5, "")]
    public void Repeat(string input, int count, string expected)
        => Assert.Equal(expected, input.Repeat(count));

    [Fact]
    public void Repeat_ValidatesArguments()
    {
        Assert.Null(((string?)null).Repeat(3));
        Assert.Throws<ArgumentOutOfRangeException>(() => "a".Repeat(-1));
    }

    [Fact]
    public void NullInput_ReturnsNull_ForAllTransforms()
    {
        string? input = null;

        Assert.Null(input.CollapseWhitespace());
        Assert.Null(input.RemoveWhitespace());
        Assert.Null(input.RemoveDiacritics());
        Assert.Null(input.Reverse());
    }
}
