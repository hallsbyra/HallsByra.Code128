using static HallsByra.Code128.Globals;

namespace HallsByra.Code128;

// A single Code with its ordinal value and the corresponding font glyph.
record struct Code(int Value, string Glyph);

// A mapping of an input symbol to a Code
record SymbolWithCode(string Symbol, Code Code);

static class Globals
{
    // All codes in the Code128 specification
    public static readonly Code[] allCodes = [.. Definition.Data.Select(r => new Code(r.Value, r.Glyph))];
    public static Code StopCode => allCodes[106];

    public static readonly CodeSetA codeSetA = new(Definition.CodeASymbolRows.Select(r => new SymbolWithCode(r.CodeA, allCodes[r.Value])).ToArray());
    public static readonly CodeSetB codeSetB = new(Definition.CodeBSymbolRows.Select(r => new SymbolWithCode(r.CodeB, allCodes[r.Value])).ToArray());
    public static readonly CodeSetC codeSetC = new(Definition.CodeCSymbolRows.Select(r => new SymbolWithCode(r.CodeC, allCodes[r.Value])).ToArray());
}

// Representation of Code Set A
class CodeSetA(SymbolWithCode[] symbolCodes) : CodeSet(symbolCodes, 1)
{
    public static readonly Code Shift = allCodes[98];
    public static readonly Code SwitchToB = allCodes[100];
    public static readonly Code SwitchToC = allCodes[99];
    public static readonly Code Start = allCodes[103];
}

// Representation of Code Set B
class CodeSetB(SymbolWithCode[] symbolCodes) : CodeSet(symbolCodes, 1)
{
    public static readonly Code Shift = allCodes[98];
    public static readonly Code SwitchToA = allCodes[101];
    public static readonly Code SwitchToC = allCodes[99];
    public static readonly Code Start = allCodes[104];
}

// Representation of Code Set C
class CodeSetC(SymbolWithCode[] symbolCodes) : CodeSet(symbolCodes, 2)
{
    public static readonly Code SwitchToA = allCodes[101];
    public static readonly Code SwitchToB = allCodes[100];
    public static readonly Code Start = allCodes[105];
}


// Represents a possible encoding of the next part of the input string using Code, leaving LeftToEncode unencoded.
record Match(string LeftToEncode, Code Code);

class CodeSet(SymbolWithCode[] symbolCodes, int symbolLength)
{
    public readonly Dictionary<string, Code> Symbol2Code = symbolCodes.ToDictionary(sc => sc.Symbol, sc => sc.Code);
    public readonly Dictionary<Code, string> Code2Symbol = symbolCodes.ToDictionary(sc => sc.Code, sc => sc.Symbol);

    // Try to produce a code that will encode the next part of the input string.
    public Match? TryMatch(string input) => 
        input.Length >= symbolLength && Symbol2Code.TryGetValue(input[0..symbolLength], out var c)
        ? new Match(input[symbolLength..], c)
        : null;
}

