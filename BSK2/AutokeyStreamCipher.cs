using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BSK2
{
    public class AutokeyStreamCipher
    {
        protected List<byte> _inputBytesArray;
        protected List<byte> _outputBytesArray;

        protected LFSR LFSR;
        protected string _seed;
        protected string _polynomial;
        public AutokeyStreamCipher(string seed, string polynomial)
        {
            _seed = seed;
            _polynomial = polynomial;
        }
        public void Encrypt(string filePath, string outputfile)
        {
            LFSR = new LFSR(_seed, _polynomial);
            _inputBytesArray = File.ReadAllBytes(filePath).ToList();
            _outputBytesArray = new List<byte>();

            #region LocalVariables
            int[] inputBitsArray;
            int[] outputBitsArray = new int[8];

            int LFSROutput;
            int decoded;
            #endregion  
            foreach (byte _byte in _inputBytesArray)
            {
                inputBitsArray = ConvertByteToBitsArray(_byte);
                for (int i = 0; i < inputBitsArray.Length; i++)
                {
                    decoded = inputBitsArray[i];
                    LFSROutput = LFSR.GetNext(decoded);
                    outputBitsArray[i] = LFSROutput;
                }
                _outputBytesArray.Add(ConvertBitsArrayToByte(outputBitsArray)); 
            }
            File.WriteAllBytes(outputfile, _outputBytesArray.ToArray());
        }
        public void Decrypt(string filePath, string outputfile)
        {
            LFSR = new LFSR(_seed, _polynomial);
            _inputBytesArray = File.ReadAllBytes(filePath).ToList();
            _outputBytesArray = new List<byte>();

            #region LocalVariables
            int[] inputBitsArray;
            int[] outputBitsArray = new int[8];

            int LFSROutput;
            int decoded;
            #endregion 
            foreach (byte _byte in _inputBytesArray)
            {
                inputBitsArray = ConvertByteToBitsArray(_byte);
                for (int i = 0; i < inputBitsArray.Length; i++)
                {
                    decoded = inputBitsArray[i];
                    LFSROutput = LFSR.GetNext2(decoded);
                    outputBitsArray[i] = LFSROutput;
                }
                _outputBytesArray.Add(ConvertBitsArrayToByte(outputBitsArray));
            }
            File.WriteAllBytes(outputfile, _outputBytesArray.ToArray());
        }
        public string EncryptString(string text)
        {
            StringBuilder sb = new StringBuilder();
            LFSR = new LFSR(_seed, _polynomial);

            int actualNumber;
            int actualXor;
            foreach (var item in text)
            {
                actualNumber = item == '1' ? 1 : 0;
                actualXor = LFSR.GetNext();
                sb.Append((actualNumber + actualXor) % 2 == 1 ? "1" : "0");
            }
            return (sb.ToString());
        }


        protected int[] ConvertByteToBitsArray(byte _byte)
        {
            string stringBits = Convert.ToString(_byte, 2).PadLeft(8, '0');
            int[] intArrayOutput = stringBits.Select(x => int.Parse(x.ToString())).ToArray();
            return intArrayOutput;
        }
        protected byte ConvertBitsArrayToByte(int[] _bitsArray)
        {
            byte val = 0;
            foreach (bool b in _bitsArray.Select(x => x == 1 ? true : false))
            {
                val <<= 1;
                if (b) val |= 1;
            }
            return val;
        }
    }


}
