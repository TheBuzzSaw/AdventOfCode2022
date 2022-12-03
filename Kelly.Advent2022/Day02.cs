using System;
using System.IO;

namespace Kelly.Advent2022;

static class Day02
{
    public static void Run(string inputFile)
    {
        var data = File.ReadAllBytes(inputFile);
        int index = 0;
        int score1 = 0;
        int score2 = 0;

        while (true)
        {
            var attack = FindValue('A');

            if (attack == -1)
                break;
            
            var defense = FindValue('X');

            var outcome1 = (defense - attack) switch
            {
                -2 => 6,
                -1 => 0,
                0 => 3,
                1 => 6,
                2 => 0,
                _ => throw new NotSupportedException("Not possible")
            };
            
            score1 += defense + 1 + outcome1;

            var outcome2 = (attack, defense) switch
            {
                (0, 0) => 3,
                (0, 1) => 1,
                (0, 2) => 2,
                (1, 0) => 1,
                (1, 1) => 2,
                (1, 2) => 3,
                (2, 0) => 2,
                (2, 1) => 3,
                (2, 2) => 1,
                (_, _) => throw new NotSupportedException("Not possible")
            };
            
            score2 += defense * 3 + outcome2;
        }

        Console.WriteLine(score1);
        Console.WriteLine(score2);

        int FindValue(int baseValue)
        {
            while (index < data.Length)
            {
                var value = data[index++] - baseValue;

                if (0 <= value && value <= 2)
                    return value;
            }

            return -1;
        }
    }
}