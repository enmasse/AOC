var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC3\\input3.txt");

var sumErrors = lines
    .Select(splitLine)
    .Select(l => findError(l.first, l.second))
    .Sum(prioritize);

Console.WriteLine($"Answer 1: {sumErrors}");

var sumErrors2 = lines
    .Chunk(3)
    .Select(findBadge)
    .Sum(prioritize);

Console.WriteLine($"Answer 2: {sumErrors2}");

char findBadge(string[] chunk)
{
    foreach (char c in chunk[0])
    {
        if (chunk[1].Contains(c) && chunk[2].Contains(c))
        {
            return c;
        }
    }

    throw new Exception("Badge not found");
}

int prioritize(char c) =>
    c switch
    {
        char a when a >= 'a' && a <= 'z' =>
            a - 'a' + 1,
        char b =>
            b - 'A' + 27 
    };

char findError(string first, string second)
{
    foreach (char c in first)
    {
        if (second.Contains(c))
        {
            return c;
        }
    }

    throw new Exception("No error found");
}

(string first, string second) splitLine(string line)
{
    var length = line.Length / 2;

    return (line[..length], line[length..]);
}
