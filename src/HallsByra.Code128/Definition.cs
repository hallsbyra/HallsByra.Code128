namespace HallsByra.Code128;

internal static class Definition
{
    public record Row(int Value, string CodeA, string CodeB, string CodeC, string Glyph);

    //
    // Code 128 data table. Used as input to create the codesets.
    // Taken from https://en.wikipedia.org/wiki/Code_128
    //
    public static readonly Row[] Data = [
        //     Value    CodeA       CodeB      CodeC  Glyph
        new (  0,        " ",        " ",       "00", "Â"),
        new (  1,        "!",        "!",       "01", "!"),
        new (  2,       "\"",       "\"",       "02", "\""),
        new (  3,        "#",        "#",       "03", "#"),
        new (  4,        "$",        "$",       "04", "$"),
        new (  5,        "%",        "%",       "05", "%"),
        new (  6,        "&",        "&",       "06", "&"),
        new (  7,        "'",        "'",       "07", "'"),
        new (  8,        "(",        "(",       "08", "("),
        new (  9,        ")",        ")",       "09", ")"),
        new ( 10,        "*",        "*",       "10", "*"),
        new ( 11,        "+",        "+",       "11", "+"),
        new ( 12,        ",",        ",",       "12", ","),
        new ( 13,        "-",        "-",       "13", "-"),
        new ( 14,        ".",        ".",       "14", "."),
        new ( 15,        "/",        "/",       "15", "/"),
        new ( 16,        "0",        "0",       "16", "0"),
        new ( 17,        "1",        "1",       "17", "1"),
        new ( 18,        "2",        "2",       "18", "2"),
        new ( 19,        "3",        "3",       "19", "3"),
        new ( 20,        "4",        "4",       "20", "4"),
        new ( 21,        "5",        "5",       "21", "5"),
        new ( 22,        "6",        "6",       "22", "6"),
        new ( 23,        "7",        "7",       "23", "7"),
        new ( 24,        "8",        "8",       "24", "8"),
        new ( 25,        "9",        "9",       "25", "9"),
        new ( 26,        ":",        ":",       "26", ":"),
        new ( 27,        ";",        ";",       "27", ";"),
        new ( 28,        "<",        "<",       "28", "<"),
        new ( 29,        "=",        "=",       "29", "="),
        new ( 30,        ">",        ">",       "30", ">"),
        new ( 31,        "?",        "?",       "31", "?"),
        new ( 32,        "@",        "@",       "32", "@"),
        new ( 33,        "A",        "A",       "33", "A"),
        new ( 34,        "B",        "B",       "34", "B"),
        new ( 35,        "C",        "C",       "35", "C"),
        new ( 36,        "D",        "D",       "36", "D"),
        new ( 37,        "E",        "E",       "37", "E"),
        new ( 38,        "F",        "F",       "38", "F"),
        new ( 39,        "G",        "G",       "39", "G"),
        new ( 40,        "H",        "H",       "40", "H"),
        new ( 41,        "I",        "I",       "41", "I"),
        new ( 42,        "J",        "J",       "42", "J"),
        new ( 43,        "K",        "K",       "43", "K"),
        new ( 44,        "L",        "L",       "44", "L"),
        new ( 45,        "M",        "M",       "45", "M"),
        new ( 46,        "N",        "N",       "46", "N"),
        new ( 47,        "O",        "O",       "47", "O"),
        new ( 48,        "P",        "P",       "48", "P"),
        new ( 49,        "Q",        "Q",       "49", "Q"),
        new ( 50,        "R",        "R",       "50", "R"),
        new ( 51,        "S",        "S",       "51", "S"),
        new ( 52,        "T",        "T",       "52", "T"),
        new ( 53,        "U",        "U",       "53", "U"),
        new ( 54,        "V",        "V",       "54", "V"),
        new ( 55,        "W",        "W",       "55", "W"),
        new ( 56,        "X",        "X",       "56", "X"),
        new ( 57,        "Y",        "Y",       "57", "Y"),
        new ( 58,        "Z",        "Z",       "58", "Z"),
        new ( 59,        "[",        "[",       "59", "["),
        new ( 60,       "\\",       "\\",       "60", "\\"),
        new ( 61,        "]",        "]",       "61", "]"),
        new ( 62,        "^",        "^",       "62", "^"),
        new ( 63,        "_",        "_",       "63", "_"),
        new ( 64,   "\u0000",        "`",       "64", "`"), // NUL             Null character       
        new ( 65,   "\u0001",        "a",       "65", "a"), // SOH / Ctrl-A    Start of Heading     
        new ( 66,   "\u0002",        "b",       "66", "b"), // STX / Ctrl-B    Start of Text        
        new ( 67,   "\u0003",        "c",       "67", "c"), // ETX / Ctrl-C1   End-of-text character        
        new ( 68,   "\u0004",        "d",       "68", "d"), // EOT / Ctrl-D2   End-of-transmission character        
        new ( 69,   "\u0005",        "e",       "69", "e"), // ENQ / Ctrl-E    Enquiry character        
        new ( 70,   "\u0006",        "f",       "70", "f"), // ACK / Ctrl-F    Acknowledge character        
        new ( 71,   "\u0007",        "g",       "71", "g"), // BEL / Ctrl-G3   Bell character       
        new ( 72,   "\u0008",        "h",       "72", "h"), // BS / Ctrl-H     Backspace        
        new ( 73,   "\u0009",        "i",       "73", "i"), // HT / Ctrl-I     Horizontal tab       
        new ( 74,   "\u000A",        "j",       "74", "j"), // LF / Ctrl-J4    Line feed        
        new ( 75,   "\u000B",        "k",       "75", "k"), // VT / Ctrl-K     Vertical tab     
        new ( 76,   "\u000C",        "l",       "76", "l"), // FF / Ctrl-L     Form feed        
        new ( 77,   "\u000D",        "m",       "77", "m"), // CR / Ctrl-M5    Carriage return      
        new ( 78,   "\u000E",        "n",       "78", "n"), // SO / Ctrl-N     Shift Out        
        new ( 79,   "\u000F",        "o",       "79", "o"), // SI / Ctrl-O6    Shift In     
        new ( 80,   "\u0010",        "p",       "80", "p"), // DLE / Ctrl-P    Data Link Escape     
        new ( 81,   "\u0011",        "q",       "81", "q"), // DC1 / Ctrl-Q7   Device Control 1     
        new ( 82,   "\u0012",        "r",       "82", "r"), // DC2 / Ctrl-R    Device Control 2     
        new ( 83,   "\u0013",        "s",       "83", "s"), // DC3 / Ctrl-S8   Device Control 3     
        new ( 84,   "\u0014",        "t",       "84", "t"), // DC4 / Ctrl-T    Device Control 4     
        new ( 85,   "\u0015",        "u",       "85", "u"), // NAK / Ctrl-U9   Negative-acknowledge character       
        new ( 86,   "\u0016",        "v",       "86", "v"), // SYN / Ctrl-V    Synchronous Idle     
        new ( 87,   "\u0017",        "w",       "87", "w"), // ETB / Ctrl-W    End of Transmission Block        
        new ( 88,   "\u0018",        "x",       "88", "x"), // CAN / Ctrl-X10  Cancel character     
        new ( 89,   "\u0019",        "y",       "89", "y"), // EM / Ctrl-Y     End of Medium        
        new ( 90,   "\u001A",        "z",       "90", "z"), // SUB / Ctrl-Z11  Substitute character     
        new ( 91,   "\u001B",        "{",       "91", "{"), // ESC             Escape character     
        new ( 92,   "\u001C",        "|",       "92", "|"), // FS              File Separator       
        new ( 93,   "\u001D",        "}",       "93", "}"), // GS              Group Separator      
        new ( 94,   "\u001E",        "~",       "94", "~"), // RS              Record Separator     
        new ( 95,   "\u001F",   "\u007F",       "95", "Ã"), // US, DEL         Unit Separator (A), Delete (B)
        new ( 96,  "<fnc 3>",  "<fnc 3>",       "96", "Ä"),  // TODO: Choose encoding for FNC
        new ( 97,  "<fnc 2>",  "<fnc 2>",       "97", "Å"),
        new ( 98,  "<Shift>",  "<Shift>",       "98", "Æ"),
        new ( 99,  "<CodeC>",  "<CodeC>",       "99", "Ç"),
        new (100,  "<CodeB>",  "<fnc 4>",  "<CodeB>", "È"),
        new (101,  "<fnc 4>",  "<codeA>",  "<CodeA>", "É"),
        new (102,  "<fnc 1>",  "<fnc 1>",  "<fnc 1>", "Ê"),
        new (103, "<StartA>", "<StartA>", "<StartA>", "Ë"),
        new (104, "<StartB>", "<StartB>", "<StartB>", "Ì"),
        new (105, "<StartC>", "<StartC>", "<StartC>", "Í"),
        new (106,   "<Stop>",   "<Stop>",   "<Stop>", "Î"),
    ];

    // The rows in the table that are used to encode symbols from the input string (as opposed to control codes such as <StartA>, etc.).
    public static Row[] CodeASymbolRows = [.. Data.Where(r => r.CodeA.Length == 1)];
    public static Row[] CodeBSymbolRows = [.. Data.Where(r => r.CodeB.Length == 1)];
    public static Row[] CodeCSymbolRows = [.. Data.Where(r => r.CodeC.Length == 2)];

    // All printable symbols that can be encoded.
    public static string PrintableSymbols => string.Join("",
        Data.Take(64).Select(r => r.CodeA)
        .Concat(Data.Take(95).Select(r => r.CodeB))
        .Distinct()
        .ToArray());
}

