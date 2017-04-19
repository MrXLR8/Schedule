using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
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
        public Lector lector { get; set; }
        public int lectionNumber { get; set; } // номер пары в дне
        public Lection(string _name, Lector _lector, int _auditory,  int _lectionNumber )
        {
            name = _name; auditory = _auditory; lectionNumber = _lectionNumber; lector.name = _lector.name; lector.middleName = _lector.middleName; lector.lastName = _lector.lastName;
        }
    }

    public class TimeInterval
    {
        class Time
        {
            int hours { get; set; }
            int minutes { get; set; }


        }
        public int pairNumber { get; set; }

    }
    public class Lector : Entry
    {
        public string lastName { get; set; }
        public string middleName { get; set; }

        public override string ToString()
        {
            string result;
            result = lastName;
            try
            {
                result += " " + name[0] + ". " + middleName[0] + ".";
            }
            catch (Exception e)
            {
                result = lastName;
            }
            return result;
        }

        public Lector(string _lastName, string _name, string _middleName)
        {
            name = _name; middleName = _middleName; lastName = _lastName;
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
