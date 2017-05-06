using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class CommandLine
    {
        public class Argument
        {
            public string argument;
            public string parametr;

            public Argument(string aug, string param) {
            argument=aug;
           parametr=param;

            }
        }

        public string command;
        public Argument[] arguments;

        public CommandLine(string input)
        {
         var raw=input.Split(' ');
            arguments = new Argument[10];
            command = raw[0];
            for(int i=1;i<raw.Length;i++)
            {
                string[] split = raw[i].Split(':');

                arguments[i - 1] = new Argument(split[0], split[1]);
            }
        }

        public string getParametr(string lookingfor)
        {
            foreach(Argument a in arguments)
            {
                if(a.argument==lookingfor)
                {
                    return a.parametr;
                }
            }
            return "";
        }
    }
}
