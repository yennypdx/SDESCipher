using System.Collections.Generic;

namespace SDES
{
   public class EncryptionSDES
    {
        LibrarySDES lib = new LibrarySDES();
        public EncryptionSDES() { }
        public string EncryptionMode(string key1, string key2, string plainTextFromUser)
        {
            return Encryption(key1, key2, plainTextFromUser);
        }

        private string Encryption(string key1, string key2, string plainText)
        {
            List<int> keyOneList = lib.ConvertStringToIntList(key1);
            List<int> keyTwoList = lib.ConvertStringToIntList(key2);
            List<int> initText = lib.ConvertStringToIntList(plainText);

            // first permutation 
            List<int> outputPinit = lib.InitialPermutation(initText);

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
            List<int> outputPone = lib.PermutateRightFourBits(pinitRightFourBits);

            /* <<<<<<<< xor with [key1] >>>>>>>> */
            List<int> outputXor = lib.FirstExorEvaluation(outputPone, keyOneList);

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
            List<int> outputFromSZeroTab = lib.EvaluateThroughS0Table(xorLeftFourBits);
            List<int> outputFromSOneTab = lib.EvaluateThroughS1Table(xorRightFourBits);

            // combine outputFromSZeroTab and outputFromSOneTab
            List<int> inputForP2 = lib.CombineTwoLists(outputFromSZeroTab, outputFromSOneTab);

            // permutate the two results from S tables 
            List<int> outputPtwo = lib.PermutateFourBitsFromSTables(inputForP2);

            // finalize first LEFT PROCESS by xor
            List<int> outputFirstProcess = lib.FinalExorEvaluation(pinitLeftFourBits, outputPtwo);

            // start SECOND ROUND process
            List<int> expandedOutputFirstProcess = lib.PermutateRightFourBits(outputFirstProcess);

            /* <<<<<<<< xor with [key2] >>>>>>>> */
            List<int> outputXorRight = lib.FirstExorEvaluation(expandedOutputFirstProcess, keyTwoList);

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
            List<int> outputFromSZeroTabTwo = lib.EvaluateThroughS0Table(xorLeftFourBitsTwo);
            List<int> outputFromSOneTabTwo = lib.EvaluateThroughS1Table(xorRightFourBitsTwo);

            // combine outputFromSZeroTab and  outputFromSOneTab
            List<int> inputForP2TwoRight = lib.CombineTwoLists(outputFromSZeroTabTwo, outputFromSOneTabTwo);

            // permutate the two results from S tables 
            List<int> outputPtwoRight = lib.PermutateFourBitsFromSTables(inputForP2TwoRight);

            //  final RIGHT XOR 
            List<int> outputSecondProcess = lib.FinalExorEvaluation(pinitRightFourBits, outputPtwoRight);

            // combine outputSecondProcess and outputFirstProcess
            List<int> combinedOutputForFinalPermutation = lib.CombineTwoLists(outputSecondProcess, outputFirstProcess);

            // get cyphered text and build to string
            List<int> cipheredList = lib.FinalPermutation(combinedOutputForFinalPermutation);

            return lib.ConvertListOfIntToString(cipheredList);
        }        
   }
}
