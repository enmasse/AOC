var grid = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC8\\input8.txt").ToArray();
//var grid = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC8\\test.txt").ToArray();

var height = grid.Length;
var width = grid[0].Length;

var visible = new bool[height, width];

for (var row = 0; row < height; row++)
{
    var prev = '0' - 1;
    for (var col = 0; col < width; col++)
    {
        var current = grid[row][col];
        if (current > prev)
        {
            visible[row, col] = true;
            prev = current;
        }
    }

    prev = '0' - 1;
    for (var col = width - 1; col > 0; col--)
    {
        var current = grid[row][col];
        if (current > prev)
        {
            visible[row, col] = true;
            prev = current;
        }
    }
}

for (var col = 0; col < width; col++)
{
    var prev = '0' - 1;
    for (var row = 0; row < height; row++)
    {
        var current = grid[row][col];
        if (current > prev)
        {
            visible[row, col] = true;
            prev = current;
        }
    }

    prev = '0' - 1;
    for (var row = height - 1; row > 0; row--)
    {
        var current = grid[row][col];
        if (current > prev)
        {
            visible[row, col] = true;
            prev = current;
        }
    }
}

var sumVisible = 0;

for (var row = 0; row < height; row++)
{
    for (var col = 0; col < width; col++)
    {
        if (visible[row, col])
        { sumVisible++; }
    }
}

Console.WriteLine($"Answer 1: {sumVisible}");

var scores = new int[height, width];

for (var row = 0; row < height; row++)
{
    for (var col = 0; col < width; col++)
    {
        scores[row, col] = CalculateScore(row, col, grid);
    }
}

var maxScore = (from int score in scores select score).Max();

Console.WriteLine($"Answer 2: {maxScore}");

int CalculateScore(int row, int col, string[] grid)
{
    var up = 0;
    var down = 0;
    var left = 0;
    var right = 0;
    var current = grid[row][col];

    for (var r = row - 1; r >= 0; r--)
    {
        up++;
        if (grid[r][col] >= current)
        { break; }
    }

    for (var r = row + 1; r < height; r++)
    {
        down++;
        if (grid[r][col] >= current)
        { break; }
    }

    for (var c = col - 1; c >= 0; c--)
    {
        left++;
        if (grid[row][c] >= current)
        { break; }
    }

    for (var c = col + 1; c < width; c++)
    {
        right++;
        if (grid[row][c] >= current)
        { break; }
    }

    return up * down * left * right;
}