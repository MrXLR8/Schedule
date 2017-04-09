using System;
using System.Collections.Generic;
using System.Text;

namespace Конструктор
{
   public class Group
    {
        public string nameOfTheGroup { get; set; }
        public Group(string _name)
        {
            nameOfTheGroup = _name;
        }

    }
}
