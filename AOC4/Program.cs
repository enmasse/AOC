var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC4\\input4.txt");

var ranges = lines
    .Select(l => l.Split(','))
    .Select(l => (getRange(l[0]), getRange(l[1])))
    .ToList();

var part1 = ranges
    .Count(l => subRangeOf(l.Item1, l.Item2) || subRangeOf(l.Item2, l.Item1));

Console.WriteLine($"Answer 1: {part1}");

var part2 = ranges
    .Count(r => overlaps(r.Item1, r.Item2));

Console.WriteLine($"Answer 2: {part2}");

bool overlaps(List<int> item1, List<int> item2)
{
    return item1.Any(i => item2.Contains(i));
}

bool subRangeOf(List<int> item1, List<int> item2) =>
    item1.All(i => item2.Contains(i));

List<int> getRange(string s)
{
    var p = s.Split('-');
    var start = int.Parse(p[0]);
    var end = int.Parse(p[1]);

    return Enumerable.Range(start, end - start + 1)
        .ToList();
}
