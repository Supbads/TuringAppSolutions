using System;
using System.Collections.Generic;

class Solution 
{
    public static int MinimumConcat(string initial, string goal)
    {
        bool isGoalCosntructable = IsGoalConstructable(initial, goal);

        if (!isGoalCosntructable)
        {
            return -1;
        }

        int initialSize = initial.Length;
        int goalLength = goal.Length;
        int result = 0;

        for (int goalOffset = 0; goalOffset < goalLength;)
        {
            if(goalOffset + initialSize > goalLength)
            {
                initialSize = goalLength - (goalOffset);
            }

            var goalSubstring = goal.Substring(goalOffset, initialSize);
            var maximumGoalCoverage = GetMaximumCoverage(initial.ToCharArray(), goalSubstring.ToCharArray());
            goalOffset += maximumGoalCoverage;
            result++;
        }

        //check if all characters are present for both strings
        // take N = initial.Count chars from goal
        // check them , if they work skip next N otherwise check N - 1
        // offset position in goal
        // repeat step 2, 3, 4
        
        return result;
    }
    
    private static int GetMaximumCoverage(char[] initial, char[] goalSubstring)
    {
        int result = 0;
        int initialOffset = 0;
        
        for (int i = 0; i < goalSubstring.Length; i++)
        {
            bool foundCharacter = false;

            var goalChar = goalSubstring[i];

            for (int j = initialOffset; j < initial.Length; j++)
            {
                initialOffset = j;
                if (initial[j] == goalChar)
                {
                    foundCharacter = true;
                    
                    break;
                }
            }
            if (foundCharacter)
            {
                result++;
            }
            else
            {
                break;
            }
        }

        return result;
    }

    private static bool IsGoalConstructable(string initial, string goal)
    {
        HashSet<char> initialChars = new HashSet<char>();

        foreach (var initialChar in initial.ToCharArray())
        {
            initialChars.Add(initialChar);
        }

        foreach (char goalChar in goal.ToCharArray())
        {
            if (!initialChars.Contains(goalChar))
            {
                return false;
            }
        }

        return true;
    }
}

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var initial = Console.ReadLine();
            var goal = Console.ReadLine();
            var minimumConcat = Solution.MinimumConcat(initial, goal);
            Console.WriteLine(minimumConcat);
        }
    }
}
