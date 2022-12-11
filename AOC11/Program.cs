using System.Collections;
using System.Linq;

//var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC11\\test.txt").Chunk(7);
var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC11\\input11.txt").Chunk(7);

var monkeys = lines.Select(CreateMonkey).ToDictionary(s => s.monkeyId);

var divisor = monkeys.Select(m => m.Value.divisor).Aggregate((acc, d) => acc * d);

for (var i = 0; i < 10000; i++)
foreach(var monkey in monkeys.Values.OrderBy(s => s.monkeyId))
{
    var numItems = monkey.items.Count;

    for (var j = 0; j < numItems; j++)
    {
        var worry = monkey.operation(monkey.items.Dequeue()) % divisor;

        monkeys[monkey.test(worry)].items.Enqueue(worry);
    }

    monkeys[monkey.monkeyId] = monkey with { numInspections = monkey.numInspections + numItems };
}

var result = monkeys.ToList()
    .Select(m => m.Value.numInspections)
    .OrderByDescending(n => n)
    .Take(2)
    .Aggregate((acc, n) => acc * n);

Console.WriteLine($"Answer 2: {result}");

Monkey CreateMonkey(string[] chunk)
{
    var monkeyId = int.Parse(chunk[0].Split().Last()[..^1]);
    var items = GetItems(chunk[1]);
    var operation = GetOperation(chunk[2]);
    var test = GetTest(chunk[3..]);

    return new Monkey(monkeyId, new Queue<long>(items), operation, test.func, 0, test.divisor);
}

List<long> GetItems(string input)
{
    return $"{input.Trim()},".Split().Skip(2).Select(s => long.Parse(s[..^1])).ToList();
}

Func<long, long> GetOperation(string input)
{
    var args = input.Trim().Split().TakeLast(3).ToList();

    switch (args)
    {
        case ["old", "*", "old"]:
            return c => c * c;
        case ["old", "*", var a]:
            {
                var b = long.Parse(a);
                return c => c * b;
            }
        case ["old", "+", var a]:
            {
                var b = long.Parse(a);
                return c => c + b;
            }
        default:
            throw new NotImplementedException();
    }
}

(Func<long, int> func, long divisor)  GetTest(string[] strings)
{
    long divisor = long.Parse(strings[0].Trim().Split().Last());
    int onTrue = int.Parse(strings[1].Trim().Split().Last());
    int onFalse = int.Parse(strings[2].Trim().Split().Last());

    Func<long, int> func = (c => (c % divisor == 0) ? onTrue : onFalse);

    return (func, divisor);
}

record Monkey(int monkeyId, Queue<long> items, Func<long, long> operation, Func<long, int> test, long numInspections, long divisor);