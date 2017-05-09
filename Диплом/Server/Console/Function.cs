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
            Server.Initialize(Convert.ToInt32(port.parametr));
        }

        private static void stopFunc()
        {
            Server.DeActivate();
           
        }
       
    }
}
