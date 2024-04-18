using System.Diagnostics;
using Xunit.Abstractions;

namespace HallsByra.Code128.Tests;

public class EncoderFacts(ITestOutputHelper output)
{
    [Theory]
    [InlineData("HELLO1234567890", "ÀHELLO«,BXnzfŒ")]
    [InlineData("ABCDEFGHIJKLM", "ÀABCDEFGHIJKLM7Œ")]
    [InlineData("NOPQRSTUVWXYZ", "ÀNOPQRSTUVWXYZiŒ")]
    [InlineData("0123456789", "Õ!7McyiŒ")]
    [InlineData("abcdefghijklm", "ÃabcdefghijklmTŒ")]
    [InlineData("nopqrstuvwxyz", "Ãnopqrstuvwxyz Œ")]
    [InlineData("1000002", "Õ*¬¬…2~Œ")]
    [InlineData("01AB2ad2BC2133", "Õ!»AB2ad2BC«5ARŒ")]
    [InlineData("\u0000", "À``Œ")]
    [InlineData("\u0000a", "À`∆aKŒ")]
    [InlineData("07", "Õ')Œ")]
    public void Encode_encodes_some_strings_into_expected_output(string input, string expected)
    {
        var encoded = Code128Encoder.Encode(input);
        Assert.Equal(expected, encoded);
    }

    [Fact]
    public void Encode_throws_if_input_cannot_be_encoded()
    {
        Assert.Throws<Exception>(() => Code128Encoder.Encode("Ä"));
    }

    [Fact]
    public void Encode_properly_encodes_and_decodes_a_large_number_of_random_strings()
    {
        string[] allSymbols = [.. Globals.codeSetA.Symbol2Code.Keys, .. Globals.codeSetB.Symbol2Code.Keys, .. Globals.codeSetC.Symbol2Code.Keys];
        string[] uniqueSymbols = new HashSet<string>(allSymbols).ToArray();
        var rand = new Random();
        for (int i = 0; i < 10000; i++)
        {
            var randomInput = string.Join("", rand.GetItems(uniqueSymbols, rand.Next(0, 100)));
            var encoded = Code128Encoder.Encode(randomInput);
            var decoded = Code128Decoder.Decode(encoded);
            Assert.Equal(randomInput, decoded);
        }
    }

    [Fact]
    public void Encode_performance()
    {
        string[] allSymbols = [.. Globals.codeSetA.Symbol2Code.Keys, .. Globals.codeSetB.Symbol2Code.Keys, .. Globals.codeSetC.Symbol2Code.Keys];
        string[] uniqueSymbols = new HashSet<string>(allSymbols).ToArray();
        var rand = new Random(1);
        var sw = Stopwatch.StartNew();
        for (int i = 0; i < 10000; i++)
        {
            var randomInput = string.Join("", rand.GetItems(uniqueSymbols, rand.Next(0, 100)));
            var encoded = Code128Encoder.Encode(randomInput);
        }
        output.WriteLine($"Encoded 10000 strings in {sw.ElapsedMilliseconds} ms.");
    }


    // Run this to generaet random input for the compatibility test.
    //
    //public void GenerateRandomInput()
    //{
    //    var allSymbols = "0123456789ABCDEFGHIJKKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    //    var rand = new Random();
    //    var inputLines = Enumerable.Range(0, 10000).Select(_ => string.Join("", rand.GetItems(allSymbols.AsSpan(), rand.Next(0, 100)))).ToArray();
    //    File.WriteAllText("new-input.txt", string.Join("\n", inputLines));
    //    output.WriteLine($"Generated {inputLines.Length} random strings.");
    //}


    // Runs large number of random strings through the encoder and compares the output with the expected output.
    [Fact]
    public void CompatibilityTest()
    {
        var inputLines = File.ReadAllLines("sample-input.txt");
        output.WriteLine($"Read {inputLines.Length} random strings.");
        var sw = Stopwatch.StartNew();
        var encodedLines = inputLines.Select(Code128Encoder.Encode).ToArray();
        sw.Stop();
        output.WriteLine($"Encoded {inputLines.Length} strings in {sw.ElapsedMilliseconds} ms.");
        output.WriteLine("Total encoded length: " + encodedLines.Sum(l => l.Length));

        File.WriteAllLines("new-output.txt", encodedLines);
        var expectedOutput = File.ReadAllLines("sample-expected.txt");
        for (int i = 0; i < inputLines.Length; i++)
        {
            Assert.Equal(expectedOutput[i], encodedLines[i]);
        }
    }

    // Can be used to print all printable symbols and their encoded values.
    // Handy for manually checking (with e.g. Word) that all output is correct and can be scanned.
    [Fact]
    public void EncodeAllPrintableSymbols()
    {
        var symbolRows = Definition.PrintableSymbols.Chunk(10).Select(cs => string.Join("", cs)).ToArray();
        foreach (var symbolRow in symbolRows)
        {
            var encoded = Code128Encoder.Encode(symbolRow);
            output.WriteLine($"{symbolRow} -> {encoded}");
        }
    }
}