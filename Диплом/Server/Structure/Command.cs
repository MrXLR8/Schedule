using System;
using System.Collections.Generic;
using System.Text;

namespace Share
{
    public partial class Command
    {
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

