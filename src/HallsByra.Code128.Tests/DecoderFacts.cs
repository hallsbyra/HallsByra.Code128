using Xunit.Abstractions;

namespace HallsByra.Code128.Tests;

public class DecoderFacts(ITestOutputHelper output)
{
    [Theory]
    [InlineData("HELLO1234567890", "ÃHELLO1234567890cŒ")]
    [InlineData("ABCDEFGHIJKLM", "ÃABCDEFGHIJKLM8Œ")]
    [InlineData("NOPQRSTUVWXYZ", "ÃNOPQRSTUVWXYZjŒ")]
    [InlineData("0123456789", "Ã0123456789nŒ")]
    [InlineData("abcdefghijklm", "ÃabcdefghijklmTŒ")]
    [InlineData("nopqrstuvwxyz", "Ãnopqrstuvwxyz Œ")]
    [InlineData("1000002", "Ã1000002TŒ")]
    [InlineData("1748578", "Ã1748578-Œ")]
    [InlineData("1782669", "Ã1782669'Œ")]
    public void Decode_decodes_some_strings_into_expected_output(string expected, string encoded)
    {
        var decoded = Code128Decoder.Decode(encoded);
        output.WriteLine($"Input: {encoded}, Result: {decoded}");
        Assert.Equal(expected, decoded);
    }
}