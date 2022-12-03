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

            Console.WriteLine($"--- Running Day {day} ---");

            switch (day)
            {
                case 1:
                    Day01.Run(args[1]);
                    break;
                case 2:
                    Day02.Run(args[1]);
                    break;
                case 3:
                    Day03.Run(args[1]);
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
}