using System.Collections.Generic;
using System.Linq;

namespace BSK2
{
    public class LFSR
    {
        LinkedList<int> _actualLFSRState;
        LinkedList<int> _polynomial;


        public LFSR(string seed, string polynomial)
        {
            _actualLFSRState = new LinkedList<int>(seed.ToList().Select(x => x == '1' ? 1 : 0));
            _polynomial = new LinkedList<int>(polynomial.ToList().Select(x => x == '1' ? 1 : 0).Reverse());
            _polynomial.RemoveFirst();

        }


        public string wyswietlLFSR()
        {
            string text = "";
            foreach (var item in _actualLFSRState)            
                text += item;

            text+="\n";
            return text;
        }

        public int GetNext()
        {
            int output = Xor();
            _actualLFSRState.RemoveLast();
            _actualLFSRState.AddFirst(output);
            return output;
        }

        public int GetNext(int wejscie)
        {
            int output = Xor();

            output = (output + wejscie) % 2;

            _actualLFSRState.RemoveLast();
            _actualLFSRState.AddFirst(output);
            return output;
        }
        public int GetNext2(int wejscie)
        {
            int output = Xor();

            output = (output + wejscie) % 2;

            _actualLFSRState.RemoveLast();
            _actualLFSRState.AddFirst(wejscie);
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

}
