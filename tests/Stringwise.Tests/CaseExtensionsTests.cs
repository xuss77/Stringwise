namespace Stringwise.Tests;

public class CaseExtensionsTests
{
    [Theory]
    [InlineData("hello world", new[] { "hello", "world" })]
    [InlineData("helloWorld", new[] { "hello", "World" })]
    [InlineData("HelloWorld", new[] { "Hello", "World" })]
    [InlineData("hello_world-again now", new[] { "hello", "world", "again", "now" })]
    [InlineData("XMLHttpRequest", new[] { "XML", "Http", "Request" })]
    [InlineData("userID", new[] { "user", "ID" })]
    [InlineData("version2Update", new[] { "version2", "Update" })]
    [InlineData("HTML", new[] { "HTML" })]
    [InlineData("  __leading and trailing__  ", new[] { "leading", "and", "trailing" })]
    [InlineData("", new string[0])]
    [InlineData("---", new string[0])]
    public void SplitWords(string input, string[] expected)
        => Assert.Equal(expected, input.SplitWords());

    [Fact]
    public void SplitWords_Null_ReturnsEmpty()
        => Assert.Empty(((string?)null).SplitWords());

    [Theory]
    [InlineData("hello world", "helloWorld", "HelloWorld", "hello_world", "hello-world", "HELLO_WORLD")]
    [InlineData("XMLHttpRequest", "xmlHttpRequest", "XmlHttpRequest", "xml_http_request", "xml-http-request", "XML_HTTP_REQUEST")]
    [InlineData("user_id", "userId", "UserId", "user_id", "user-id", "USER_ID")]
    [InlineData("max-retry-count", "maxRetryCount", "MaxRetryCount", "max_retry_count", "max-retry-count", "MAX_RETRY_COUNT")]
    [InlineData("SOME_CONSTANT", "someConstant", "SomeConstant", "some_constant", "some-constant", "SOME_CONSTANT")]
    [InlineData("a", "a", "A", "a", "a", "A")]
    [InlineData("", "", "", "", "", "")]
    public void CaseConversions(string input, string camel, string pascal, string snake, string kebab, string screaming)
    {
        Assert.Equal(camel, input.ToCamelCase());
        Assert.Equal(pascal, input.ToPascalCase());
        Assert.Equal(snake, input.ToSnakeCase());
        Assert.Equal(kebab, input.ToKebabCase());
        Assert.Equal(screaming, input.ToScreamingSnakeCase());
    }

    [Fact]
    public void CaseConversions_AreRoundTripStable()
    {
        const string original = "orderLineItemId";

        Assert.Equal(original, original.ToSnakeCase().ToCamelCase());
        Assert.Equal(original, original.ToKebabCase().ToCamelCase());
        Assert.Equal(original, original.ToPascalCase().ToCamelCase());
    }

    [Fact]
    public void CaseConversions_AreUnaffectedByTurkishCulture()
    {
        using var _ = new CultureScope("tr-TR");

        Assert.Equal("fileId", "FILE_ID".ToCamelCase());
        Assert.Equal("ITEM_INDEX", "itemIndex".ToScreamingSnakeCase());
    }

    [Fact]
    public void CaseConversions_NullInput_ReturnsNull()
    {
        string? input = null;

        Assert.Null(input.ToCamelCase());
        Assert.Null(input.ToPascalCase());
        Assert.Null(input.ToSnakeCase());
        Assert.Null(input.ToKebabCase());
        Assert.Null(input.ToScreamingSnakeCase());
        Assert.Null(input.Capitalize());
    }

    [Theory]
    [InlineData("hello", "Hello")]
    [InlineData("Hello", "Hello")]
    [InlineData("hELLO", "HELLO")]
    [InlineData("1abc", "1abc")]
    [InlineData("", "")]
    public void Capitalize(string input, string expected)
        => Assert.Equal(expected, input.Capitalize());
}
