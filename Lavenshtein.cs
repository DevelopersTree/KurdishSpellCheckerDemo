using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevenshteinDistance
{
    public static class Lavenshtein
    {
        // The three implentations are based on this article: https://en.wikipedia.org/wiki/Levenshtein_distance

        public static int Number = 0;
        public static int GetDistanceRecursive(char[] text1, int length1, char[] text2, int length2)
        {
            Number++;
            if (length1 == 0) return length2;
            if (length2 == 0) return length1;

            var cost = 0;

            if (text1[length1 - 1] == text2[length2 - 1])
                cost = 0;
            else
                cost = 1;

            return Min(GetDistanceRecursive(text1, length1 - 1, text2, length2) + 1,
                       GetDistanceRecursive(text1, length1, text2, length2 - 1) + 1,
                       GetDistanceRecursive(text1, length1 - 1, text2, length2 - 1) + cost);
        }

        public static int GetDistanceMatrix(string text1, string text2)
        {
            var matrix = new int[text1.Length, text2.Length];

            for (int i = 0; i < text1.Length; i++)
                matrix[i, 0] = i;

            for (int j = 0; j < text2.Length; j++)
                matrix[0, j] = j;

            for (int j = 1; j < text2.Length; j++)
            {
                for (int i = 1; i < text1.Length; i++)
                {
                    var substitutionCost = text1[i] == text2[j] ? 0 : 1;
                    matrix[i, j] = Min(matrix[i - 1, j] + 1,
                                       matrix[i, j - 1] + 1,
                                       matrix[i - 1, j - 1] + substitutionCost);
                }
            }

            return matrix[text1.Length - 1, text2.Length - 1];
        }

        public static int GetDistanceTwoRows(string s, string t)
        {
            var vector0 = new int[t.Length + 1];
            var vector1 = new int[t.Length + 1];
            int[] tempReference;

            for (int i = 0; i <= t.Length; i++)
                vector0[i] = i;

            for (int i = 0; i < s.Length; i++)
            {
                vector1[0] = i + 1;

                for (int j = 0; j < t.Length; j++)
                {
                    var substitutionCost = s[i] == t[j] ? 0 : 1;
                    vector1[j + 1] = Min(vector1[j] + 1,
                                         vector0[j + 1] + 1,
                                         vector0[j] + substitutionCost);
                }

                tempReference = vector0;
                vector0 = vector1;
                vector1 = tempReference;
            }

            return vector0[t.Length];
        }

        private static int Min(int number1, int number2, int number3)
        {
            if (number1 > number2)
            {
                return number2 > number3 ? number3 : number2;
            }
            else
            {
                return number1 > number3 ? number3 : number1;
            }
        }
    }
}
