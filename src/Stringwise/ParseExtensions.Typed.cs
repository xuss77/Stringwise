// Typed shortcuts over the generic parse methods. Keep both classes in sync.
namespace Stringwise;

public static partial class ParseExtensions
{
    /// <summary>Parses the string as <see cref="int"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static int ToInt(this string? value) => value.To<int>();

    /// <summary>Parses the string as <see cref="int"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static int? ToIntOrNull(this string? value) => value.ToOrNull<int>();

    /// <summary>Parses the string as <see cref="int"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static int ToIntOrDefault(this string? value, int defaultValue = default) => value.ToOrDefault(defaultValue);

    /// <summary>Parses the string as <see cref="long"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static long ToLong(this string? value) => value.To<long>();

    /// <summary>Parses the string as <see cref="long"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static long? ToLongOrNull(this string? value) => value.ToOrNull<long>();

    /// <summary>Parses the string as <see cref="long"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static long ToLongOrDefault(this string? value, long defaultValue = default) => value.ToOrDefault(defaultValue);

    /// <summary>Parses the string as <see cref="short"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static short ToShort(this string? value) => value.To<short>();

    /// <summary>Parses the string as <see cref="short"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static short? ToShortOrNull(this string? value) => value.ToOrNull<short>();

    /// <summary>Parses the string as <see cref="short"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static short ToShortOrDefault(this string? value, short defaultValue = default) => value.ToOrDefault(defaultValue);

    /// <summary>Parses the string as <see cref="byte"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static byte ToByte(this string? value) => value.To<byte>();

    /// <summary>Parses the string as <see cref="byte"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static byte? ToByteOrNull(this string? value) => value.ToOrNull<byte>();

    /// <summary>Parses the string as <see cref="byte"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static byte ToByteOrDefault(this string? value, byte defaultValue = default) => value.ToOrDefault(defaultValue);

    /// <summary>Parses the string as <see cref="decimal"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static decimal ToDecimal(this string? value) => value.To<decimal>();

    /// <summary>Parses the string as <see cref="decimal"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static decimal? ToDecimalOrNull(this string? value) => value.ToOrNull<decimal>();

    /// <summary>Parses the string as <see cref="decimal"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static decimal ToDecimalOrDefault(this string? value, decimal defaultValue = default) => value.ToOrDefault(defaultValue);

    /// <summary>Parses the string as <see cref="double"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static double ToDouble(this string? value) => value.To<double>();

    /// <summary>Parses the string as <see cref="double"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static double? ToDoubleOrNull(this string? value) => value.ToOrNull<double>();

    /// <summary>Parses the string as <see cref="double"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static double ToDoubleOrDefault(this string? value, double defaultValue = default) => value.ToOrDefault(defaultValue);

    /// <summary>Parses the string as <see cref="float"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static float ToFloat(this string? value) => value.To<float>();

    /// <summary>Parses the string as <see cref="float"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static float? ToFloatOrNull(this string? value) => value.ToOrNull<float>();

    /// <summary>Parses the string as <see cref="float"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static float ToFloatOrDefault(this string? value, float defaultValue = default) => value.ToOrDefault(defaultValue);

    /// <summary>Parses the string as <see cref="Guid"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static Guid ToGuid(this string? value) => value.To<Guid>();

    /// <summary>Parses the string as <see cref="Guid"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static Guid? ToGuidOrNull(this string? value) => value.ToOrNull<Guid>();

    /// <summary>Parses the string as <see cref="Guid"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static Guid ToGuidOrDefault(this string? value, Guid defaultValue = default) => value.ToOrDefault(defaultValue);

    /// <summary>Parses the string as <see cref="DateTime"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static DateTime ToDateTime(this string? value) => value.To<DateTime>();

    /// <summary>Parses the string as <see cref="DateTime"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static DateTime? ToDateTimeOrNull(this string? value) => value.ToOrNull<DateTime>();

    /// <summary>Parses the string as <see cref="DateTime"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static DateTime ToDateTimeOrDefault(this string? value, DateTime defaultValue = default) => value.ToOrDefault(defaultValue);

    /// <summary>Parses the string as <see cref="DateTimeOffset"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static DateTimeOffset ToDateTimeOffset(this string? value) => value.To<DateTimeOffset>();

    /// <summary>Parses the string as <see cref="DateTimeOffset"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static DateTimeOffset? ToDateTimeOffsetOrNull(this string? value) => value.ToOrNull<DateTimeOffset>();

    /// <summary>Parses the string as <see cref="DateTimeOffset"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static DateTimeOffset ToDateTimeOffsetOrDefault(this string? value, DateTimeOffset defaultValue = default) => value.ToOrDefault(defaultValue);

    /// <summary>Parses the string as <see cref="DateOnly"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static DateOnly ToDateOnly(this string? value) => value.To<DateOnly>();

    /// <summary>Parses the string as <see cref="DateOnly"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static DateOnly? ToDateOnlyOrNull(this string? value) => value.ToOrNull<DateOnly>();

    /// <summary>Parses the string as <see cref="DateOnly"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static DateOnly ToDateOnlyOrDefault(this string? value, DateOnly defaultValue = default) => value.ToOrDefault(defaultValue);

    /// <summary>Parses the string as <see cref="TimeOnly"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static TimeOnly ToTimeOnly(this string? value) => value.To<TimeOnly>();

    /// <summary>Parses the string as <see cref="TimeOnly"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static TimeOnly? ToTimeOnlyOrNull(this string? value) => value.ToOrNull<TimeOnly>();

    /// <summary>Parses the string as <see cref="TimeOnly"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static TimeOnly ToTimeOnlyOrDefault(this string? value, TimeOnly defaultValue = default) => value.ToOrDefault(defaultValue);

    /// <summary>Parses the string as <see cref="TimeSpan"/> using the invariant culture, throwing if it is null or invalid.</summary>
    public static TimeSpan ToTimeSpan(this string? value) => value.To<TimeSpan>();

    /// <summary>Parses the string as <see cref="TimeSpan"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static TimeSpan? ToTimeSpanOrNull(this string? value) => value.ToOrNull<TimeSpan>();

    /// <summary>Parses the string as <see cref="TimeSpan"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static TimeSpan ToTimeSpanOrDefault(this string? value, TimeSpan defaultValue = default) => value.ToOrDefault(defaultValue);
}

public static partial class CultureParseExtensions
{
    /// <summary>Parses the string as <see cref="int"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static int ToInt(this string? value, IFormatProvider? provider) => value.To<int>(provider);

    /// <summary>Parses the string as <see cref="int"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static int? ToIntOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<int>(provider);

    /// <summary>Parses the string as <see cref="int"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static int ToIntOrDefault(this string? value, int defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);

    /// <summary>Parses the string as <see cref="long"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static long ToLong(this string? value, IFormatProvider? provider) => value.To<long>(provider);

    /// <summary>Parses the string as <see cref="long"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static long? ToLongOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<long>(provider);

    /// <summary>Parses the string as <see cref="long"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static long ToLongOrDefault(this string? value, long defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);

    /// <summary>Parses the string as <see cref="short"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static short ToShort(this string? value, IFormatProvider? provider) => value.To<short>(provider);

    /// <summary>Parses the string as <see cref="short"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static short? ToShortOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<short>(provider);

    /// <summary>Parses the string as <see cref="short"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static short ToShortOrDefault(this string? value, short defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);

    /// <summary>Parses the string as <see cref="byte"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static byte ToByte(this string? value, IFormatProvider? provider) => value.To<byte>(provider);

    /// <summary>Parses the string as <see cref="byte"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static byte? ToByteOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<byte>(provider);

    /// <summary>Parses the string as <see cref="byte"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static byte ToByteOrDefault(this string? value, byte defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);

    /// <summary>Parses the string as <see cref="decimal"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static decimal ToDecimal(this string? value, IFormatProvider? provider) => value.To<decimal>(provider);

    /// <summary>Parses the string as <see cref="decimal"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static decimal? ToDecimalOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<decimal>(provider);

    /// <summary>Parses the string as <see cref="decimal"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static decimal ToDecimalOrDefault(this string? value, decimal defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);

    /// <summary>Parses the string as <see cref="double"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static double ToDouble(this string? value, IFormatProvider? provider) => value.To<double>(provider);

    /// <summary>Parses the string as <see cref="double"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static double? ToDoubleOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<double>(provider);

    /// <summary>Parses the string as <see cref="double"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static double ToDoubleOrDefault(this string? value, double defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);

    /// <summary>Parses the string as <see cref="float"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static float ToFloat(this string? value, IFormatProvider? provider) => value.To<float>(provider);

    /// <summary>Parses the string as <see cref="float"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static float? ToFloatOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<float>(provider);

    /// <summary>Parses the string as <see cref="float"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static float ToFloatOrDefault(this string? value, float defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);

    /// <summary>Parses the string as <see cref="Guid"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static Guid ToGuid(this string? value, IFormatProvider? provider) => value.To<Guid>(provider);

    /// <summary>Parses the string as <see cref="Guid"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static Guid? ToGuidOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<Guid>(provider);

    /// <summary>Parses the string as <see cref="Guid"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static Guid ToGuidOrDefault(this string? value, Guid defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);

    /// <summary>Parses the string as <see cref="DateTime"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static DateTime ToDateTime(this string? value, IFormatProvider? provider) => value.To<DateTime>(provider);

    /// <summary>Parses the string as <see cref="DateTime"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static DateTime? ToDateTimeOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<DateTime>(provider);

    /// <summary>Parses the string as <see cref="DateTime"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static DateTime ToDateTimeOrDefault(this string? value, DateTime defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);

    /// <summary>Parses the string as <see cref="DateTimeOffset"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static DateTimeOffset ToDateTimeOffset(this string? value, IFormatProvider? provider) => value.To<DateTimeOffset>(provider);

    /// <summary>Parses the string as <see cref="DateTimeOffset"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static DateTimeOffset? ToDateTimeOffsetOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<DateTimeOffset>(provider);

    /// <summary>Parses the string as <see cref="DateTimeOffset"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static DateTimeOffset ToDateTimeOffsetOrDefault(this string? value, DateTimeOffset defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);

    /// <summary>Parses the string as <see cref="DateOnly"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static DateOnly ToDateOnly(this string? value, IFormatProvider? provider) => value.To<DateOnly>(provider);

    /// <summary>Parses the string as <see cref="DateOnly"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static DateOnly? ToDateOnlyOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<DateOnly>(provider);

    /// <summary>Parses the string as <see cref="DateOnly"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static DateOnly ToDateOnlyOrDefault(this string? value, DateOnly defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);

    /// <summary>Parses the string as <see cref="TimeOnly"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static TimeOnly ToTimeOnly(this string? value, IFormatProvider? provider) => value.To<TimeOnly>(provider);

    /// <summary>Parses the string as <see cref="TimeOnly"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static TimeOnly? ToTimeOnlyOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<TimeOnly>(provider);

    /// <summary>Parses the string as <see cref="TimeOnly"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static TimeOnly ToTimeOnlyOrDefault(this string? value, TimeOnly defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);

    /// <summary>Parses the string as <see cref="TimeSpan"/> using <paramref name="provider"/>, throwing if it is null or invalid.</summary>
    public static TimeSpan ToTimeSpan(this string? value, IFormatProvider? provider) => value.To<TimeSpan>(provider);

    /// <summary>Parses the string as <see cref="TimeSpan"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static TimeSpan? ToTimeSpanOrNull(this string? value, IFormatProvider? provider) => value.ToOrNull<TimeSpan>(provider);

    /// <summary>Parses the string as <see cref="TimeSpan"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static TimeSpan ToTimeSpanOrDefault(this string? value, TimeSpan defaultValue, IFormatProvider? provider) => value.ToOrDefault(defaultValue, provider);
}
