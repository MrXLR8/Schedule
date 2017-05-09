using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Server
{
    public class CommandLine
    {
        const string LOGTYPE="CMD";

        public string command;
        public Argument[] arguments;

        public CommandLine get()
        {

            string input = Console.ReadLine();
            var raw= input.ToLower().Split(' ');
            File.AppendAllText(Log.filename, "["+DateTime.Now+"] >>>>" + input + Environment.NewLine);
            arguments = new Argument[10];
            command = raw[0];
            for(int i=1;i<raw.Length;i++)
            {
                string[] split = raw[i].Split(':');
                try
                {
                    arguments[i - 1] = new Argument(split[0], split[1]);
                }
                catch(IndexOutOfRangeException exc)
                {
                    Log.write(LOGTYPE, "Комманда " + command + " не существует или произведен некоректный ввод параметра",ConsoleColor.Yellow);
                    return null;
                }
            }
            if(!check())
            {
                Log.write(LOGTYPE, "Команда " + command + " не выполнена. Не хватает параметра(ов)", ConsoleColor.Yellow);

                return null;
            }
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
