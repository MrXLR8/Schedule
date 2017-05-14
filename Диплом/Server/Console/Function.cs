﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Server
{
    public static class Function
    {
        public static Argument getParametr(CommandLine input,string lookingfor)
        {
            foreach (Argument a in input.arguments)
            {

                if (a?.argument == lookingfor)
                {
                    return a;
                }
            }
            return null;
        }

        public static void execute(CommandLine input)
        {
            string command;
            try
            {
                command = input.command;
            }
            catch(NullReferenceException exc)
            {
                return;
            }

            switch (command)
            {
                case "start":
                    startFunc(input);
                    break;
                case "stop":
                    stopFunc();
                    break;
                case "help":
                    helpFunc();
                    break;
                case "blacklist":
                    blacklist(input);
                    break;
                case "whitelist":
                    whitelist(input);
                    break;

                

            }
        }

        private static void startFunc(CommandLine input)
        {
            int port=0;
            try
            {
                port = Convert.ToInt32(getParametr(input, "port").parametr);
            }
            catch(FormatException exc) { Log.write("INPT", "Введен неверный порт для комманды start", ConsoleColor.Red); return; }
            if(Server.SocketServer.status=="Отключен")
            SocketServer.Initialize(port);
            else { Log.write("SOCK", "Сервер уже запущен. Для остановки используйте команду stop",ConsoleColor.Red); }
        }

        private static void stopFunc()
        {
            if (Server.SocketServer.status == "Включен")
                SocketServer.DeActivate();
            else { Log.write("SOCK", "Сервер не запущен. Для запуска используйте команду start port:номерпорта", ConsoleColor.Red); }
           
        }
#region help

        public static List<helpinfo> allhelp = new List<helpinfo>();

        public struct helpinfo
        {
            public string command;
            public string desc;
            public List<argumentinfo> arguments;
        }

        public struct argumentinfo
        {
            public string argument;
            public string parametr;
        }


        private static void helpFunc()
        {
            Console.WriteLine("=======================");
            Console.WriteLine("Комманды вводяться в следующем формате: {комманда} {аргумент}:{параметр аргумента}. Пример: start port:11000");
            Console.WriteLine("=======================");

            void fillHelp()
            {
                helpinfo toAdd;
                argumentinfo toAdd2;

                toAdd.command = "start";
                toAdd.desc = "Запускает сервер на выбранном порте";
                toAdd.arguments = new List<argumentinfo>();
               
                toAdd2.argument = "port";
                toAdd2.parametr = "Обязательный параметр. Указывает порт на котором будет запущен сервер";
               
                toAdd.arguments.Add(toAdd2);

                allhelp.Add(toAdd);

                toAdd.command = "stop";
                toAdd.desc = "Останавливает сервер";
                toAdd.arguments = new List<argumentinfo>();

                allhelp.Add(toAdd);

            }

            if (allhelp.Count == 0) fillHelp();

            foreach (helpinfo h in allhelp)
            {
                
                Console.WriteLine("Команда: " + h.command );
                Console.WriteLine(h.desc);
                
                if (h.arguments.Count > 0)

                {
                    Console.WriteLine("Аргументы:");
                    foreach (argumentinfo a in h.arguments)
                    {
                        Console.WriteLine(a.argument + " - " + a.parametr);
                    }
                }
                Console.WriteLine("=======================");
            }
        }

        #endregion
        private static void blacklist(CommandLine input)
        {

            IPAddress ip;
            try
            {
                ip = IPAddress.Parse(input.arguments[0].parametr);
            }
            catch (FormatException exc) { Log.write("INPT", "Введен неверный IP для комманды blacklist", ConsoleColor.Red); return; }
            if (input.arguments[0].argument == "add")
            {
                Builder.Global.listAdd(Builder.Global.blacklist, ip);
                Builder.Global.SaveList(Builder.Global.blacklist, "blacklist.json");
                Log.write("FILE", ip, "Добавлен в черный список");
            }
            if (input.arguments[0].argument == "remove")
            {
                if (Builder.Global.listRemove(Builder.Global.blacklist, ip))
                {
                    Builder.Global.SaveList(Builder.Global.blacklist, "blacklist.json");
                    Log.write("FILE", ip, "Убран из черного списка");
                }
                else { Log.write("FILE", ip, "Не найден в черном списке"); }
            }
        }

        private static void whitelist(CommandLine input)
        {
           
            IPAddress ip;
            try
            {
                ip = IPAddress.Parse(input.arguments[0].parametr);
            }
            catch (FormatException exc) { Log.write("INPT", "Введен неверный IP для комманды whitelist", ConsoleColor.Red); return; }
            if (input.arguments[0].argument == "add")
            {
                Builder.Global.listAdd(Builder.Global.whitelist, ip);
                Builder.Global.SaveList(Builder.Global.whitelist, "whitelist.json");
                Log.write("FILE", ip, "Добавлен в белый список");
            }
            if(input.arguments[0].argument == "remove")
            {

                if (Builder.Global.listRemove(Builder.Global.whitelist, ip))
                {
                    Builder.Global.SaveList(Builder.Global.whitelist, "whitelist.json");
                    Log.write("FILE", ip, "Убран из белого списка");
                }
                else { Log.write("FILE", ip, "Не найден в белом списке списке"); }
            }
        }

    }
}
