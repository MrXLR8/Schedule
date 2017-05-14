using System;
using System.Reflection;
using System.Text;
using Builder;
namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string Version = "0.2";

            Log.formFile();
            Log.write("LOG", "Лог запщуен");
            Console.Clear();

            Console.WriteLine("Добро пожаловать в приложение сервер для расписаний");
            Console.WriteLine("Автор приложения Федоров Андрей, студент 4-го курса КДАВТ");
            Console.WriteLine("Для справки по коммандам напишите help");
            Console.WriteLine("Версия приложения "+ Version);
            Console.WriteLine("===============================================");

            Global.LoadSchedule("last.schd");

            Global.blacklist=Global.LoadList("blacklist.json");
            if (Global.blacklist.Count > 0) Log.write("FILE", "Загружен черный список IP-адрессов с " + Global.blacklist.Count + " записями", ConsoleColor.Green);

            Global.whitelist = Global.LoadList("whitelist.json");
            if (Global.whitelist.Count > 0) Log.write("FILE", "Загружен белый список IP-адрессов с " + Global.whitelist.Count + " записями", ConsoleColor.Green);

            while (true)
            {
               

               

                Function.execute(new CommandLine().get());

                
            }
        }
    }
}