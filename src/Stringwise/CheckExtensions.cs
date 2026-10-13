using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Stringwise;

/// <summary>Boolean checks on strings. All methods are null-safe.</summary>
public static class CheckExtensions
{
    /// <summary>Returns <see langword="true"/> if the string is <see langword="null"/> or empty.</summary>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value)
        => string.IsNullOrEmpty(value);

    /// <summary>Returns <see langword="true"/> if the string is <see langword="null"/>, empty, or only whitespace.</summary>
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? value)
        => string.IsNullOrWhiteSpace(value);

    /// <summary>Returns <see langword="true"/> if the string contains at least one non-whitespace character.</summary>
    public static bool HasValue([NotNullWhen(true)] this string? value)
        => !string.IsNullOrWhiteSpace(value);

    /// <summary>Returns <see langword="true"/> if the string is non-empty and consists only of ASCII digits (0-9).</summary>
    public static bool IsDigitsOnly([NotNullWhen(true)] this string? value)
        => !string.IsNullOrEmpty(value) && !value.AsSpan().ContainsAnyExceptInRange('0', '9');

    /// <summary>Returns <see langword="true"/> if the string is non-empty and consists only of letters.</summary>
    public static bool IsLettersOnly([NotNullWhen(true)] this string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        foreach (var c in value)
        {
            if (!char.IsLetter(c))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Returns <see langword="true"/> if the string parses as a number (e.g. <c>"42"</c>, <c>"-3.5"</c>, <c>"1e3"</c>).
    /// Uses the invariant culture; see <see cref="CultureParseExtensions.IsNumeric"/> for a culture-aware overload.
    /// </summary>
    public static bool IsNumeric([NotNullWhen(true)] this string? value)
        => value.IsNumeric(CultureInfo.InvariantCulture);

    /// <summary>Case-insensitive (ordinal) <see cref="string.Contains(string)"/>. Returns <see langword="false"/> if either string is <see langword="null"/>.</summary>
    public static bool ContainsIgnoreCase([NotNullWhen(true)] this string? value, string? search)
        => value is not null && search is not null && value.Contains(search, StringComparison.OrdinalIgnoreCase);

    /// <summary>Case-insensitive (ordinal) equality. Two <see langword="null"/> strings are equal.</summary>
    public static bool EqualsIgnoreCase(this string? value, string? other)
        => string.Equals(value, other, StringComparison.OrdinalIgnoreCase);

    /// <summary>Case-insensitive (ordinal) <see cref="string.StartsWith(string)"/>. Returns <see langword="false"/> if either string is <see langword="null"/>.</summary>
    public static bool StartsWithIgnoreCase([NotNullWhen(true)] this string? value, string? prefix)
        => value is not null && prefix is not null && value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);

    /// <summary>Case-insensitive (ordinal) <see cref="string.EndsWith(string)"/>. Returns <see langword="false"/> if either string is <see langword="null"/>.</summary>
    public static bool EndsWithIgnoreCase([NotNullWhen(true)] this string? value, string? suffix)
        => value is not null && suffix is not null && value.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
}
