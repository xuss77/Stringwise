using System.Diagnostics.CodeAnalysis;

namespace Stringwise;

/// <summary>
/// Substring helpers that never throw on short input or missing delimiters.
/// A <see langword="null"/> input always returns <see langword="null"/>;
/// a missing delimiter returns <see cref="string.Empty"/>.
/// </summary>
public static class SliceExtensions
{
    /// <summary>Returns the first <paramref name="length"/> characters, or the whole string if it is shorter.</summary>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is negative.</exception>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? Left(this string? value, int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        return value is null || value.Length <= length ? value : value[..length];
    }

    /// <summary>Returns the last <paramref name="length"/> characters, or the whole string if it is shorter.</summary>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is negative.</exception>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? Right(this string? value, int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        return value is null || value.Length <= length ? value : value[^length..];
    }

    /// <summary>Returns the text before the first occurrence of <paramref name="delimiter"/>, or empty if not found.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? Before(this string? value, string delimiter, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(delimiter);
        if (value is null)
        {
            return null;
        }

        var index = value.IndexOf(delimiter, comparison);
        return index < 0 ? string.Empty : value[..index];
    }

    /// <summary>Returns the text before the last occurrence of <paramref name="delimiter"/>, or empty if not found.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? BeforeLast(this string? value, string delimiter, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(delimiter);
        if (value is null)
        {
            return null;
        }

        var index = value.LastIndexOf(delimiter, comparison);
        return index < 0 ? string.Empty : value[..index];
    }

    /// <summary>Returns the text after the first occurrence of <paramref name="delimiter"/>, or empty if not found.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? After(this string? value, string delimiter, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(delimiter);
        if (value is null)
        {
            return null;
        }

        var index = value.IndexOf(delimiter, comparison);
        return index < 0 ? string.Empty : value[(index + delimiter.Length)..];
    }

    /// <summary>Returns the text after the last occurrence of <paramref name="delimiter"/>, or empty if not found.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? AfterLast(this string? value, string delimiter, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(delimiter);
        if (value is null)
        {
            return null;
        }

        var index = value.LastIndexOf(delimiter, comparison);
        return index < 0 ? string.Empty : value[(index + delimiter.Length)..];
    }

    /// <summary>
    /// Returns the text between the first <paramref name="start"/> and the next <paramref name="end"/> after it,
    /// or empty if either is not found. Example: <c>"a[b]c".Between("[", "]")</c> returns <c>"b"</c>.
    /// </summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? Between(this string? value, string start, string end, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(start);
        ArgumentNullException.ThrowIfNull(end);
        if (value is null)
        {
            return null;
        }

        var startIndex = value.IndexOf(start, comparison);
        if (startIndex < 0)
        {
            return string.Empty;
        }

        startIndex += start.Length;
        var endIndex = value.IndexOf(end, startIndex, comparison);
        return endIndex < 0 ? string.Empty : value[startIndex..endIndex];
    }
}
