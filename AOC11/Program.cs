using System.Collections;
using System.Linq;

var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC11\\input11.txt").ToList();

Console.WriteLine($"Answer 1: {Run(20, c => c / 3)}");

var divisor = GetDivisor(lines);

Console.WriteLine($"Answer 2: {Run(10000, c => c % divisor)}");

long GetDivisor(List<string> lines)
{
    return lines
        .Where(l => l.Contains("Test: divisible by"))
        .Select(l => l.Split().Last())
        .Select(long.Parse)
        .Aggregate((acc, x) => acc * x);
}

long Run(int rounds, Func<long, long> reduce)
{
    var monkeys = lines.Chunk(7).Select(CreateMonkey).ToDictionary(s => s.monkeyId);

    var divisor = monkeys.Select(m => m.Value.divisor).Aggregate((acc, d) => acc * d);

    for (var i = 0; i < rounds; i++)
        foreach (var monkey in monkeys.Values.OrderBy(s => s.monkeyId))
        {
            var numItems = monkey.items.Count;

            for (var j = 0; j < numItems; j++)
            {
                var worry = reduce(monkey.operation(monkey.items.Dequeue()));

                monkeys[monkey.test(worry)].items.Enqueue(worry);
            }

            monkeys[monkey.monkeyId] = monkey with { numInspections = monkey.numInspections + numItems };
        }

    return monkeys
        .Select(m => m.Value.numInspections)
        .OrderByDescending(n => n)
        .Take(2)
        .Aggregate((acc, n) => acc * n);
}

Monkey CreateMonkey(string[] chunk)
{
    var monkeyId = int.Parse(chunk[0].Split().Last()[..^1]);
    var items = GetItems(chunk[1]);
    var operation = GetOperation(chunk[2]);
    var (divisor, onTrue, onFalse) = GetTest(chunk[3..]);

    Func<long, int> testFunc = a => (a % divisor == 0) ? onTrue : onFalse;

    return new Monkey(monkeyId, new Queue<long>(items), operation, testFunc, 0, divisor);
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

(long divisor, int onTrue, int onFalse)  GetTest(string[] strings)
{
    long divisor = long.Parse(strings[0].Trim().Split().Last());
    int onTrue = int.Parse(strings[1].Trim().Split().Last());
    int onFalse = int.Parse(strings[2].Trim().Split().Last());

    return (divisor, onTrue, onFalse);
}

record Monkey(int monkeyId, Queue<long> items, Func<long, long> operation, Func<long, int> test, long numInspections, long divisor);