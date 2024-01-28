using System;
using System.Collections.Generic;

namespace Antiplagiarism
{
    public static class LongestCommonSubsequenceCalculator
    {
        public static List<string> Calculate(List<string> first, List<string> second)
        {
            var optimizationList = InitializeOptimization(first, second);
            return initializeAnswer(optimizationList, first, second);
        }

        private static List<string> initializeAnswer(int[,] opt, List<string> first, List<string> second)
        {
            var resultAnswer = new List<string>();
            int x = 0;
            int y = 0;
            while (x < first.Count && y < second.Count)
            {
                if (first[x] == second[y])
                {
                    y++;
                    resultAnswer.Add(first[x]);
                    x++;
                }
                else if (opt[x, y] == opt[x + 1, y]) x++;
                else y++;
            }
            return resultAnswer;
        }

        private static int[,] InitializeOptimization(List<string> first, List<string> second)
        {
            var optimization = new int[first.Count + 1, second.Count + 1];
            for (int i = first.Count - 1; i >= 0; i--)
            {
                for (int j = second.Count - 1; j >= 0; j--)
                {
                    if (first[i] == second[j])
                        optimization[i, j] = optimization[i + 1, j + 1] + 1;
                    else
                        optimization[i, j] = Math.Max(optimization[i + 1, j], optimization[i, j + 1]);
                }
            }
            return optimization;
        }
    }
}
