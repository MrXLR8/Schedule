using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public static class Function
    {
        public static void execute(CommandLine input)
        {
            string command = input.command;

            switch (command)
            {
                case "start":
                    startFunc(input.getParametr("port"));
                    break;

            }
        }

        private static void startFunc(Argument port)
        {
            Server.Initialize(Convert.ToInt32(port.parametr));
        }
       
    }
}
