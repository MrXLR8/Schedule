using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Interval lectionInterval { get; set; } // номер пары в дне
        public Lection(string _name, Interval _lectionNumber, Lector _lector, int _auditory )
        {
            name = _name;
            auditory = _auditory;
            lectionInterval = _lectionNumber;
            lector =_lector;
            swapList = new ObservableCollection<LectionSwap>();
        }
        public ObservableCollection<LectionSwap> swapList { get; set; }

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
        public string para { get; set; } 
        public  DateTime period { get; set; }
        public LectionSwap(DateTime _period,string _para)
        {
            period = _period;
            para = _para;
        }
        public string periodString
        {
            get
            {
                string result;

                string correctDate(int input)
                {
                    if(input<10)
                    {
                        return "0" + input.ToString();
                    }
                    return input.ToString();
                }

                result = string.Format("{0}. {1}", correctDate(period.Day), correctDate(period.Month));
                return result;
            }

        }
    }
}
