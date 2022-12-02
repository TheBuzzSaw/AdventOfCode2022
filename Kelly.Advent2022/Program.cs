using System;
using System.IO;

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
        var mostCalories = 0;
        var currentCalories = 0;

        foreach (var line in lines)
        {
            if (int.TryParse(line, out var calories) && 0 < calories)
            {
                currentCalories += calories;
            }
            else if (mostCalories < currentCalories)
            {
                mostCalories = currentCalories;
                currentCalories = 0;
            }
            else
            {
                currentCalories = 0;
            }
        }

        if (mostCalories < currentCalories)
            mostCalories = currentCalories;
        
        Console.WriteLine(mostCalories);
    }
}