using Xunit.Abstractions;

namespace HallsByra.Code128.Tests;

public class DecoderFacts(ITestOutputHelper output)
{
    [Theory]
    [InlineData("HELLO1234567890", "�HELLO1234567890c�")]
    [InlineData("ABCDEFGHIJKLM", "�ABCDEFGHIJKLM8�")]
    [InlineData("NOPQRSTUVWXYZ", "�NOPQRSTUVWXYZj�")]
    [InlineData("0123456789", "�0123456789n�")]
    [InlineData("abcdefghijklm", "�abcdefghijklmT�")]
    [InlineData("nopqrstuvwxyz", "�nopqrstuvwxyz��")]
    [InlineData("1000002", "�1000002T�")]
    [InlineData("1748578", "�1748578-�")]
    [InlineData("1782669", "�1782669'�")]
    public void Decode_decodes_some_strings_into_expected_output(string expected, string encoded)
    {
        var decoded = Code128Decoder.Decode(encoded);
        output.WriteLine($"Input: {encoded}, Result: {decoded}");
        Assert.Equal(expected, decoded);
    }
}