using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDES
{
   public  class EncryptionSDES
    {
        public EncryptionSDES() { }
        public string EncryptionMode(string key1, string key2, string plainTextFromUser)
        {
            return Encryption(key1, key2, plainTextFromUser);
        }

        private string Encryption(string key1, string key2, string plainText)
        {
            List<int> keyOneList = ConvertStringToIntList(key1);
            List<int> keyTwoList = ConvertStringToIntList(key2);
            List<int> initText = ConvertStringToIntList(plainText);

            // first permutation 
            List<int> outputPinit = InitialPermutation(initText);

            // split outputPinit to two four-bits blocks
            List<int> pinitLeftFourBits = new List<int>();
            List<int> pinitRightFourBits = new List<int>();
            for (int i = 0; i < initText.Count; i++){
                var tempVal = outputPinit[i];
                if (i < 4)
                    pinitLeftFourBits.Add(tempVal);
                else 
                    pinitRightFourBits.Add(tempVal);
            }

            //  expanding the right-four-bits
            List<int> outputPone = PermutateRightFourBits(pinitRightFourBits);

            // XOR the outputPone with key1
            List<int> outputXor = FirstExorEvaluation(outputPone, keyOneList);

            // split outputXor into two  
            List<int> xorLeftFourBits = new List<int>();
            List<int> xorRightFourBits = new List<int>();
            for (int i = 0; i < outputXor.Count; i++) {
                var tempVal = outputXor[i];
                if (i < 4)
                    xorLeftFourBits.Add(tempVal);
                else
                    xorRightFourBits.Add(tempVal);
            }

            // evaluate with S-Zero and S-One tables
            List<int> outputFromSZeroTab = EvaluateThroughS0Table(xorLeftFourBits);
            List<int> outputFromSOneTab = EvaluateThroughS1Table(xorRightFourBits);

            // combine outputFromSZeroTab and outputFromSOneTab
            List<int> inputForP2 = CombineTwoLists(outputFromSZeroTab, outputFromSOneTab);

            // permutate the two results from S tables 
            List<int> outputPtwo = PermutateFourBitsFromSTables(inputForP2);

            // finalize first LEFT PROCESS by xor
            List<int> outputFirstProcess = FinalExorEvaluation(pinitLeftFourBits, outputPtwo);

            // start SECOND ROUND process
            List<int> expandedOutputFirstProcess = PermutateRightFourBits(outputFirstProcess);

            // xor with Key2
            List<int> outputXorRight = FirstExorEvaluation(expandedOutputFirstProcess, keyTwoList);

            // split outputXorRight into two  xor-left-four-bits and xor-right-four-bits
            List<int> xorLeftFourBitsTwo = new List<int>();
            List<int> xorRightFourBitsTwo = new List<int>();
            for (int i = 0; i < outputXorRight.Count; i++){
                var tempVal = outputXorRight[i];
                if (i < 4)
                    xorLeftFourBitsTwo.Add(tempVal);
                else
                    xorRightFourBitsTwo.Add(tempVal);
            }

            // evaluate xor-left-four-bits using S-Zero and S-One tables
            List<int> outputFromSZeroTabTwo = EvaluateThroughS0Table(xorLeftFourBitsTwo);
            List<int> outputFromSOneTabTwo = EvaluateThroughS1Table(xorRightFourBitsTwo);

            // combine outputFromSZeroTab and  outputFromSOneTab
            List<int> inputForP2TwoRight = CombineTwoLists(outputFromSZeroTabTwo, outputFromSOneTabTwo);

            // permutate the two results from S tables 
            List<int> outputPtwoRight = PermutateFourBitsFromSTables(inputForP2TwoRight);

            //  final RIGHT XOR 
            List<int> outputSecondProcess = FinalExorEvaluation(pinitRightFourBits, outputPtwoRight);

            // combine outputSecondProcess and outputFirstProcess
            List<int> combinedOutputForFinalPermutation = CombineTwoLists(outputSecondProcess, outputFirstProcess);

            // get cyphered text and build to string
            List<int> cipheredList = FinalPermutation(combinedOutputForFinalPermutation);

            return ConvertListOfIntToString(cipheredList);
        }

        private List<int> ConvertStringToIntList(string inputText)
        {
            List<int> output = new List<int>();
            for (int i = 0; i < inputText.Length; i++){
                output.Add((int)Char.GetNumericValue(inputText[i]));
            }
            return output;
        }

        private string ConvertListOfIntToString(List<int> inputList)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var val in inputList){
                builder.Append(val);
            }
            return builder.ToString();
        }

        private List<int> InitialPermutation(List<int> inputList)
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

        private List<int> PermutateRightFourBits(List<int> inputList)
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

        private List<int> FirstExorEvaluation(List<int> listFromStables, List<int> inputKey1)
        {
            List<int> output = new List<int>();
            for (int i = 0; i < inputKey1.Count; i++){
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

        private List<int> EvaluateThroughS0Table(List<int> inputList)
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

        private List<int> EvaluateThroughS1Table(List<int> inputList)
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

        private List<int> CombineTwoLists(List<int> inputList1, List<int> inputList2)
        {
            List<int> output = inputList1;
            output.AddRange(inputList2);
            return output;
        }

        private List<int> PermutateFourBitsFromSTables(List<int> inputList)
        {
            List<int> output = new List<int>();
            output.Add(inputList[3]);
            output.Add(inputList[0]);
            output.Add(inputList[2]);
            output.Add(inputList[1]);
            return output;
        }

        private List<int> FinalExorEvaluation(List<int> leftInitList, List<int> outputPtwotList)
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

        private List<int> FinalPermutation(List<int> inputList)
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
