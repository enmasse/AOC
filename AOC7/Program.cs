using System.Linq;
using System.Runtime.InteropServices;

var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC7\\input7.txt").Skip(1);
//var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC7\\test.txt").Skip(1);

var root = new Dictionary<string, object>();

PopulateDirectory(root, lines.GetEnumerator());

int sum = 0;

foreach (var node in root)
{
    if (node.Value is Dictionary<string, object>)
    {
        ForEachDirectory((Dictionary<string, object>)node.Value, node.Key, (key, a) =>
        {
            var size = CalculateSize(a);
            Console.WriteLine($"Directory {key} has size {size}");

            if (size < 100000)
            { sum += size; }
        });
    }
}

Console.WriteLine($"Answer 1: {sum}");

List<int> sizes = new();

ForEachDirectory(root, "/", (key, a) =>
{
    sizes.Add(CalculateSize(a));
});

var max = sizes.Max();
var free = 70000000 - max;
var needed = 30000000;
var candidates = sizes
    .Where(s => s + free > needed);

Console.WriteLine($"Answer 2: {candidates.Min()}");

void ForEachDirectory(Dictionary<string, object> node, string name, Action<string, Dictionary<string, object>> action)
{
    action(name, node);

    foreach(var n in node)
    {
        if (n.Value is Dictionary<string, object>)
        {
            ForEachDirectory((Dictionary<string, object>)n.Value, n.Key, action);
        }
    }
}

void PopulateDirectory(Dictionary<string, object> node, IEnumerator<string> enumerator)
{
    while(enumerator.MoveNext())
    {
        var current = enumerator.Current.Split(' ');

        switch (current)
        {
            case var a when a[0] == "dir":
                node.TryAdd(a[1], new Dictionary<string, object>());
                break;
            case var a when int.TryParse(a[0], out int size):
                node.TryAdd(a[1], new FileType(size));
                break;
            case var a when $"{a[0]}{a[1]}" == "$ls":
                break;
            case var a when $"{a[0]}{a[1]}{a[2]}" == "$cd..":
                return;
            case var a when $"{a[0]}{a[1]}" == "$cd":
                PopulateDirectory((Dictionary<string, object>)node[a[2]], enumerator);
                break;
            default:
                break;
        }
    }
}

int CalculateSize(Dictionary<string, object> node)
{
    return node
        .ToList()
        .Select(n => n.Value switch
        {
            FileType a => a.size,
            Dictionary<string, object> a => CalculateSize(a),
            _ => 0
        })
        .Cast<int>()
        .Sum();
}

record FileType(int size);