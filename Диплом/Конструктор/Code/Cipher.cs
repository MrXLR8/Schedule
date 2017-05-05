using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    static class Cipher
    {
        public static string encryption(string informmation)
        {
            if(informmation.Length%2!=0)
            {
                informmation += " ";
            }
            int number = 0;
            string cod = "";
            for(int i=0; i< informmation.Length;i+=2)
            {
                cod += (char)(informmation[i]+1+ number);
                number++;
                if(number>3)
                {
                    number = 0;
                }
            }
            number = 0;
            for (int i = 1; i < informmation.Length; i += 2)
            {
                cod += (char)(informmation[i]+2+ number);
                number++;
                if (number > 3)
                {
                    number = 0;
                }
            }
            return cod;
        }
        public static string transcript(string informmation)
        {
            string cod = "";
            int number = 0;
            for (int i = 0; i < informmation.Length/2; i ++)
            {
                cod += (char)(informmation[i]-1- number);
                cod += (char)(informmation[i+ informmation.Length / 2]-2- number);
                number++;
                if (number > 3)
                {
                    number = 0;
                }
            }
            if (cod[cod.Length - 1] == ' ')
            {
                string cod2 = "";
                for (int i = 0; i < cod.Length-1 ; i++)
                {
                    cod2+=cod[i];
                }
                cod = cod2;
            }
            return cod;
        }
    }
}
