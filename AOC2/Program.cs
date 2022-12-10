using System.Xml.Serialization;

var lines = File.ReadLines("C:\\Users\\mats.alritzson\\source\\repos\\AOC\\AOC2\\input2.txt");

var hands = lines
    .Select(line =>
    {
        Hand a = (Hand)(line[0] - 'A' + 1);
        Hand b = (Hand)(line[2] - 'X' + 1);

        return (a, b);
    }).ToList();

var score = hands
    .Sum(hand => calcScore(hand.a, hand.b));

Console.WriteLine($"Answer part 1: {score}");

var score2 = hands
    .Select(hand => resolveHands(hand.a, hand.b))
    .Sum(hand => calcScore(hand.a, hand.b));

Console.WriteLine($"Answer part 2: {score2}");

(Hand a, Hand b) resolveHands(Hand a, Hand b) =>
    (a, (Outcome)b) switch
    {
        (Hand.Rock, Outcome.Lose) => (a, Hand.Scissors),
        (Hand.Rock, Outcome.Win) => (a, Hand.Paper),
        (Hand.Paper, Outcome.Lose) => (a, Hand.Rock),
        (Hand.Paper, Outcome.Win) => (a, Hand.Scissors),
        (Hand.Scissors, Outcome.Lose) => (a, Hand.Paper),
        (Hand.Scissors, Outcome.Win) => (a, Hand.Rock),
        (var x, _) => (x, x),
    };

int calcScore(Hand a, Hand b) =>
    (int)((a, b) switch
    {
        (Hand.Rock, Hand.Paper) => 2,
        (Hand.Rock, Hand.Scissors) => 0,
        (Hand.Paper, Hand.Rock) => 0,
        (Hand.Paper, Hand.Scissors) => 2,
        (Hand.Scissors, Hand.Rock) => 2,
        (Hand.Scissors, Hand.Paper) => 0,
        _ => 1
    } * 3 + b);

enum Hand
{
    Rock = 1,
    Paper = 2,
    Scissors = 3,
};

enum Outcome
{
    Lose = 1,
    Draw = 2,
    Win = 3,
};