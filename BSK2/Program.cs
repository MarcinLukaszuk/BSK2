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
          //  SSC.Encrypt("test2.bin");
            SSC.Encrypt("encoded.bin");

            //CompareArrays("test2.bin", "decoded.bin");




        }

        static void CompareArrays(string filepath1, string filepath2)
        {  
            var array1 = File.ReadAllBytes(filepath1).ToList();
            var array2 = File.ReadAllBytes(filepath2).ToList();


            for (int i = 0; i < array1.Count; i++)
            {
                if (array1[i]!=array2[i])
                {
                    Console.WriteLine("njie to samo");
                }
            }
            Console.WriteLine("to samo");

            Console.Read();

        }


    }
}
