using static HallsByra.Code128.Globals;

namespace HallsByra.Code128;

static class Checksum
{
    public static Code Calculate(Span<Code> codes)
    {
        var checksum = (codes[0].Value + codes[1..].ToArray().Select((c, i) => c.Value * (i + 1)).Sum()) % 103;
        return allCodes[checksum];
    }
}
