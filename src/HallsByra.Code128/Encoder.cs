using static HallsByra.Code128.Globals;

namespace HallsByra.Code128;

public static class Code128Encoder
{
    // Represents a possible candidate for the string so far.
    // LeftToEncode: the part of the input string that remains to be encoded.
    // Codes: the codes that have been used so far to encode the input string.
    // CurrentCodeSet: the code set that is currently being used.
    record Candidate(string LeftToEncode, Code[] Codes, CodeSet CurrentCodeSet);

    /// <summary>
    /// Encodes the input string into a Code128 barcode.
    /// </summary>
    /// <param name="input">The input symboles to be encoded.</param>
    /// <returns>Glyphs to be rendered in Libre Barcode 128</returns>
    public static string Encode(string input)
    {
        // The initial set of candidates.
        Candidate[] candidates =
        [
            new (input, [CodeSetA.Start], codeSetA),
            new (input, [CodeSetB.Start], codeSetB),
            new (input, [CodeSetC.Start], codeSetC),
        ];

        while (true)
        {
            if (candidates.Length == 0)
                throw new Exception("There is no way to encode the input string.");

            // Are we finished?
            var finished = candidates.Where(c => c.LeftToEncode.Length == 0).ToArray();
            if (finished.Length > 0)
                return PrepareWinner(finished);

            // Explore the stragglers (candidates with the longest left to encode)
            (var stragglers, var rest) = ExtractStragglers(candidates);
            var newCandidates = stragglers.SelectMany(ExploreCandidate);
            candidates = [.. rest, .. newCandidates];

            // Prune the candidates
            candidates = PruneCandidates(candidates);
        }
    }

    // Elect a winner among the finished candidates, calculate checksum and convert to string
    private static string PrepareWinner(Candidate[] finishedCandidates)
    {
        var bestCandidate = finishedCandidates.OrderBy(c => c.Codes.Length).First();
        var checkSumCode = Checksum.Calculate(bestCandidate.Codes);
        Code[] codes = [.. bestCandidate.Codes, checkSumCode, StopCode];
        var result = string.Join("", codes.Select(c => c.Glyph));
        return result;
    }

    private static (IEnumerable<Candidate> stragglers, IEnumerable<Candidate> rest) ExtractStragglers(Candidate[] candidates)
    {
        var maxLen = candidates.Max(c => c.LeftToEncode.Length);
        return candidates.SplitBy(c => c.LeftToEncode.Length == maxLen);
    }

    private static Candidate[] PruneCandidates(Candidate[] candidates)
    {
        // For each current code set, keep only the candidates with the shortest code length
        var prunedCandidates = from c in candidates
                               group c by c.CurrentCodeSet into g
                               select g.OrderBy(c => c.Codes.Length).First();
        return prunedCandidates.ToArray();
    }

    // Given a candidate, explore the possible next steps from here.
    private static IEnumerable<Candidate> ExploreCandidate(Candidate candidate) => candidate.CurrentCodeSet switch
    {
        CodeSetA => ExploreFromA(candidate),
        CodeSetB => ExploreFromB(candidate),
        CodeSetC => ExploreFromC(candidate),
        _ => throw new Exception("Invalid code set")
    };

    private static IEnumerable<Candidate> ExploreFromA(Candidate candidate)
    {
        // Stay in A (always more preferable than switching to B)
        if (codeSetA.TryMatch(candidate.LeftToEncode) is Match aMatch)
            yield return new Candidate(aMatch.LeftToEncode, [.. candidate.Codes, aMatch.Code], codeSetA);
        else if (codeSetB.TryMatch(candidate.LeftToEncode) is Match bMatch)
        {
            // Shift to B
            yield return new Candidate(bMatch.LeftToEncode, [.. candidate.Codes, CodeSetA.Shift, bMatch.Code], codeSetA);
            // Switch to B
            yield return new Candidate(bMatch.LeftToEncode, [.. candidate.Codes, CodeSetA.SwitchToB, bMatch.Code], codeSetB);
        }
        // Switch to C
        if (codeSetC.TryMatch(candidate.LeftToEncode) is Match cMatch)
            yield return new Candidate(cMatch.LeftToEncode, [.. candidate.Codes, CodeSetA.SwitchToC, cMatch.Code], codeSetC);
    }

    private static IEnumerable<Candidate> ExploreFromB(Candidate candidate)
    {
        // Stay in B (always more preferable than switching to A)
        if (codeSetB.TryMatch(candidate.LeftToEncode) is Match bMatch)
            yield return new Candidate(bMatch.LeftToEncode, [.. candidate.Codes, bMatch.Code], codeSetB);
        else if (codeSetA.TryMatch(candidate.LeftToEncode) is Match aMatch)
        {
            // Shift to A
            yield return new Candidate(aMatch.LeftToEncode, [.. candidate.Codes, CodeSetB.Shift, aMatch.Code], codeSetB);
            // Switch to A
            yield return new Candidate(aMatch.LeftToEncode, [.. candidate.Codes, CodeSetB.SwitchToA, aMatch.Code], codeSetA);
        }
        // Switch to C
        if (codeSetC.TryMatch(candidate.LeftToEncode) is Match cMatch)
            yield return new Candidate(cMatch.LeftToEncode, [.. candidate.Codes, CodeSetB.SwitchToC, cMatch.Code], codeSetC);
    }

    private static IEnumerable<Candidate> ExploreFromC(Candidate candidate)
    {
        // Stay in C (always more preferable than switching to A/B)
        if (codeSetC.TryMatch(candidate.LeftToEncode) is Match cMatch)
            yield return new Candidate(cMatch.LeftToEncode, [.. candidate.Codes, cMatch.Code], codeSetC);
        else
        {
            // Switch to A
            if (codeSetA.TryMatch(candidate.LeftToEncode) is Match aMatch)
                yield return new Candidate(aMatch.LeftToEncode, [.. candidate.Codes, CodeSetC.SwitchToA, aMatch.Code], codeSetA);
            // Switch to B
            if (codeSetB.TryMatch(candidate.LeftToEncode) is Match bMatch)
                yield return new Candidate(bMatch.LeftToEncode, [.. candidate.Codes, CodeSetC.SwitchToB, bMatch.Code], codeSetB);
        }
    }
}