namespace Kelly.Advent2022;

static class Tools
{
    public static bool IsOdd(int n) => (n & 1) == 1;
    public static bool IsEven(int n) => (n & 1) == 0;

    public static bool NeverDescends(int a, int b, int c) => a <= b && b <= c;
    public static bool IsAlpha(int c) => NeverDescends('a', c, 'z') || NeverDescends('A', c, 'Z');
}