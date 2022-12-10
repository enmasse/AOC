var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC9\\input9.txt");

IEnumerable<char> Expand(IEnumerable<string> lines)
{
    foreach(var line in lines)
    {
        var c = line[0];
        var count = int.Parse(line[2..]);

        for (var i = 0; i < count; i++)
        { yield return c;}
    }
}

var expand = Expand(lines);

IEnumerable<Position> GetPositions(IEnumerable<char> moves)
{
    var pos = new Position(0, 0);

    yield return pos;

    foreach (var m in moves)
    {
        switch (m)
        {
            case 'U':
                pos = pos with { y = pos.y + 1 };
                yield return pos;
                break;
            case 'D':
                pos = pos with { y = pos.y - 1 };
                yield return pos;
                break;
            case 'L':
                pos = pos with { x = pos.x - 1 };
                yield return pos;
                break;
            case 'R':
                pos = pos with { x = pos.x + 1 };
                yield return pos;
                break;
            default:
                break;
        }
    }
}

var positions = GetPositions(expand);

IEnumerable<Position> GetTailPositions(IEnumerable<Position> head)
{
    var enumerator = head.GetEnumerator();

    enumerator.MoveNext();
    var tail = enumerator.Current;
    yield return tail;

    while (enumerator.MoveNext())
    {
        var newpos = enumerator.Current;

        if (Math.Abs(tail.x - newpos.x) > 1 || Math.Abs(tail.y - newpos.y) > 1)
        {
            tail = UpdatePosition(tail, newpos);
            yield return tail;
        }
    }
}

Position UpdatePosition(Position tail, Position newpos)
{
    var stepx = (newpos.x - tail.x) switch
    {
        0 => 0,
        > 0 => 1,
        < 0 => -1
    };

    var stepy = (newpos.y - tail.y) switch
    {
        0 => 0,
        > 0 => 1,
        < 0 => -1
    };

    return tail with { x = tail.x + stepx, y = tail.y + stepy };
}

var tailPositions = GetTailPositions(positions);

var sumPositions = tailPositions.Distinct().Count();

Console.WriteLine($"Answer 1: {sumPositions}");

int CalculateTailVisitedPositions(IEnumerable<Position> positions, int ropeLength)
{
    var tailPositions = positions;

    for (var i = 1; i < ropeLength; i++)
    {
        tailPositions = GetTailPositions(tailPositions);
    }

    return tailPositions.Distinct().Count();
}

Console.WriteLine($"Answer 2: {CalculateTailVisitedPositions(positions, 10)}");

for (var i = 1; i <= 500; i++)
{
    Console.WriteLine($"Rope length {i}: {CalculateTailVisitedPositions(positions, i)}");
}

record Position(int x, int y);