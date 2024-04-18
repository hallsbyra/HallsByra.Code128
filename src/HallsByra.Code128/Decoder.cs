using static HallsByra.Code128.Globals;

namespace HallsByra.Code128;

public class Code128Decoder
{
    private static readonly Dictionary<string, Code> GlyphsToCodes = allCodes.ToDictionary(c => c.Glyph, c => c);

    /// <summary>
    /// Decodes code 128 glyphs into a string.
    /// </summary>
    /// <param name="encoded">The previously encoded code 128 string of glyphs.</param>
    /// <returns>Decoded symbols.</returns>
    public static string Decode(string encoded)
    {
        static Code Glyph2Code(char c) => GlyphsToCodes.TryGetValue(c.ToString(), out var code)
            ? code
            : throw new Exception($"Illegal character in input: {c}");

        Span<Code> codes = [.. encoded.Select(Glyph2Code)];
        return Decode(codes);
    }
    private static string Decode(Span<Code> codes)
    {
        if (codes is [var start, .. var content, var checksum, var stop])
        {
            if (stop != StopCode)
                throw new Exception("Input is not terminated by a stop code");
            if (checksum != Checksum.Calculate([start, .. content]))
                throw new Exception("Checksum mismatch.");
            return Decode("", content, CodesetByStartCode(start));
        }
        else
            throw new Exception("Input is too short.");
    }

    private static CodeSet CodesetByStartCode(Code start) =>
          start == CodeSetA.Start ? codeSetA
        : start == CodeSetB.Start ? codeSetB
        : start == CodeSetC.Start ? codeSetC
        : throw new Exception("Invalid start code");

    private static string Decode(string decoded, Span<Code> encoded, CodeSet codeSet) => (encoded.Length, codeSet) switch
    {
        (0, _) => decoded,
        (_, CodeSetA) when encoded[0] == CodeSetA.SwitchToB => Decode(decoded, encoded[1..], codeSetB),
        (_, CodeSetA) when encoded[0] == CodeSetA.SwitchToC => Decode(decoded, encoded[1..], codeSetC),
        (_, CodeSetA) when encoded[0] == CodeSetA.Shift => Decode(decoded + codeSetB.Code2Symbol[encoded[1]], encoded[2..], codeSetA),
        (_, CodeSetB) when encoded[0] == CodeSetB.SwitchToA => Decode(decoded, encoded[1..], codeSetA),
        (_, CodeSetB) when encoded[0] == CodeSetB.SwitchToC => Decode(decoded, encoded[1..], codeSetC),
        (_, CodeSetB) when encoded[0] == CodeSetB.Shift => Decode(decoded + codeSetA.Code2Symbol[encoded[1]], encoded[2..], codeSetB),
        (_, CodeSetC) when encoded[0] == CodeSetC.SwitchToA => Decode(decoded, encoded[1..], codeSetA),
        (_, CodeSetC) when encoded[0] == CodeSetC.SwitchToB => Decode(decoded, encoded[1..], codeSetB),
        _ => Decode(decoded + codeSet.Code2Symbol[encoded[0]], encoded[1..], codeSet)
    };
}
