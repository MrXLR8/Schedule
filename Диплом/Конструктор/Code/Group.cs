using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        Week chuslutel = new Week("ch");
        Week znamenatel = new Week("zm");

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


    class Week
    {
        public Day Monday { get; set; }
        public Day  Tuesday { get; set; }
        public Day Wednesday { get; set; }
        public Day Thursday { get; set; }
        public Day  Friday { get; set; }
        public Day Saturday{ get; set; }

        Day pick( string toFind)
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
            if(type=="ch")
            {
                Monday = new Day("Monday", Global.main.chMonday);
                Tuesday = new Day("Tuesday", Global.main.chTuesday);
                Wednesday = new Day("Wednesday", Global.main.chWednesday);
                Thursday = new Day("Thursday", Global.main.chThursday);
                Friday = new Day("Friday", Global.main.chFriday);
                Saturday = new Day("Saturday", Global.main.chSaturday);
            }

            if (type == "zm")
            {
                Monday = new Day("Monday", Global.main.zmMonday);
                Tuesday = new Day("Tuesday", Global.main.zmTuesday);
                Wednesday = new Day("Wednesday", Global.main.zmWednesday);
                Thursday = new Day("Thursday", Global.main.zmThursday);
                Friday = new Day("Friday", Global.main.zmFriday);
                Saturday = new Day("Saturday", Global.main.zmSaturday);
            }
        }
    }

    class Day: Entry
    {
        public ObservableCollection<Lection> lectionList { get; set; }
        public ListBox list;
        public Day(string _name, ListBox _list)
        {
            name = _name;
            list = _list;
            list.Items.Clear();
            lectionList = new ObservableCollection<Lection>();
        }

        public void add(Lection _input)
        {
            
            Lection duplicate;
            duplicate = gotDuplicate(_input.lectionInterval);
            if (duplicate != null) // если в дне уже есть пара в этом интервале
            {
                duplicate = _input.Clone();
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
            list.ItemsSource = lectionList;
            list.Items.Refresh();
        }

    }

}
