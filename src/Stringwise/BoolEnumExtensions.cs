using System.Globalization;

namespace Stringwise;

/// <summary>Parsing extensions for <see cref="bool"/> and enum types.</summary>
public static class BoolEnumExtensions
{
    /// <summary>
    /// Parses the string as a <see cref="bool"/>, throwing if it is not recognized.
    /// Accepts (case-insensitive, surrounding whitespace ignored):
    /// <c>true/false</c>, <c>yes/no</c>, <c>y/n</c>, <c>on/off</c>, <c>1/0</c>, <c>t/f</c>.
    /// </summary>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
    /// <exception cref="FormatException"><paramref name="value"/> is not a recognized boolean.</exception>
    public static bool ToBool(this string? value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value.ToBoolOrNull()
            ?? throw new FormatException($"'{value}' is not a recognized boolean value.");
    }

    /// <summary>Parses the string as a <see cref="bool"/> (see <see cref="ToBool"/> for accepted values), or returns <see langword="null"/>.</summary>
    public static bool? ToBoolOrNull(this string? value)
    {
        if (value is null)
        {
            return null;
        }

        var span = value.AsSpan().Trim();
        if (IsAny(span, "true", "yes", "y", "on", "1", "t"))
        {
            return true;
        }

        if (IsAny(span, "false", "no", "n", "off", "0", "f"))
        {
            return false;
        }

        return null;
    }

    /// <summary>Parses the string as a <see cref="bool"/> (see <see cref="ToBool"/> for accepted values), or returns <paramref name="defaultValue"/>.</summary>
    public static bool ToBoolOrDefault(this string? value, bool defaultValue = false)
        => value.ToBoolOrNull() ?? defaultValue;

    /// <summary>
    /// Parses the string as <typeparamref name="TEnum"/>, throwing if it is not a defined member.
    /// Matches member names (case-insensitive by default) and comma-separated flag combinations.
    /// Numeric strings are accepted only when they map to a defined member.
    /// </summary>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
    /// <exception cref="FormatException"><paramref name="value"/> is not a defined member of <typeparamref name="TEnum"/>.</exception>
    public static TEnum ToEnum<TEnum>(this string? value, bool ignoreCase = true)
        where TEnum : struct, Enum
    {
        ArgumentNullException.ThrowIfNull(value);
        return value.ToEnumOrNull<TEnum>(ignoreCase)
            ?? throw new FormatException($"'{value}' is not a defined value of {typeof(TEnum).Name}.");
    }

    /// <summary>Parses the string as <typeparamref name="TEnum"/> (see <see cref="ToEnum{TEnum}"/>), or returns <see langword="null"/>.</summary>
    public static TEnum? ToEnumOrNull<TEnum>(this string? value, bool ignoreCase = true)
        where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value) || !Enum.TryParse(value, ignoreCase, out TEnum result))
        {
            return null;
        }

        // Enum.TryParse happily accepts any number ("42"), even if no member has that value.
        var trimmed = value.AsSpan().Trim();
        var looksNumeric = long.TryParse(trimmed, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
        return looksNumeric && !Enum.IsDefined(result) ? null : result;
    }

    /// <summary>Parses the string as <typeparamref name="TEnum"/> (see <see cref="ToEnum{TEnum}"/>), or returns <paramref name="defaultValue"/>.</summary>
    public static TEnum ToEnumOrDefault<TEnum>(this string? value, TEnum defaultValue = default, bool ignoreCase = true)
        where TEnum : struct, Enum
        => value.ToEnumOrNull<TEnum>(ignoreCase) ?? defaultValue;

    private static bool IsAny(ReadOnlySpan<char> span, params ReadOnlySpan<string> candidates)
    {
        foreach (var candidate in candidates)
        {
            if (span.Equals(candidate, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
