using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Stringwise;

/// <summary>
/// Generic parsing extensions for any type implementing <see cref="IParsable{TSelf}"/>
/// (int, long, decimal, double, Guid, DateTime, DateOnly, TimeSpan, IPAddress, ...).
/// </summary>
/// <remarks>
/// Every method here uses <see cref="CultureInfo.InvariantCulture"/>, so results never depend on the
/// machine's locale. To parse with a specific culture, use the overloads in <see cref="CultureParseExtensions"/>.
/// Naming convention used across the library:
/// <list type="bullet">
/// <item><c>ToX</c> throws when the input cannot be parsed.</item>
/// <item><c>ToXOrNull</c> returns <see langword="null"/> when the input cannot be parsed.</item>
/// <item><c>ToXOrDefault</c> returns a fallback value when the input cannot be parsed.</item>
/// <item><c>TryToX</c> follows the standard Try pattern.</item>
/// </list>
/// </remarks>
public static partial class ParseExtensions
{
    /// <summary>Parses the string as <typeparamref name="T"/> using the invariant culture, throwing if it is invalid.</summary>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
    /// <exception cref="FormatException"><paramref name="value"/> is not a valid <typeparamref name="T"/>.</exception>
    /// <exception cref="OverflowException">The value is outside the range of <typeparamref name="T"/>.</exception>
    public static T To<T>(this string? value)
        where T : IParsable<T>
        => value.To<T>(CultureInfo.InvariantCulture);

    /// <summary>Parses the string as <typeparamref name="T"/> using the invariant culture, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static T? ToOrNull<T>(this string? value)
        where T : struct, IParsable<T>
        => value.ToOrNull<T>(CultureInfo.InvariantCulture);

    /// <summary>Parses the string as <typeparamref name="T"/> using the invariant culture, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static T ToOrDefault<T>(this string? value, T defaultValue)
        where T : IParsable<T>
        => value.ToOrDefault(defaultValue, CultureInfo.InvariantCulture);

    /// <summary>Attempts to parse the string as <typeparamref name="T"/> using the invariant culture.</summary>
    /// <returns><see langword="true"/> if parsing succeeded; otherwise <see langword="false"/>.</returns>
    public static bool TryTo<T>(this string? value, [MaybeNullWhen(false)] out T result)
        where T : IParsable<T>
        => value.TryTo(CultureInfo.InvariantCulture, out result);
}

/// <summary>
/// Culture-aware overloads of the <see cref="ParseExtensions"/> methods.
/// </summary>
/// <remarks>
/// These live in a separate class on purpose: if the invariant overloads shared a class with
/// <see cref="IFormatProvider"/> overloads, code analysis rule CA1305 would warn on every
/// <c>"42".ToInt()</c> call in consuming projects, even though the default is already culture-safe.
/// </remarks>
public static partial class CultureParseExtensions
{
    /// <summary>Parses the string as <typeparamref name="T"/> using <paramref name="provider"/>, throwing if it is invalid.</summary>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
    /// <exception cref="FormatException"><paramref name="value"/> is not a valid <typeparamref name="T"/>.</exception>
    /// <exception cref="OverflowException">The value is outside the range of <typeparamref name="T"/>.</exception>
    public static T To<T>(this string? value, IFormatProvider? provider)
        where T : IParsable<T>
    {
        ArgumentNullException.ThrowIfNull(value);
        return T.Parse(value, provider);
    }

    /// <summary>Parses the string as <typeparamref name="T"/> using <paramref name="provider"/>, or returns <see langword="null"/> if it is null or invalid.</summary>
    public static T? ToOrNull<T>(this string? value, IFormatProvider? provider)
        where T : struct, IParsable<T>
        => value.TryTo(provider, out T result) ? result : null;

    /// <summary>Parses the string as <typeparamref name="T"/> using <paramref name="provider"/>, or returns <paramref name="defaultValue"/> if it is null or invalid.</summary>
    public static T ToOrDefault<T>(this string? value, T defaultValue, IFormatProvider? provider)
        where T : IParsable<T>
        => value.TryTo<T>(provider, out var result) ? result : defaultValue;

    /// <summary>Attempts to parse the string as <typeparamref name="T"/> using <paramref name="provider"/>.</summary>
    /// <returns><see langword="true"/> if parsing succeeded; otherwise <see langword="false"/>.</returns>
    public static bool TryTo<T>(this string? value, IFormatProvider? provider, [MaybeNullWhen(false)] out T result)
        where T : IParsable<T>
        => T.TryParse(value, provider, out result);

    /// <summary>Returns <see langword="true"/> if the string parses as a finite number using <paramref name="provider"/>.</summary>
    public static bool IsNumeric([NotNullWhen(true)] this string? value, IFormatProvider? provider)
        => double.TryParse(value, NumberStyles.Float, provider, out var d) && double.IsFinite(d);
}
