using System;
using System.Collections.Generic;

namespace SDES
{
    public class KeyGenerator
    {
        public String GenerateKeys(String input)
        {

            // Permutation 1
            var permuteOne = new List<Char>
            {
                input[2],
                input[4],
                input[1],
                input[6],
                input[3],
                input[9],
                input[0],
                input[8],
                input[7],
                input[5]
            };

            var resultKey = string.Join("", permuteOne.ToArray());

            var keyPartOne = resultKey.Substring(0, 5);
            var keyPartTwo = resultKey.Substring(5, 5);

            keyPartOne = LeftShiftString(keyPartOne);
            keyPartTwo = LeftShiftString(keyPartTwo);

            var keyTwo = GenerateKeyTwo(keyPartOne, keyPartTwo);

            var keyPartOneString = keyPartOne.ToString().PadLeft(5, '0');
            var keyPartTwoString = keyPartTwo.ToString().PadLeft(5, '0');

            var keyOne = keyPartOneString + keyPartTwoString;

            var permuteTwo = new List<Char>
            {
                keyOne[5],
                keyOne[2],
                keyOne[6],
                keyOne[3],
                keyOne[7],
                keyOne[4],
                keyOne[9],
                keyOne[8]
            };

            keyOne = string.Join("", permuteTwo.ToArray());

            return keyOne + keyTwo;
        }

        public static string LeftShiftString(string t)
        {
            return t.Substring(1, t.Length - 1) + t.Substring(0, 1);
        }

        private String GenerateKeyTwo(String inputOne, String inputTwo)
        {
            var keyPartOne = LeftShiftString(inputOne);
            keyPartOne = LeftShiftString(keyPartOne);

            var keyPartTwo = LeftShiftString(inputTwo);
            keyPartTwo = LeftShiftString(keyPartTwo);

            var keyTwo = keyPartOne + keyPartTwo;

            var permuteTwo = new List<Char>
            {
                keyTwo[5],
                keyTwo[2],
                keyTwo[6],
                keyTwo[3],
                keyTwo[7],
                keyTwo[4],
                keyTwo[9],
                keyTwo[8]
            };

            keyTwo = string.Join("", permuteTwo.ToArray());

            return keyTwo;
        }
    }
}
