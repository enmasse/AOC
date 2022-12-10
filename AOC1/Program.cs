// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;

string[] lines = File.ReadAllLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC1\\input1.txt");

IEnumerable<ICollection<int>> chunkLines(IEnumerable<string> lines)
{
    var output = new List<int>();

    foreach(var line in lines)
    {
        if(int.TryParse(line, out int result))
        {
            output.Add(result);
        }
        else
        {
            var copy = output.ToList();
            output.Clear();
            yield return copy;
        }
    }
    yield return output;
}

var sums = chunkLines(lines)
    .Select(chunk => chunk.Sum())
    .ToList();

Console.WriteLine($"Answer part1: {sums.Max()}");

var topThree = sums
    .OrderByDescending(x => x)
    .Take(3)
    .Sum();

Console.WriteLine($"Answer part2: {topThree}");
