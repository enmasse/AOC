using System.Collections;
using System.Linq;

//var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC11\\test.txt").Chunk(7);
var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC11\\input11.txt").Chunk(7);

var monkeys = lines.Select(CreateMonkey).ToDictionary(s => s.monkeyId);

for (var i = 0; i < 20; i++)
foreach(var monkey in monkeys.Values.OrderBy(s => s.monkeyId))
{
    var numItems = monkey.items.Count;

    for (var j = 0; j < numItems; j++)
    {
        var worry = monkey.operation(monkey.items.Dequeue()) / 3;

        monkeys[monkey.test(worry)].items.Enqueue(worry);
    }

    monkeys[monkey.monkeyId] = monkey with { numInspections = monkey.numInspections + numItems };
}

var result = monkeys.ToList()
    .Select(m => m.Value.numInspections)
    .OrderByDescending(m => m)
    .Take(2)
    .Aggregate((acc, s) => acc * s);

Console.WriteLine($"Answer 1: {result}");

Monkey CreateMonkey(string[] chunk)
{
    var monkeyId = int.Parse(chunk[0].Split().Last()[..^1]);
    var items = GetItems(chunk[1]);
    var operation = GetOperation(chunk[2]);
    var test = GetTest(chunk[3..]);

    return new Monkey(monkeyId, new Queue<int>(items), operation, test, 0);
}

List<int> GetItems(string input)
{
    return $"{input.Trim()},".Split().Skip(2).Select(s => int.Parse(s[..^1])).ToList();
}

Func<int, int> GetOperation(string input)
{
    var args = input.Trim().Split().TakeLast(3).ToList();

    switch (args)
    {
        case ["old", "*", "old"]:
            return c => c * c;
        case ["old", "*", var a]:
            {
                var b = int.Parse(a);
                return c => c * b;
            }
        case ["old", "+", var a]:
            {
                var b = int.Parse(a);
                return c => c + b;
            }
        default:
            throw new NotImplementedException();
    }
}

Func<int, int> GetTest(string[] strings)
{
    int divisor = int.Parse(strings[0].Trim().Split().Last());
    int onTrue = int.Parse(strings[1].Trim().Split().Last());
    int onFalse = int.Parse(strings[2].Trim().Split().Last());

    return c => (c % divisor == 0) ? onTrue : onFalse;
}

record Monkey(int monkeyId, Queue<int> items, Func<int, int> operation, Func<int, int> test, int numInspections);