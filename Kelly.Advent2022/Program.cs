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

                for (int i = 0; i < 2; ++i)
                {
                    if (mostCalories[i + 1] < mostCalories[i])
                    {
                        var swapValue = mostCalories[i + 1];
                        mostCalories[i + 1] = mostCalories[i];
                        mostCalories[i] = swapValue;
                    }
                }
            }
        }
    }
}