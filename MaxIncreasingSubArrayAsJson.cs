using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;


    public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
            return JsonSerializer.Serialize(new List<int>());

        List<int> bestSubarray = new List<int>();
        List<int> currentSubarray = new List<int>();

        foreach (int number in numbers)
        {
            if (currentSubarray.Count == 0 || number > currentSubarray.Last())
            {
                currentSubarray.Add(number);
            }
            else
            {
                if (currentSubarray.Sum() > bestSubarray.Sum())
                    bestSubarray = new List<int>(currentSubarray);

                currentSubarray.Clear();
                currentSubarray.Add(number);
            }
        }

        if (currentSubarray.Sum() > bestSubarray.Sum())
            bestSubarray = new List<int>(currentSubarray);

        return JsonSerializer.Serialize(bestSubarray);
    }

