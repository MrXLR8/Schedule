using System;
using System.Collections.Generic;
using System.Text;

namespace Libary
{

    public abstract class Entry
    {
        public string name { get; set; }
    }

    public class Group: Entry
    {
        public Group(string _name)
        {
            name= _name;
        }
    }

    public class Lection: Entry
    {
        public int auditory { get; set; }
        public string lector { get; set; }
        public int lectionNumber { get; set; } // номер пары в дне
        public Lection(string _name, string _lector, int _auditory,  int _lectionNumber )
        {
            name = _name; auditory = _auditory; lectionNumber = _lectionNumber; lector = _lector;
        }
    }


    public class LectionSwap
    {
        public  Lection para { get; set; } 
        public  DateTime period { get; set; }
        public LectionSwap(DateTime _period,Lection _para)
        {
            period = _period;
            para = _para;
        }
        public string periodString
        {
            get
            {
                string result;
                result = string.Format("{0}. {1}", period.Day.ToString(), period.Month.ToString());
                return result;
            }

        }
    }
}
