var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC5\\input5.txt").ToList();

var init = new Stack<char>[] { new(), new(), new(), new(), new(), new(), new(), new(), new() };

var stacks = lines
    .TakeWhile(l => l[0] == '[')
    .Select(getLine)
    .Reverse()
    .Aggregate(init, assembleCrates);

var instructions = lines
    .Where(l => l.Length > 0 && l[0] == 'm')
    .Select(getArguments)
    .ToList();

var result1 = instructions
    .Aggregate(stacks, move1)
    .Aggregate(string.Empty, present);

Console.WriteLine($"Answer 1: {result1}");

stacks = lines
    .TakeWhile(l => l[0] == '[')
    .Select(getLine)
    .Reverse()
    .Aggregate(init, assembleCrates);

var result2 = instructions
    .Aggregate(stacks, move2)
    .Aggregate(string.Empty, present);

Console.WriteLine($"Answer 2: {result2}");

Stack<char>[] move2(Stack<char>[] acc, (int amount, int from, int to) o)
{
    var s = new Stack<char>();

    for (int i = 0; i < o.amount; i++)
    {
        var val = acc[o.from - 1].Pop();
        s.Push(val);
    }

    for (int i = 0; i < o.amount; i++)
    {
        var val = s.Pop();
        acc[o.to - 1].Push(val);
    }

    return acc;
}

string present(string acc, Stack<char> s)
{
    return $"{acc}{s.Pop()}";
}

Stack<char>[] move1(Stack<char>[] acc, (int amount, int from, int to) o)
{
    for (int i = 0; i < o.amount; i++)
    {
        var val = acc[o.from - 1].Pop();
        acc[o.to - 1].Push(val);
    }

    return acc;
}

(int amount, int from, int to) getArguments(string s)
{
    var w = s.Split(' ');
    var amount = int.Parse(w[1]);
    var from = int.Parse(w[3]);
    var to = int.Parse(w[5]);

    return (amount, from, to);
}

Stack<char>[] assembleCrates(Stack<char>[] acc, string s)
{
    for (int i = 0; i < s.Length; i++)
    {
        if (s[i] != ' ')
        {
            acc[i].Push(s[i]);
        }
    }

    return acc;
}

// [Q] [D] [P] [L] [V] [D] [D] [C] [Z]
string getLine(string a)
{
    int[] idxs = new[] { 1, 5, 9, 13, 17, 21, 25, 29, 33 };

    return idxs.Aggregate(string.Empty, (acc, e) => $"{acc}{a[e]}");
}