using System;
using System.IO;

namespace Kelly.Advent2022;

static class Day03
{
    public static void Run(string inputFile)
    {
        // Not only does this dodge allocating an array for every single line in the file,
        // I have a nice mutable buffer to shuffle values around in.
        var bytes = File.ReadAllBytes(inputFile);

        int prioritySum = 0;

        // This does mean I have to find the line boundaries myself, but oh well. Not hard.
        int startIndex = 0;

        while (true)
        {
            while (startIndex < bytes.Length && !Tools.IsAlpha(bytes[startIndex]))
                ++startIndex;
            
            if (bytes.Length <= startIndex)
                break;

            int endIndex = startIndex + 1;

            while (endIndex < bytes.Length && Tools.IsAlpha(bytes[endIndex]))
                ++endIndex;
            
            var line = bytes.AsSpan(startIndex, endIndex - startIndex);
            var commonValue = GetCommonValue(line);
            var priority = GetPriority(commonValue);
            prioritySum += priority;
            
            startIndex = endIndex + 1;
        }

        Console.WriteLine(prioritySum);
    }

    static int GetCommonValue(Span<byte> line)
    {
        if (Tools.IsOdd(line.Length))
            throw new AdventException("Each rucksack must have an even number of items.");
        
        var half = line.Length / 2;
        var left = line.Slice(0, half);
        var right = line.Slice(half);

        left.Sort();
        right.Sort();

        var leftIndex = 0;
        var rightIndex = 0;

        while (true)
        {
            if (left[leftIndex] == right[rightIndex])
                return left[leftIndex];
            
            if (right[rightIndex] < left[leftIndex])
            {
                if (++rightIndex == half)
                    throw new AdventException("No common value found.");
            }
            else if (++leftIndex == half)
            {
                throw new AdventException("No common value found.");
            }
        }
    }

    static int GetPriority(int c)
    {
        if (Tools.NeverDescends('a', c, 'z'))
            return c - 'a' + 1;
        
        if (Tools.NeverDescends('A', c, 'Z'))
            return c - 'A' + 27;
        
        throw new ArgumentOutOfRangeException(nameof(c));
    }
}