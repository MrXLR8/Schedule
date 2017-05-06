using System;
using System.Collections.Generic;
using System.Text;

namespace Share
{
    public class Command
    {
        public Command() { }

        public string type;
        public List<string> arguments=new List<string>();


        public string toAnswer;

        public void qualify()
        {
            switch (type)
            {
                case "check":
                    checkCommand();
                    return;
            }
        }

        public void checkCommand()
        {
            toAnswer = arguments[0];
        }

    }
}

