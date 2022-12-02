using System;
using System.IO;
using System.Linq;

namespace Kelly.Advent2022;

static class Program
{
    static void Main(string[] args)
    {
        try
        {
            var day = int.Parse(args[0]);

            switch (day)
            {
                case 1:
                    Day1(args[1]);
                    break;
                case 2:
                    Day2(args[1]);
                    break;
                default:
                    Console.WriteLine($"Day {day} is not implemented.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(ex);
            Console.WriteLine();
        }
    }

    static void Day1(string inputFile)
    {
        var lines = File.ReadAllLines(inputFile);
        var mostCalories = new int[3];
        var currentCalories = 0;

        foreach (var line in lines)
        {
            if (int.TryParse(line, out var calories) && 0 < calories)
            {
                currentCalories += calories;
            }
            else
            {
                BubbleUp();
                currentCalories = 0;
            }
        }
        
        BubbleUp();
        Console.WriteLine(string.Join(", ", mostCalories));
        Console.WriteLine(mostCalories.Sum());

        void BubbleUp()
        {
            if (mostCalories[0] < currentCalories)
            {
                mostCalories[0] = currentCalories;

                for (int i = 0; i < mostCalories.Length - 1; ++i)
                {
                    if (mostCalories[i + 1] < mostCalories[i])
                    {
                        var swapValue = mostCalories[i + 1];
                        mostCalories[i + 1] = mostCalories[i];
                        mostCalories[i] = swapValue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    static void Day2(string inputFile)
    {
        var data = File.ReadAllBytes(inputFile);
        int index = 0;
        int score = 0;

        while (true)
        {
            var attack = FindValue('A');

            if (attack == -1)
                break;
            
            var defense = FindValue('X');

            var outcome = (defense - attack) switch
            {
                -2 => 6,
                -1 => 0,
                0 => 3,
                1 => 6,
                2 => 0,
                _ => throw new NotSupportedException("Not possible")
            };
            score += defense + 1 + outcome;
        }

        Console.WriteLine(score);

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