using System.Globalization;

namespace Stringwise.Tests;

/// <summary>
/// Exercises every typed shortcut (ToX / ToXOrNull / ToXOrDefault) in both the invariant
/// and the culture-aware class, so a copy-paste mistake in the generated file can't slip through.
/// </summary>
public class TypedShortcutTests
{
    private static readonly CultureInfo German = CultureInfo.GetCultureInfo("de-DE");

    [Fact]
    public void Int_Shortcuts() => Check("12", 12, s => s.ToInt(), s => s.ToIntOrNull(), (s, d) => s.ToIntOrDefault(d),
        (s, p) => s.ToInt(p), (s, p) => s.ToIntOrNull(p), (s, d, p) => s.ToIntOrDefault(d, p), fallback: 99);

    [Fact]
    public void Long_Shortcuts() => Check("12", 12L, s => s.ToLong(), s => s.ToLongOrNull(), (s, d) => s.ToLongOrDefault(d),
        (s, p) => s.ToLong(p), (s, p) => s.ToLongOrNull(p), (s, d, p) => s.ToLongOrDefault(d, p), fallback: 99L);

    [Fact]
    public void Short_Shortcuts() => Check("12", (short)12, s => s.ToShort(), s => s.ToShortOrNull(), (s, d) => s.ToShortOrDefault(d),
        (s, p) => s.ToShort(p), (s, p) => s.ToShortOrNull(p), (s, d, p) => s.ToShortOrDefault(d, p), fallback: (short)99);

    [Fact]
    public void Byte_Shortcuts() => Check("12", (byte)12, s => s.ToByte(), s => s.ToByteOrNull(), (s, d) => s.ToByteOrDefault(d),
        (s, p) => s.ToByte(p), (s, p) => s.ToByteOrNull(p), (s, d, p) => s.ToByteOrDefault(d, p), fallback: (byte)99);

    [Fact]
    public void Decimal_Shortcuts() => Check("1.5", 1.5m, s => s.ToDecimal(), s => s.ToDecimalOrNull(), (s, d) => s.ToDecimalOrDefault(d),
        (s, p) => s.ToDecimal(p), (s, p) => s.ToDecimalOrNull(p), (s, d, p) => s.ToDecimalOrDefault(d, p), fallback: 9.9m, germanInput: "1,5");

    [Fact]
    public void Double_Shortcuts() => Check("1.5", 1.5, s => s.ToDouble(), s => s.ToDoubleOrNull(), (s, d) => s.ToDoubleOrDefault(d),
        (s, p) => s.ToDouble(p), (s, p) => s.ToDoubleOrNull(p), (s, d, p) => s.ToDoubleOrDefault(d, p), fallback: 9.9, germanInput: "1,5");

    [Fact]
    public void Float_Shortcuts() => Check("1.5", 1.5f, s => s.ToFloat(), s => s.ToFloatOrNull(), (s, d) => s.ToFloatOrDefault(d),
        (s, p) => s.ToFloat(p), (s, p) => s.ToFloatOrNull(p), (s, d, p) => s.ToFloatOrDefault(d, p), fallback: 9.9f, germanInput: "1,5");

    [Fact]
    public void Guid_Shortcuts()
    {
        var guid = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");
        var fallback = System.Guid.NewGuid();
        Check(guid.ToString(), guid, s => s.ToGuid(), s => s.ToGuidOrNull(), (s, d) => s.ToGuidOrDefault(d),
            (s, p) => s.ToGuid(p), (s, p) => s.ToGuidOrNull(p), (s, d, p) => s.ToGuidOrDefault(d, p), fallback);
    }

    [Fact]
    public void DateTime_Shortcuts() => Check("2026-01-02", new DateTime(2026, 1, 2), s => s.ToDateTime(), s => s.ToDateTimeOrNull(), (s, d) => s.ToDateTimeOrDefault(d),
        (s, p) => s.ToDateTime(p), (s, p) => s.ToDateTimeOrNull(p), (s, d, p) => s.ToDateTimeOrDefault(d, p), fallback: System.DateTime.UnixEpoch, germanInput: "02.01.2026");

    [Fact]
    public void DateTimeOffset_Shortcuts() => Check("2026-01-02T00:00:00+00:00", new DateTimeOffset(2026, 1, 2, 0, 0, 0, TimeSpan.Zero),
        s => s.ToDateTimeOffset(), s => s.ToDateTimeOffsetOrNull(), (s, d) => s.ToDateTimeOffsetOrDefault(d),
        (s, p) => s.ToDateTimeOffset(p), (s, p) => s.ToDateTimeOffsetOrNull(p), (s, d, p) => s.ToDateTimeOffsetOrDefault(d, p),
        fallback: System.DateTimeOffset.UnixEpoch);

    [Fact]
    public void DateOnly_Shortcuts() => Check("2026-01-02", new DateOnly(2026, 1, 2), s => s.ToDateOnly(), s => s.ToDateOnlyOrNull(), (s, d) => s.ToDateOnlyOrDefault(d),
        (s, p) => s.ToDateOnly(p), (s, p) => s.ToDateOnlyOrNull(p), (s, d, p) => s.ToDateOnlyOrDefault(d, p), fallback: System.DateOnly.MinValue, germanInput: "02.01.2026");

    [Fact]
    public void TimeOnly_Shortcuts() => Check("13:45", new TimeOnly(13, 45), s => s.ToTimeOnly(), s => s.ToTimeOnlyOrNull(), (s, d) => s.ToTimeOnlyOrDefault(d),
        (s, p) => s.ToTimeOnly(p), (s, p) => s.ToTimeOnlyOrNull(p), (s, d, p) => s.ToTimeOnlyOrDefault(d, p), fallback: System.TimeOnly.MinValue);

    [Fact]
    public void TimeSpan_Shortcuts() => Check("01:30:00", System.TimeSpan.FromMinutes(90), s => s.ToTimeSpan(), s => s.ToTimeSpanOrNull(), (s, d) => s.ToTimeSpanOrDefault(d),
        (s, p) => s.ToTimeSpan(p), (s, p) => s.ToTimeSpanOrNull(p), (s, d, p) => s.ToTimeSpanOrDefault(d, p), fallback: System.TimeSpan.MaxValue);

    [Fact]
    public void IsNumeric_WithProvider()
    {
        Assert.True("1,5".IsNumeric(German));
        Assert.False("abc".IsNumeric(German));
    }

    private static void Check<T>(
        string validInput,
        T expected,
        Func<string?, T> to,
        Func<string?, T?> toOrNull,
        Func<string?, T, T> toOrDefault,
        Func<string?, IFormatProvider, T> toWithProvider,
        Func<string?, IFormatProvider, T?> toOrNullWithProvider,
        Func<string?, T, IFormatProvider, T> toOrDefaultWithProvider,
        T fallback,
        string? germanInput = null)
        where T : struct
    {
        const string Garbage = "not a value";

        Assert.Equal(expected, to(validInput));
        Assert.Equal(expected, toOrNull(validInput));
        Assert.Equal(expected, toOrDefault(validInput, fallback));
        Assert.Null(toOrNull(Garbage));
        Assert.Null(toOrNull(null));
        Assert.Equal(fallback, toOrDefault(Garbage, fallback));
        Assert.Equal(fallback, toOrDefault(null, fallback));
        Assert.Throws<FormatException>(() => to(Garbage));
        Assert.Throws<ArgumentNullException>(() => to(null));

        var culture = germanInput is null ? CultureInfo.InvariantCulture : German;
        var cultureInput = germanInput ?? validInput;
        Assert.Equal(expected, toWithProvider(cultureInput, culture));
        Assert.Equal(expected, toOrNullWithProvider(cultureInput, culture));
        Assert.Equal(expected, toOrDefaultWithProvider(cultureInput, fallback, culture));
        Assert.Null(toOrNullWithProvider(Garbage, culture));
        Assert.Equal(fallback, toOrDefaultWithProvider(Garbage, fallback, culture));
    }
}
