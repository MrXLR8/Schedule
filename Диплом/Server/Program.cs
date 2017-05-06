using System;
using System.Reflection;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string Version = "0.1";

            Console.WriteLine("Добро пожаловать в приложение сервер для расписаний");
            Console.WriteLine("Автор приложения Федоров Андрей, студент 4-го курса КДАВТ");
            Console.WriteLine("Версия приложения "+ Version);
            Console.WriteLine("===============================================");
            while (true)
            {
               

               

                Function.execute(new CommandLine().get());

                
            }
        }
    }
}