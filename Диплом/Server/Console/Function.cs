using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public static class Function
    {
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
                    startFunc(input.getParametr("port"));
                    break;
                case "stop":
                    stopFunc();
                    break;

            }
        }

        private static void startFunc(Argument port)
        {
            if(Server.SocketServer.status=="Отключен")
            SocketServer.Initialize(Convert.ToInt32(port.parametr));
            else { Log.write("SOCK", "Сервер уже запущен. Для остановки используйте команду stop",ConsoleColor.Red); }
        }

        private static void stopFunc()
        {
            SocketServer.DeActivate();
           
        }
       
    }
}
