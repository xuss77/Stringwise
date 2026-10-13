using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Stringwise;

/// <summary>
/// Converts between naming conventions. Word boundaries are detected from separators
/// (spaces, punctuation, <c>_</c>, <c>-</c>), lower-to-upper transitions (<c>fooBar</c>)
/// and the end of acronyms (<c>XMLHttp</c> splits as <c>XML</c> + <c>Http</c>).
/// A <see langword="null"/> input always returns <see langword="null"/>.
/// </summary>
public static class CaseExtensions
{
    /// <summary>Converts to <c>camelCase</c>. Example: <c>"user ID number"</c> becomes <c>"userIdNumber"</c>.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? ToCamelCase(this string? value)
        => value is null ? null : JoinCapitalized(SplitWords(value), lowerFirst: true);

    /// <summary>Converts to <c>PascalCase</c>. Example: <c>"user_id-number"</c> becomes <c>"UserIdNumber"</c>.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? ToPascalCase(this string? value)
        => value is null ? null : JoinCapitalized(SplitWords(value), lowerFirst: false);

    /// <summary>Converts to <c>snake_case</c>. Example: <c>"XMLHttpRequest"</c> becomes <c>"xml_http_request"</c>.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? ToSnakeCase(this string? value)
        => value is null ? null : string.Join('_', SplitWords(value).Select(w => w.ToLowerInvariant()));

    /// <summary>Converts to <c>kebab-case</c>. Example: <c>"XMLHttpRequest"</c> becomes <c>"xml-http-request"</c>.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? ToKebabCase(this string? value)
        => value is null ? null : string.Join('-', SplitWords(value).Select(w => w.ToLowerInvariant()));

    /// <summary>Converts to <c>SCREAMING_SNAKE_CASE</c>. Example: <c>"maxRetryCount"</c> becomes <c>"MAX_RETRY_COUNT"</c>.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? ToScreamingSnakeCase(this string? value)
        => value is null ? null : string.Join('_', SplitWords(value).Select(w => w.ToUpperInvariant()));

    /// <summary>Upper-cases the first character and leaves the rest unchanged.</summary>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? Capitalize(this string? value)
        => string.IsNullOrEmpty(value) || char.IsUpper(value[0])
            ? value
            : string.Concat(char.ToUpperInvariant(value[0]).ToString(), value.AsSpan(1));

    /// <summary>Splits a string into words using the rules described on <see cref="CaseExtensions"/>.</summary>
    public static IReadOnlyList<string> SplitWords(this string? value)
    {
        var words = new List<string>();
        if (string.IsNullOrEmpty(value))
        {
            return words;
        }

        var start = -1;
        for (var i = 0; i < value.Length; i++)
        {
            var c = value[i];
            if (!char.IsLetterOrDigit(c))
            {
                if (start >= 0)
                {
                    words.Add(value[start..i]);
                    start = -1;
                }

                continue;
            }

            if (start < 0)
            {
                start = i;
                continue;
            }

            var prev = value[i - 1];
            var lowerToUpper = char.IsUpper(c) && (char.IsLower(prev) || char.IsDigit(prev));
            var acronymEnd = char.IsUpper(c) && char.IsUpper(prev)
                && i + 1 < value.Length && char.IsLower(value[i + 1]);
            if (lowerToUpper || acronymEnd)
            {
                words.Add(value[start..i]);
                start = i;
            }
        }

        if (start >= 0)
        {
            words.Add(value[start..]);
        }

        return words;
    }

    private static string JoinCapitalized(IReadOnlyList<string> words, bool lowerFirst)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < words.Count; i++)
        {
            var word = words[i].ToLowerInvariant();
            if (i == 0 && lowerFirst)
            {
                sb.Append(word);
                continue;
            }

            sb.Append(char.ToUpperInvariant(word[0])).Append(word.AsSpan(1));
        }

        return sb.ToString();
    }
}
