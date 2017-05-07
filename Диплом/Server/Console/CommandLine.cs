using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class CommandLine
    {


        public string command;
        public Argument[] arguments;

        public CommandLine get()
        {
           
         var raw= Console.ReadLine().ToLower().Split(' ');
            arguments = new Argument[10];
            command = raw[0];
            for(int i=1;i<raw.Length;i++)
            {
                string[] split = raw[i].Split(':');
                try
                {
                    arguments[i - 1] = new Argument(split[0], split[1]);
                }
                catch(IndexOutOfRangeException exc) { Console.WriteLine("[CONSOLE]Комманда " + command + " не существует или произведен некоректный ввод параметра"); return null; }
            }
            if(!check()) { Console.WriteLine("[CONSOLE]Команда " + command + " не выполнена. Не хватает параметра(ов)"); return null; }
            return this;
        }

        bool check()
        {
            switch (command)
            {
                case "start":
                    if (getParametr("port")!=null) return true;
                    break;
            }
            return false;
        }

        public Argument getParametr(string lookingfor)
        {
            foreach(Argument a in arguments)
            {
                
                if(a?.argument==lookingfor)
                {
                    return a;
                }
            }
            return null;
        }


    }

    public class Argument
    {
        public string argument;
        public string parametr;

        public Argument(string aug, string param)
        {
            argument = aug;
            parametr = param;

        }
    }
}
