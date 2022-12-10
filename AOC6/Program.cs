var chars = File.ReadAllText("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC6\\input6.txt");

Console.WriteLine($"Answer 1: {FindStart(chars, 4)}");
Console.WriteLine($"Answer 2: {FindStart(chars, 14)}");

int FindStart(string input, int length)
{
    int i = 0;

    for (; i < input.Length - length + 1; i++)
    {
        var marker = input[i..(i + length)];

        if (AllUnique(marker))
        { break; }
    }

    return i + length;
}

bool AllUnique(string marker) =>
    marker.Distinct().Count() == marker.Length;
