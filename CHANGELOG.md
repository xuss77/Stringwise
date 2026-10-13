# Changelog

## 0.1.0 — Unreleased

Initial release.

- Generic parsing for any `IParsable<T>`: `To<T>`, `ToOrNull<T>`, `ToOrDefault<T>`, `TryTo<T>`
- Typed shortcuts for int, long, short, byte, decimal, double, float, Guid, DateTime, DateTimeOffset, DateOnly, TimeOnly, TimeSpan
- `ToBool` family (yes/no, on/off, 1/0, ...) and `ToEnum` family
- Checks: `IsNullOrEmpty`, `IsNullOrWhiteSpace`, `HasValue`, `IsDigitsOnly`, `IsLettersOnly`, `IsNumeric`, ignore-case comparisons
- Slicing: `Left`, `Right`, `Before`, `BeforeLast`, `After`, `AfterLast`, `Between`
- Transforms: `NullIfEmpty`, `NullIfWhiteSpace`, `Truncate`, `CollapseWhitespace`, `RemoveWhitespace`, `RemoveDiacritics`, `ToSlug`, `Reverse`, `Repeat`
- Casing: `ToCamelCase`, `ToPascalCase`, `ToSnakeCase`, `ToKebabCase`, `ToScreamingSnakeCase`, `Capitalize`, `SplitWords`
