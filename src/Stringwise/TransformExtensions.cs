using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Stringwise;

/// <summary>
/// Extensions that produce a modified copy of a string.
/// A <see langword="null"/> input always returns <see langword="null"/>.
/// </summary>
public static class TransformExtensions
{
    /// <summary>Returns <see langword="null"/> if the string is empty; otherwise the string itself.</summary>
    public static string? NullIfEmpty(this string? value)
        => string.IsNullOrEmpty(value) ? null : value;

    /// <summary>Returns <see langword="null"/> if the string is empty or whitespace; otherwise the string itself.</summary>
    public static string? NullIfWhiteSpace(this string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value;

    /// <summary>
    /// Shortens the string to at most <paramref name="maxLength"/> characters, including <paramref name="ellipsis"/>.
    /// Never splits a surrogate pair (e.g. an emoji). Example: <c>"Hello world".Truncate(8, "…")</c> returns <c>"Hello w…"</c>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxLength"/> is negative or shorter than <paramref name="ellipsis"/>.</exception>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? Truncate(this string? value, int maxLength, string ellipsis = "")
    {
        ArgumentOutOfRangeException.ThrowIfNegative(maxLength);
        ArgumentNullException.ThrowIfNull(ellipsis);
        ArgumentOutOfRangeException.ThrowIfLessThan(maxLength, ellipsis.Length);

        if (value is null || value.Length <= maxLength)
        {
            return value;
        }

        var cut = maxLength - ellipsis.Length;
        if (cut > 0 && char.IsHighSurrogate(value[cut - 1]))
        {
            cut--;
        }

        return string.Concat(value.AsSpan(0, cut), ellipsis);
    }

    /// <summary>Trims the string and replaces every run of whitespace with a single space.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? CollapseWhitespace(this string? value)
    {
        if (value is null)
        {
            return null;
        }

        var sb = new StringBuilder(value.Length);
        var pendingSpace = false;
        foreach (var c in value.AsSpan().Trim())
        {
            if (char.IsWhiteSpace(c))
            {
                pendingSpace = true;
                continue;
            }

            if (pendingSpace)
            {
                sb.Append(' ');
                pendingSpace = false;
            }

            sb.Append(c);
        }

        return sb.ToString();
    }

    /// <summary>Removes every whitespace character from the string.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? RemoveWhitespace(this string? value)
    {
        if (value is null)
        {
            return null;
        }

        var sb = new StringBuilder(value.Length);
        foreach (var c in value)
        {
            if (!char.IsWhiteSpace(c))
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    /// <summary>Removes accents and other combining marks. Example: <c>"Crème brûlée"</c> becomes <c>"Creme brulee"</c>.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? RemoveDiacritics(this string? value)
    {
        if (value is null)
        {
            return null;
        }

        var decomposed = value.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(decomposed.Length);
        foreach (var c in decomposed)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    /// Converts the string to a URL-friendly slug: lowercase ASCII letters and digits joined by <paramref name="separator"/>.
    /// Example: <c>"Crème Brûlée &amp; Café!"</c> becomes <c>"creme-brulee-cafe"</c>.
    /// </summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? ToSlug(this string? value, char separator = '-')
    {
        if (value is null)
        {
            return null;
        }

        var sb = new StringBuilder(value.Length);
        var pendingSeparator = false;
        foreach (var c in value.RemoveDiacritics())
        {
            var lower = char.ToLowerInvariant(c);
            var mapped = char.IsAsciiLetterOrDigit(lower) ? lower.ToString() : Transliterate(lower);
            if (mapped is null)
            {
                pendingSeparator = sb.Length > 0;
                continue;
            }

            if (pendingSeparator)
            {
                sb.Append(separator);
                pendingSeparator = false;
            }

            sb.Append(mapped);
        }

        return sb.ToString();
    }

    /// <summary>Reverses the string by user-perceived character, so emoji and accented letters stay intact.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? Reverse(this string? value)
    {
        if (value is null || value.Length <= 1)
        {
            return value;
        }

        var elements = new List<string>();
        var enumerator = StringInfo.GetTextElementEnumerator(value);
        while (enumerator.MoveNext())
        {
            elements.Add(enumerator.GetTextElement());
        }

        elements.Reverse();
        return string.Concat(elements);
    }

    /// <summary>Repeats the string <paramref name="count"/> times. Example: <c>"ab".Repeat(3)</c> returns <c>"ababab"</c>.</summary>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? Repeat(this string? value, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        if (value is null)
        {
            return null;
        }

        return new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
    }

    // Letters that have no Unicode decomposition but have a common ASCII spelling.
    private static string? Transliterate(char c) => c switch
    {
        'ß' => "ss",
        'æ' => "ae",
        'œ' => "oe",
        'ø' => "o",
        'ł' => "l",
        'đ' => "d",
        'ð' => "d",
        'þ' => "th",
        _ => null,
    };
}
