using System;
using System.Collections.Generic;
using System.IO;

namespace Kelly.Advent2022;

static class Day03
{
    public static void Run(string inputFile)
    {
        // Not only does this dodge allocating an array for every single line in the file,
        // I have a nice mutable buffer to shuffle values around in.
        var bytes = File.ReadAllBytes(inputFile);
        var triads = new List<(int s, int f)>();

        int lineNumber = 0;
        int prioritySum1 = 0;
        int prioritySum2 = 0;

        // This does mean I have to find the line boundaries myself, but oh well. Not hard.
        int start = 0;

        while (true)
        {
            ++lineNumber;

            while (start < bytes.Length && !Tools.IsAlpha(bytes[start]))
                ++start;
            
            if (bytes.Length <= start)
                break;

            int finish = start + 1;

            while (finish < bytes.Length && Tools.IsAlpha(bytes[finish]))
                ++finish;
            
            var line = bytes.AsSpan(start, finish - start);
            var commonValue1 = GetCommonValue(line);
            var priority1 = GetPriority(commonValue1);
            prioritySum1 += priority1;

            triads.Add((start, finish));
            if (triads.Count == 3)
            {
                var commonValue2 = GetCommonValue(
                    bytes.AsSpan(triads[0].s, triads[0].f - triads[0].s),
                    bytes.AsSpan(triads[1].s, triads[1].f - triads[1].s),
                    bytes.AsSpan(triads[2].s, triads[2].f - triads[2].s));
                var priority2 = GetPriority(commonValue2);
                prioritySum2 += priority2;
                triads.Clear();
            }
            
            start = finish + 1;
        }

        Console.WriteLine("Part 1: " + prioritySum1);
        Console.WriteLine("Part 2: " + prioritySum2);
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

    static int GetCommonValue(Span<byte> line1, Span<byte> line2, Span<byte> line3)
    {
        line2.Sort();
        line3.Sort();

        foreach (var value in line1)
        {
            if (0 <= line2.BinarySearch(value) && 0 <= line3.BinarySearch(value))
                return value;
        }

        throw new AdventException("No common value found.");
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