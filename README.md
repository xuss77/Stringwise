# Stringwise

[![CI](https://github.com/xuss77/Stringwise/actions/workflows/ci.yml/badge.svg)](https://github.com/xuss77/Stringwise/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Stringwise.svg)](https://www.nuget.org/packages/Stringwise)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

**Predictable, culture-safe, null-safe string extensions for .NET.**

Stop rewriting `ToIntOrNull()` in every project. Stringwise gives you a small, carefully tested set of string helpers that behave the same on every machine.

```csharp
using Stringwise;

"42".ToIntOrNull();              // 42
"abc".ToIntOrNull();             // null
"abc".ToIntOrDefault(-1);        // -1
"1.5".ToDecimal();               // 1.5 — even on a German machine
"yes".ToBool();                  // true
"red".ToEnum<Color>();           // Color.Red
"key=value".After("=");          // "value"
"Crème Brûlée!".ToSlug();        // "creme-brulee"
"XMLHttpRequest".ToSnakeCase();  // "xml_http_request"
"Hello world".Truncate(8, "…");  // "Hello w…"
```

## Install

```bash
dotnet add package Stringwise
```

Requires .NET 10. Trimming and Native AOT compatible. No dependencies.

## Design principles

| Principle | What it means |
|---|---|
| **Consistent names** | `ToX()` throws · `ToXOrNull()` returns `null` · `ToXOrDefault(fallback)` returns a fallback · `TryTo<T>(out x)` |
| **Culture-safe by default** | Parsing uses `CultureInfo.InvariantCulture`. `"1.5"` is always 1.5, never 15. Pass an `IFormatProvider` when you *want* local formats. |
| **Null-safe** | `null` input never throws a `NullReferenceException`. Transforms return `null` for `null`, and nullable annotations tell the compiler exactly that. |
| **No surprises** | Slicing never throws on short strings. `Truncate` never splits an emoji. Case conversion ignores the Turkish-I problem. |
| **Analyzer-friendly** | Calling `"42".ToInt()` doesn't trigger CA1305 in your project. Culture-aware overloads live in a separate class, so the safe default stays warning-free. |

## API

### Parsing

Works for **any** `IParsable<T>` type via generics, plus typed shortcuts for the common ones.

```csharp
"42".To<int>();                       // throws if invalid
"42".ToOrNull<int>();                 // int?
"x".ToOrDefault(10);                  // 10
if ("42".TryTo(out int n)) { ... }

"127.0.0.1".To<IPAddress>();          // any IParsable<T> works
"1,5".ToDecimal(CultureInfo.GetCultureInfo("de-DE"));  // explicit culture
```

Typed shortcuts (each has `ToX`, `ToXOrNull`, `ToXOrDefault`):
`Int`, `Long`, `Short`, `Byte`, `Decimal`, `Double`, `Float`, `Guid`, `DateTime`, `DateTimeOffset`, `DateOnly`, `TimeOnly`, `TimeSpan`.

**Booleans** accept `true/false`, `yes/no`, `y/n`, `on/off`, `1/0`, `t/f` (case-insensitive, trimmed):

```csharp
"Yes".ToBool();            // true
"off".ToBoolOrNull();      // false
"maybe".ToBoolOrNull();    // null
```

**Enums** match names case-insensitively, support flag combinations, and reject numbers that aren't defined members:

```csharp
"blue".ToEnum<Color>();                // Color.Blue
"Read, Write".ToEnum<Access>();        // Access.Read | Access.Write
"42".ToEnumOrNull<Color>();            // null (Enum.TryParse would return (Color)42!)
"red".ToEnumOrNull<Color>(ignoreCase: false);  // null
```

### Checks

| Method | Example |
|---|---|
| `IsNullOrEmpty()` / `IsNullOrWhiteSpace()` | `name.IsNullOrWhiteSpace()` |
| `HasValue()` | `true` when there's at least one non-whitespace char |
| `IsDigitsOnly()` | `"0123".IsDigitsOnly()` → `true` (ASCII 0-9 only) |
| `IsLettersOnly()` | `"Ünï".IsLettersOnly()` → `true` |
| `IsNumeric()` | `"-3.5e2".IsNumeric()` → `true` |
| `ContainsIgnoreCase()` / `EqualsIgnoreCase()` / `StartsWithIgnoreCase()` / `EndsWithIgnoreCase()` | ordinal, null-safe |

### Slicing

None of these throw on short input. A missing delimiter returns `""`.

```csharp
"Hello".Left(3);                 // "Hel"
"Hello".Right(50);               // "Hello"
"a/b/c".Before("/");             // "a"
"a/b/c".BeforeLast("/");         // "a/b"
"a/b/c".After("/");              // "b/c"
"a/b/c".AfterLast("/");          // "c"
"f(x)".Between("(", ")");        // "x"
```

### Transforms

```csharp
"".NullIfEmpty();                        // null
"   ".NullIfWhiteSpace();                // null
"Hello world".Truncate(8, "…");          // "Hello w…"
"  a   b \n c ".CollapseWhitespace();    // "a b c"
" a b ".RemoveWhitespace();              // "ab"
"Crème brûlée".RemoveDiacritics();       // "Creme brulee"
"Straße & Café!".ToSlug();               // "strasse-cafe"
"ab😀".Reverse();                        // "😀ba" (grapheme-aware)
"ab".Repeat(3);                          // "ababab"
```

### Casing

Word boundaries come from separators, `lowerUpper` transitions and acronym ends (`XMLHttp` → `XML` + `Http`).

```csharp
"user ID number".ToCamelCase();          // "userIdNumber"
"user_id".ToPascalCase();                // "UserId"
"XMLHttpRequest".ToSnakeCase();          // "xml_http_request"
"XMLHttpRequest".ToKebabCase();          // "xml-http-request"
"maxRetryCount".ToScreamingSnakeCase();  // "MAX_RETRY_COUNT"
"hello".Capitalize();                    // "Hello"
"fooBarBaz".SplitWords();                // ["foo", "Bar", "Baz"]
```

## Building from source

```bash
dotnet test
dotnet run -c Release --project benchmarks/Stringwise.Benchmarks
```

## Contributing

Ideas and PRs welcome. Every new method needs:

1. XML docs with an example,
2. tests for `null`, empty, whitespace and edge cases, plus culture tests if it touches parsing or casing,
3. a name that follows the `ToX` / `ToXOrNull` / `ToXOrDefault` convention where it applies.

## License

[MIT](LICENSE)
