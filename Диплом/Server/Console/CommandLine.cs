using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Server
{
    public class CommandLine
    {
        const string LOGTYPE="INPT";

        public string command;
        public Argument[] arguments;

        public CommandLine get()
        {

            string input = Console.ReadLine();
            string[] raw= input.ToLower().Split(' ');
            File.AppendAllText(Log.filename, "["+DateTime.Now+"] >>>>" + input + Environment.NewLine);
            arguments = new Argument[10];
            command = raw[0];

            if (raw.Length != 1)
            {
                for (int i = 1; i < raw.Length; i++)
                {
                    string[] split = raw[i].Split(':');
                    try
                    {
                        arguments[i - 1] = new Argument(split[0], split[1]);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Log.write(LOGTYPE, "Для " + command + " произведен некоректный ввод параметра", ConsoleColor.Yellow);
                        return null;
                    }
                }
            }

            string checkSTR = check();
            if(checkSTR == "parametr")
            {
                Log.write(LOGTYPE, "Команда " + command + " не выполнена. Ошибка ввода аргументов", ConsoleColor.Yellow);
                return null;
                
            }
            else if(checkSTR=="nocommand")
            {
                Log.write(LOGTYPE, "Комманда " + command + " не существует", ConsoleColor.Yellow);
                return null;
            }
            return this;
        }

        string check()
        {
            switch (command)
            {
                case "start":
                    if (getParametr("port") != null) return "good"; else { return "parametr"; };
                case "stop": return "good";
                case "help": return "good";
                case "blacklist":
                    if (getParametr("add") != null||getParametr("remove")!=null) return "good"; else { return "parametr"; };
                case "whitelist":
                    if (getParametr("add") != null || getParametr("remove") != null) return "good"; else { return "parametr"; };
            }
            return "nocommand";
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
