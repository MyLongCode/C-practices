using System;
using System.Collections.Generic;

using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism
{
    public class LevenshteinCalculator
    {
        public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
        {
            var optimizationList = new List<ComparisonResult>();
            for (var i = 0; i < documents.Count; i++)
                for (var j = i; j < documents.Count - 1; j++)
                    optimizationList.Add(LevenshteinDistance(documents[i], documents[j + 1]));
            return optimizationList;
        }

        public static double[,] InitializeOptimization(int first, int second)
        {
            double[,] result = new double[first + 1, second + 1];
            for (int i = 0; i <= first; i++)
                result[i, 0] = i;
            for (int i = 0; i <= second; i++)
                result[0, i] = i;
            return result
        }

        public static ComparisonResult LevenshteinDistance(DocumentTokens first, DocumentTokens second)
        {
            var optimization = InitializeOptimization(first.Count + 1, second.Count + 1);
            for (int i = 1; i <= first.Count; i++)
            {
                for (int j = 1; j <= second.Count; j++)
                {
                    if (first[i - 1] == second[j - 1])
                    {
                        optimization[i, j] = optimization[i - 1, j - 1];
                    }
                    else
                    {
                        double distance = Math.Min(
                            TokenDistanceCalculator.GetTokenDistance(first[i - 1], second[j - 1])
                            + optimization[i - 1, j - 1],
                            1 + optimization[i, j - 1]);

                        optimization[i, j] = Math.Min(optimization[i - 1, j] + 1, distance);
                    }
                }
            }

            return new ComparisonResult(first, second, optimization[first.Count, second.Count]);
        }
    }
}

