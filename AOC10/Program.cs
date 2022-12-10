using System.Data;

var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC10\\input10.txt");
//var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC10\\test.txt");

IEnumerable<int> GetValuesForEachCycle(IEnumerable<string> lines)
{
    int x = 1;

    foreach (var line in lines)
    {
        if (line[..4] == "addx")
        {
            yield return x;
            yield return x;
            x += int.Parse(line[4..]);
        }
        else if (line == "noop")
        {
            yield return x;
        }
        else
        {
            Console.WriteLine($"Strange line: {line}");
        }
    }
}

var values = GetValuesForEachCycle(lines).ToList();

var sum = 0;

for(int i = 20; i < 230; i += 40)
{
    sum += values[i - 1] * i;
}

Console.WriteLine($"Answer 1: {sum}");

char[] CreateSpriteMask(int position)
{
    var positions = new[] { position - 1, position, position + 1 };
    var output = new char[40];

    for (int i = 0; i < output.Length; i++)
    {
        if (positions.Contains(i))
        {
            output[i] = '#';
        }
        else
        {
            output[i] = '.';
        }
    }

    return output;
}

var masks = values.Select(CreateSpriteMask).ToList();

for (int c = 0; c < masks.Count; c++)
{
    if (c % 40 == 0)
    {
        Console.WriteLine("");
    }

    Console.Write(masks[c][c % 40]);
}