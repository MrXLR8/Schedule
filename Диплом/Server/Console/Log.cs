using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Server
{
    public static class Log
    {
        public static string filename;
        public static void write(string category, string info)
        {
            write(category, info, ConsoleColor.Gray);
        }

        public static void write(string category, string info,ConsoleColor color)
        {
            if (string.IsNullOrEmpty(filename)) formFile();
            string fileoutput;
            fileoutput = string.Format("[{0}] ({1}): {2}", DateTime.Now, category, info);

            string DateOutput = DateTime.Now.ToString();


            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("[" + DateOutput + "] ");


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("(" + category+") ");

            Console.ForegroundColor = color;
            Console.WriteLine(info);

            File.AppendAllText(filename, fileoutput + Environment.NewLine);

            Console.ResetColor();
        }



        public static void write(string category, IPAddress ip, string info, ConsoleColor color)
        {
            if (string.IsNullOrEmpty(filename)) formFile();
            string fileoutput;
            fileoutput = string.Format("[{0}] ({1}): {2}", DateTime.Now, category,ip + " "+ info);

            string DateOutput= DateTime.Now.ToString();
            string IPOutput = ip.ToString();

            Console.ForegroundColor=ConsoleColor.DarkYellow;
            Console.Write("["+DateOutput+"] ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("(" + category + ") ");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("{"+IPOutput+"}: ");

            Console.ForegroundColor = color;
            Console.WriteLine(info);

            File.AppendAllText(filename, fileoutput + Environment.NewLine);

            Console.ResetColor();
        }
        public static void write(string category, IPAddress ip, string info)
        {
            write(category, info, ConsoleColor.Gray);
        }



            public static void formFile()
        {
            try
            {
                Directory.CreateDirectory("logs");
                filename = "logs\\SCHD_Server_" + DateTime.Now + ".log";
                filename = filename.Replace(' ', '_');
                filename = filename.Replace(':', '.');
            }
            catch (Exception)
            {
                filename = "SCHD_SERVER_(" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour+")";
                Log.write("FILE", "Не удалось сформировать файл. Записываю лог в файл в корне программы: "+filename, ConsoleColor.Red);
            }
           
        }
    }
}
