using System;
using System.Collections.Generic;
using System.Text;

namespace SDES
{
    public class LibrarySDES
    {
        public LibrarySDES() { }

        public List<int> ConvertStringToIntList(string inputText)
        {
            List<int> output = new List<int>();
            for (int i = 0; i < inputText.Length; i++)
            {
                output.Add((int)Char.GetNumericValue(inputText[i]));
            }
            return output;
        }

        public string ConvertListOfIntToString(List<int> inputList)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var val in inputList)
            {
                builder.Append(val);
            }
            return builder.ToString();
        }

        public List<int> InitialPermutation(List<int> inputList)
        {
            List<int> output = new List<int>();
            output.Add(inputList[3]);
            output.Add(inputList[0]);
            output.Add(inputList[2]);
            output.Add(inputList[4]);
            output.Add(inputList[6]);
            output.Add(inputList[1]);
            output.Add(inputList[7]);
            output.Add(inputList[5]);
            return output;
        }

        public List<int> PermutateRightFourBits(List<int> inputList)
        {
            List<int> output = new List<int>();
            output.Add(inputList[3]);
            output.Add(inputList[0]);
            output.Add(inputList[1]);
            output.Add(inputList[2]);
            output.Add(inputList[1]);
            output.Add(inputList[2]);
            output.Add(inputList[3]);
            output.Add(inputList[0]);
            return output;
        }

        public List<int> FirstExorEvaluation(List<int> listFromStables, List<int> inputKey1)
        {
            List<int> output = new List<int>();
            for (int i = 0; i < inputKey1.Count; i++)
            {
                var tempVal = 0;
                if (listFromStables[i] == 0 && inputKey1[i] == 1)
                    tempVal = 1;
                else if (listFromStables[i] == 1 && inputKey1[i] == 0)
                    tempVal = 1;
                else
                    tempVal = 0;

                output.Add(tempVal);
            }
            return output;
        }

        public List<int> EvaluateThroughS0Table(List<int> inputList)
        {
            List<int> output = new List<int>();
            string[,] sZeroTable = new string[,]
            {
                { "01", "00", "11", "10" },
                { "11", "10", "01", "00" },
                { "00", "10", "01", "11" },
                { "11", "01", "11", "10" }
            };

            var rowNum = 0;
            var colNum = 0;
            List<int> rowList = new List<int>();
            rowList.Add(inputList[0]);
            rowList.Add(inputList[3]);
            var rowIdxStr = ConvertListOfIntToString(rowList);
            if (rowIdxStr.Equals("00"))
                rowNum = 0;
            else if (rowIdxStr.Equals("01"))
                rowNum = 1;
            else if (rowIdxStr.Equals("10"))
                rowNum = 2;
            else
                rowNum = 3;

            List<int> colList = new List<int>();
            colList.Add(inputList[1]);
            colList.Add(inputList[2]);
            var colIdxStr = ConvertListOfIntToString(colList);

            if (colIdxStr.Equals("00"))
                colNum = 0;
            else if (colIdxStr.Equals("01"))
                colNum = 1;
            else if (colIdxStr.Equals("10"))
                colNum = 2;
            else
                colNum = 3;

            var valueTable = sZeroTable[rowNum, colNum];
            output = ConvertStringToIntList(valueTable);

            return output;
        }

        public List<int> EvaluateThroughS1Table(List<int> inputList)
        {
            List<int> output = new List<int>();
            string[,] sOneTable = new string[,]
            {
                { "00", "01", "11", "10" },
                { "10", "00", "01", "11" },
                { "11", "00", "01", "00" },
                { "10", "01", "00", "11" }
            };

            var rowNum = 0;
            var colNum = 0;
            List<int> rowList = new List<int>();
            rowList.Add(inputList[0]);
            rowList.Add(inputList[3]);
            var rowIdxStr = ConvertListOfIntToString(rowList);
            if (rowIdxStr.Equals("00"))
                rowNum = 0;
            else if (rowIdxStr.Equals("01"))
                rowNum = 1;
            else if (rowIdxStr.Equals("10"))
                rowNum = 2;
            else
                rowNum = 3;

            List<int> colList = new List<int>();
            colList.Add(inputList[1]);
            colList.Add(inputList[2]);
            var colIdxStr = ConvertListOfIntToString(colList);

            if (colIdxStr.Equals("00"))
                colNum = 0;
            else if (colIdxStr.Equals("01"))
                colNum = 1;
            else if (colIdxStr.Equals("10"))
                colNum = 2;
            else
                colNum = 3;

            var valueTable = sOneTable[rowNum, colNum];
            output = ConvertStringToIntList(valueTable);

            return output;
        }

        public List<int> CombineTwoLists(List<int> inputList1, List<int> inputList2)
        {
            List<int> output = inputList1;
            output.AddRange(inputList2);
            return output;
        }

        public List<int> PermutateFourBitsFromSTables(List<int> inputList)
        {
            List<int> output = new List<int>();
            output.Add(inputList[3]);
            output.Add(inputList[0]);
            output.Add(inputList[2]);
            output.Add(inputList[1]);
            return output;
        }

        public List<int> FinalExorEvaluation(List<int> leftInitList, List<int> outputPtwotList)
        {
            List<int> output = new List<int>();
            for (int i = 0; i < leftInitList.Count; i++)
            {
                var tempVal = 0;
                if (leftInitList[i] == 0 && outputPtwotList[i] == 1)
                    tempVal = 1;
                else if (leftInitList[i] == 1 && outputPtwotList[i] == 0)
                    tempVal = 1;
                else
                    tempVal = 0;

                output.Add(tempVal);
            }
            return output;
        }

        public List<int> FinalPermutation(List<int> inputList)
        {
            List<int> output = new List<int>();
            output.Add(inputList[1]);
            output.Add(inputList[4]);
            output.Add(inputList[2]);
            output.Add(inputList[0]);
            output.Add(inputList[5]);
            output.Add(inputList[7]);
            output.Add(inputList[3]);
            output.Add(inputList[6]);
            return output;
        }
    }
}
