using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;

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

        public Week chuslutel = new Week("ch");
        public Week znamenatel = new Week("zm");

    }

    public class Lection: Entry
    {
        public int auditory { get; set; }
        public Lector lector { get; set; }
        public int lectionInterval { get; set; } // номер пары в дне

        public Lection(string _name, int _lectionNumber, Lector _lector, int _auditory )
        {
            name = _name;
            auditory = _auditory;
            lectionInterval = _lectionNumber;
            lector =_lector;
            swapList = new ObservableCollection<LectionSwap>();
        }

        public Lection Clone()
        {
            Lection toReturn;
            toReturn = new Lection(name, lectionInterval, lector, auditory);
            toReturn.swapList = swapList;
            return toReturn;
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


    public class Week
    {
        public Day Monday { get; set; }
        public Day  Tuesday { get; set; }
        public Day Wednesday { get; set; }
        public Day Thursday { get; set; }
        public Day  Friday { get; set; }
        public Day Saturday{ get; set; }

        public Day pick( string toFind)
        {
            switch (toFind)
            {
                case "Понедельник":
                    return Monday;
                case "Вторник":
                    return Tuesday;
                case "Среда":
                    return Wednesday;
                case "Четверг":
                    return Thursday;
                case "Пятница":
                    return Friday;
                case "Суббота":
                    return Saturday;
            }
            return null;
        }

        public Week(string type)
        {
                Monday = new Day("Monday", type);
                Tuesday = new Day("Tuesday", type);
                Wednesday = new Day("Wednesday", type);
                Thursday = new Day("Thursday", type);
                Friday = new Day("Friday", type);
                Saturday = new Day("Saturday", type);
            
        }
    }

    public class Day: Entry
    {
        public ObservableCollection<Lection> lectionList { get; set; }
        string lookname;
        public Day(string _name,string _weekType)
        {
            lectionList = new ObservableCollection<Lection>();
            ListBox list;
            lookname = _weekType + _name;

            list = (ListBox) Global.main.FindName(lookname);
            list.ItemsSource = lectionList;
            
        }

        public void add(Lection _input)
        {
            
            Lection duplicate;
            duplicate = gotDuplicate(_input.lectionInterval);
            if (duplicate != null) // если в дне уже есть пара в этом интервале
            {
                duplicate.auditory = _input.auditory;
                duplicate.lectionInterval = _input.lectionInterval;
                duplicate.lector = _input.lector;
                duplicate.name = _input.name;
                duplicate.swapList = _input.swapList;
            }
            else { lectionList.Add(_input); }

            lectionList.OrderBy(s => s.lectionInterval);
            reDraw();

        }

        Lection gotDuplicate (int look)
        {
            foreach (Lection c in lectionList)
            {
                if (c.lectionInterval == look) return c;
            }
            return null;
        }

        void reDraw()
        {
            ObservableCollection<LectionControl> toShow = new ObservableCollection<LectionControl>();
            foreach(Lection c in lectionList)
            {
                toShow.Add(new LectionControl(c));
            }
            var list = (ListBox)Global.main.FindName(lookname);
            list.ItemsSource = toShow;
            list.Items.Refresh();
        }

    }

}
