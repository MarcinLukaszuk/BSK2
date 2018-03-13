using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK2
{
    class Program
    {
        static void Main(string[] args)
        {
            SynchronousStreamCipher SSC = new SynchronousStreamCipher("0010", "10011");
            SSC.Encrypt("test.bin");
            
        }
    }
}
