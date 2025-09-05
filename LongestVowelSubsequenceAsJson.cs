using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;


    public static string LongestVowelsSubsequenceAsJson(List<string> words)
    {

        if (words == null || words.Count == 0)
            return "[]";

        var results = new List<ResultItem>(words.Count);

        foreach (var word in words)
        {
            if (string.IsNullOrEmpty(word))
            {
                results.Add(new ResultItem(word ?? string.Empty, string.Empty, 0));
                continue;
            }

            int bestStart = -1, bestLen = 0;
            int curStart = -1, curLen = 0;

            for (int i = 0; i < word.Length; i++)
            {
                char c = word[i];

                if (IsVowel(c))
                {
                    if (curLen == 0) curStart = i;
                    curLen++;

                    if (curLen > bestLen)
                    {
                        bestLen = curLen;
                        bestStart = curStart;
                    }
                }
                else
                {
                    curLen = 0;
                }
            }

            string sequence = bestLen > 0 ? word.Substring(bestStart, bestLen) : string.Empty;
            results.Add(new ResultItem(word, sequence, bestLen));
        }

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        return JsonSerializer.Serialize(results, jsonOptions);
    }

    private static bool IsVowel(char c)
    {
        switch (char.ToLowerInvariant(c))
        {
            case 'a':
            case 'e':
            case 'i':
            case 'o':
            case 'u':
                return true;
            default:
                return false;
        }
    }

    private sealed record ResultItem(string Word, string Sequence, int Length);

