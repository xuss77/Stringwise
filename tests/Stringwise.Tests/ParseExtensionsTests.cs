using System.Globalization;
using System.Net;

namespace Stringwise.Tests;

public class ParseExtensionsTests
{
    [Theory]
    [InlineData("42", 42)]
    [InlineData("-7", -7)]
    [InlineData("  15  ", 15)]
    [InlineData("+3", 3)]
    [InlineData("2147483647", int.MaxValue)]
    [InlineData("-2147483648", int.MinValue)]
    public void ToInt_ParsesValidInput(string input, int expected)
    {
        Assert.Equal(expected, input.ToInt());
        Assert.Equal(expected, input.ToIntOrNull());
        Assert.Equal(expected, input.ToIntOrDefault(-1));
        Assert.Equal(expected, input.To<int>());
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc")]
    [InlineData("4.2")]
    [InlineData("12abc")]
    [InlineData("2147483648")]
    public void ToIntOrNull_ReturnsNullForInvalidInput(string input)
    {
        Assert.Null(input.ToIntOrNull());
        Assert.Equal(-1, input.ToIntOrDefault(-1));
        Assert.Equal(0, input.ToIntOrDefault());
        Assert.False(input.TryTo<int>(out _));
    }

    [Fact]
    public void NullInput_IsHandledByEveryVariant()
    {
        string? input = null;

        Assert.Null(input.ToIntOrNull());
        Assert.Equal(5, input.ToIntOrDefault(5));
        Assert.False(input.TryTo<int>(out _));
        Assert.Throws<ArgumentNullException>(() => input.ToInt());
        Assert.Throws<ArgumentNullException>(() => input.To<Guid>());
    }

    [Fact]
    public void ToInt_ThrowsFormatException_ForGarbage()
        => Assert.Throws<FormatException>(() => "nope".ToInt());

    [Fact]
    public void ToInt_ThrowsOverflowException_WhenOutOfRange()
        => Assert.Throws<OverflowException>(() => "99999999999".ToInt());

    [Fact]
    public void ToByte_RejectsOutOfRange()
    {
        Assert.Equal((byte)255, "255".ToByteOrNull());
        Assert.Null("256".ToByteOrNull());
        Assert.Null("-1".ToByteOrNull());
    }

    [Fact]
    public void ToLong_And_ToShort_Work()
    {
        Assert.Equal(9_000_000_000L, "9000000000".ToLong());
        Assert.Equal((short)-300, "-300".ToShortOrNull());
        Assert.Null("40000".ToShortOrNull());
    }

    [Theory]
    [InlineData("de-DE")]
    [InlineData("fr-FR")]
    [InlineData("tr-TR")]
    [InlineData("en-US")]
    public void DecimalParsing_IsCultureInvariant_ByDefault(string culture)
    {
        using var _ = new CultureScope(culture);

        Assert.Equal(1.5m, "1.5".ToDecimal());
        Assert.Equal(1.5, "1.5".ToDouble());
        Assert.Equal(1.5f, "1.5".ToFloat());
        Assert.Equal(1234.5m, "1,234.5".ToDecimalOrNull());
    }

    [Fact]
    public void ExplicitProvider_IsRespected()
    {
        var german = CultureInfo.GetCultureInfo("de-DE");

        Assert.Equal(1.5m, "1,5".ToDecimal(german));
        Assert.Equal(1.5, "1,5".ToDoubleOrNull(german));
        Assert.Equal(1.5m, "1,5".ToOrDefault(0m, german));
    }

    [Fact]
    public void ToDouble_HandlesExponentAndSpecialValues()
    {
        Assert.Equal(1000d, "1e3".ToDouble());
        Assert.Equal(double.PositiveInfinity, "Infinity".ToDouble());
        Assert.True(double.IsNaN("NaN".ToDouble()));
    }

    [Fact]
    public void ToGuid_Works()
    {
        var guid = Guid.NewGuid();

        Assert.Equal(guid, guid.ToString().ToGuid());
        Assert.Equal(guid, guid.ToString("N").ToGuidOrNull());
        Assert.Equal(guid, guid.ToString("B").ToGuidOrNull());
        Assert.Null("not-a-guid".ToGuidOrNull());
        Assert.Equal(Guid.Empty, "nope".ToGuidOrDefault());
    }

    [Fact]
    public void DateAndTimeTypes_ParseIsoFormats()
    {
        using var _ = new CultureScope("de-DE");

        Assert.Equal(new DateTime(2026, 3, 4, 5, 6, 7), "2026-03-04T05:06:07".ToDateTime());
        Assert.Equal(new DateOnly(2026, 3, 4), "2026-03-04".ToDateOnly());
        Assert.Equal(new TimeOnly(13, 45), "13:45".ToTimeOnlyOrNull());
        Assert.Equal(TimeSpan.FromMinutes(90), "01:30:00".ToTimeSpan());
        Assert.Equal(
            new DateTimeOffset(2026, 3, 4, 5, 6, 7, TimeSpan.FromHours(2)),
            "2026-03-04T05:06:07+02:00".ToDateTimeOffsetOrNull());
        Assert.Null("2026-02-30".ToDateOnlyOrNull());
        Assert.Null("25:00".ToTimeOnlyOrNull());
    }

    [Fact]
    public void Invariant_DateParsing_DoesNotUseLocalDayMonthOrder()
    {
        // Invariant culture reads "03/04/2026" as March 4th regardless of the machine's culture.
        using var _ = new CultureScope("en-GB");

        Assert.Equal(new DateOnly(2026, 3, 4), "03/04/2026".ToDateOnly());
        Assert.Equal(new DateOnly(2026, 4, 3), "03/04/2026".ToDateOnly(CultureInfo.GetCultureInfo("en-GB")));
    }

    [Fact]
    public void TryTo_ReturnsParsedValue()
    {
        Assert.True("123".TryTo(out int value));
        Assert.Equal(123, value);
    }

    [Fact]
    public void GenericMethods_WorkWithAnyIParsableType()
    {
        Assert.Equal(IPAddress.Loopback, "127.0.0.1".To<IPAddress>());
        Assert.Equal(Int128.MaxValue, "170141183460469231731687303715884105727".To<Int128>());
        Assert.Equal('x', "x".ToOrNull<char>());
        Assert.Null("xy".ToOrNull<char>());
    }

    [Fact]
    public void ToOrDefault_WorksWithReferenceTypes()
    {
        var fallback = IPAddress.None;

        Assert.Same(fallback, "garbage".ToOrDefault(fallback));
        Assert.Equal(IPAddress.Parse("10.0.0.1"), "10.0.0.1".ToOrDefault(fallback));
    }
}
