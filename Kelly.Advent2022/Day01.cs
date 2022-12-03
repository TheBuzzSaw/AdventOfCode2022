using System;
using System.IO;
using System.Linq;

namespace Kelly.Advent2022;

static class Day01
{
    public static void Run(string inputFile)
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
}