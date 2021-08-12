using System;
using System.Collections.Generic;

public static class Utility
{
    public static IEnumerable<int> ToIntIds(this string[] input)
    {
        foreach (var item in input)
        {
            if (int.TryParse(item, out int num))
            {
                yield return num;
            }
        }
    }
}
