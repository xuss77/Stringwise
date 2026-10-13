using System.Globalization;
using System.Net;

namespace Stringwise.Tests;

/// <summary>Keeps every example in README.md honest. If you change one, change the other.</summary>
public class ReadmeExamplesTests
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

    [Fact]
    public void QuickStart()
    {
        using var _ = new CultureScope("de-DE");

        Assert.Equal(42, "42".ToIntOrNull());
        Assert.Null("abc".ToIntOrNull());
        Assert.Equal(-1, "abc".ToIntOrDefault(-1));
        Assert.Equal(1.5m, "1.5".ToDecimal());
        Assert.True("yes".ToBool());
        Assert.Equal(Color.Red, "red".ToEnum<Color>());
        Assert.Equal("value", "key=value".After("="));
        Assert.Equal("creme-brulee", "Crème Brûlée!".ToSlug());
        Assert.Equal("xml_http_request", "XMLHttpRequest".ToSnakeCase());
        Assert.Equal("Hello w…", "Hello world".Truncate(8, "…"));
    }

    [Fact]
    public void Parsing()
    {
        Assert.Equal(42, "42".To<int>());
        Assert.Equal(42, "42".ToOrNull<int>());
        Assert.Equal(10, "x".ToOrDefault(10));
        Assert.True("42".TryTo(out int n));
        Assert.Equal(42, n);
        Assert.Equal(IPAddress.Loopback, "127.0.0.1".To<IPAddress>());
        Assert.Equal(1.5m, "1,5".ToDecimal(CultureInfo.GetCultureInfo("de-DE")));

        Assert.True("Yes".ToBool());
        Assert.False("off".ToBoolOrNull());
        Assert.Null("maybe".ToBoolOrNull());

        Assert.Equal(Color.Blue, "blue".ToEnum<Color>());
        Assert.Equal(Access.Read | Access.Write, "Read, Write".ToEnum<Access>());
        Assert.Null("42".ToEnumOrNull<Color>());
        Assert.Null("red".ToEnumOrNull<Color>(ignoreCase: false));
    }

    [Fact]
    public void Checks()
    {
        Assert.True("0123".IsDigitsOnly());
        Assert.True("Ünï".IsLettersOnly());
        Assert.True("-3.5e2".IsNumeric());
    }

    [Fact]
    public void Slicing()
    {
        Assert.Equal("Hel", "Hello".Left(3));
        Assert.Equal("Hello", "Hello".Right(50));
        Assert.Equal("a", "a/b/c".Before("/"));
        Assert.Equal("a/b", "a/b/c".BeforeLast("/"));
        Assert.Equal("b/c", "a/b/c".After("/"));
        Assert.Equal("c", "a/b/c".AfterLast("/"));
        Assert.Equal("x", "f(x)".Between("(", ")"));
    }

    [Fact]
    public void Transforms()
    {
        Assert.Null("".NullIfEmpty());
        Assert.Null("   ".NullIfWhiteSpace());
        Assert.Equal("a b c", "  a   b \n c ".CollapseWhitespace());
        Assert.Equal("ab", " a b ".RemoveWhitespace());
        Assert.Equal("Creme brulee", "Crème brûlée".RemoveDiacritics());
        Assert.Equal("strasse-cafe", "Straße & Café!".ToSlug());
        Assert.Equal("😀ba", "ab😀".Reverse());
        Assert.Equal("ababab", "ab".Repeat(3));
    }

    [Fact]
    public void Casing()
    {
        Assert.Equal("userIdNumber", "user ID number".ToCamelCase());
        Assert.Equal("UserId", "user_id".ToPascalCase());
        Assert.Equal("xml_http_request", "XMLHttpRequest".ToSnakeCase());
        Assert.Equal("xml-http-request", "XMLHttpRequest".ToKebabCase());
        Assert.Equal("MAX_RETRY_COUNT", "maxRetryCount".ToScreamingSnakeCase());
        Assert.Equal("Hello", "hello".Capitalize());
        Assert.Equal(["foo", "Bar", "Baz"], "fooBarBaz".SplitWords());
    }
}
