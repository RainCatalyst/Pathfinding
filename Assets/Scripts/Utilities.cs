using System;

public static class Utilities
{
    public static LetterState[] CompareWords(string source, string target)
    {
        var targetCount = GetLetterCount(target);
        // Assuming word lengths match
        var states = new LetterState[source.Length];
        for (int i = 0; i < source.Length; i++)
        {
            LetterState state;
            if (source[i] == target[i])
            {
                state = LetterState.Solved;
                targetCount[source[i]]--;
            }
            else if (targetCount[source[i]] > 0)
            {
                state = LetterState.WrongPlace;
                targetCount[source[i]]--;
            }
            else
            {
                state = LetterState.Unused;
            }

            states[i] = state;
        }

        return states;
    }

    public static byte[] GetLetterCount(string word)
    {
        var count = new byte[Char.MaxValue];
        foreach (char c in word)
            count[c]++;
        return count;
    }
}