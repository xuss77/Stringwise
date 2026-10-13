using System.Globalization;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Stringwise.Benchmarks;

internal static class Program
{
    private static void Main(string[] args)
        => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
}

[MemoryDiagnoser]
public class ParseBenchmarks
{
    private const string Valid = "123456";
    private const string Invalid = "12x456";

    [Benchmark(Baseline = true)]
    public int? BclTryParse()
        => int.TryParse(Valid, NumberStyles.Integer, CultureInfo.InvariantCulture, out var v) ? v : null;

    [Benchmark]
    public int? ToIntOrNull_Valid() => Valid.ToIntOrNull();

    [Benchmark]
    public int? ToIntOrNull_Invalid() => Invalid.ToIntOrNull();

    [Benchmark]
    public bool? ToBoolOrNull() => "yes".ToBoolOrNull();
}

[MemoryDiagnoser]
public class TransformBenchmarks
{
    private const string Title = "  Crème Brûlée & Café: The XMLHttpRequest Guide!  ";

    [Benchmark]
    public string ToSlug() => Title.ToSlug();

    [Benchmark]
    public string ToSnakeCase() => Title.ToSnakeCase();

    [Benchmark]
    public string CollapseWhitespace() => Title.CollapseWhitespace();

    [Benchmark]
    public string Truncate() => Title.Truncate(20, "…");
}
