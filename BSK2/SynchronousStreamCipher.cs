using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK2
{
    public class LFSR2
    {
        LinkedList<int> _actualLFSRState;
        LinkedList<int> _polynomial;


        public LFSR2(string seed, string polynomial)
        {
            _actualLFSRState = new LinkedList<int>(seed.ToList().Select(x => x == '1' ? 1 : 0));
            _polynomial = new LinkedList<int>(polynomial.ToList().Select(x => x == '1' ? 1 : 0).Reverse());
            _polynomial.RemoveFirst();

        }


        public int GetNext()
        {
            int output = Xor();
            _actualLFSRState.RemoveLast();
            _actualLFSRState.AddFirst(output);
            return output;
        }

        private int Xor()
        {
            int counter = 0;
            for (int i = 0; i < _polynomial.Count; i++)
                counter += _polynomial.ElementAt(i) == 1 ? _actualLFSRState.ElementAt(i) : 0;
            return counter % 2;
        }
    }


    public class SynchronousStreamCipher
    {
        List<byte> _inputBytesArray;
        List<byte> _outputBytesArray;

        private LFSR2 LFSR;
        private string _seed;
        private string _polynomial;
        public SynchronousStreamCipher(string seed, string polynomial)
        {
            _seed = seed;
            _polynomial = polynomial;
        }
        public void Encrypt(string filePath)
        {
            LFSR = new LFSR2(_seed, _polynomial);
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
                    LFSROutput = LFSR.GetNext();
                    decoded = inputBitsArray[i];
                    outputBitsArray[i] = LFSROutput == decoded ? 0 : 1;
                }
                _outputBytesArray.Add(ConvertBitsArrayToByte(outputBitsArray));
            }

            File.WriteAllBytes("decoded" + filePath, _outputBytesArray.ToArray());
        }

        public string EncryptString(string text)
        {
            StringBuilder sb = new StringBuilder();
            LFSR = new LFSR2(_seed, _polynomial);

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


        private int[] ConvertByteToBitsArray(byte _byte)
        {
            string stringBits = Convert.ToString(_byte, 2).PadLeft(8, '0');
            int[] intArrayOutput = stringBits.Select(x => int.Parse(x.ToString())).ToArray();
            return intArrayOutput;
        }
        private byte ConvertBitsArrayToByte(int[] _bitsArray)
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