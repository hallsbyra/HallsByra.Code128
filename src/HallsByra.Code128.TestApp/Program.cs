// See https://aka.ms/new-console-template for more information
using HallsByra.Code128;
using System.Diagnostics;


Console.WriteLine("Encoding...");

var allSymbols = "0123456789ABCDEFGHIJKKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
var rand = new Random();
var sw = Stopwatch.StartNew();
var count = 100000;
for (int i = 0; i < count; i++)
{
    var randomInput = string.Join("", rand.GetItems(allSymbols.AsSpan(), rand.Next(0, 100)));
    var encoded = Code128Encoder.Encode(randomInput);
}
Console.WriteLine($"Encoded {count} strings in {sw.ElapsedMilliseconds} ms.");

