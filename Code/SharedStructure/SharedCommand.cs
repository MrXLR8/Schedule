using System;
using System.Collections.Generic;
using System.Text;

namespace Share
{
    public partial class Command
    {
        public Command() { }

        public string type;
        public List<string> arguments = new List<string>();


        public string toAnswer;


    }
}

