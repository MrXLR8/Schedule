﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
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

        public void massReDraw()
        {
            chuslutel.redraw();
            znamenatel.redraw();
        }

    }

    public class Lection: Entry
    {
        public struct WeekInfo
        {
            string day;
            string week;
        }

        public int auditory { get; set; }
        public Lector lector { get; set; }
        public int lectionInterval { get; set; } // номер пары в дне
        public ObservableCollection<LectionSwap> swapList { get; set; }

        public Week week;
        public Day day;

 

        public Lection(string _name, int _lectionNumber, Lector _lector, int _auditory)
        {
  

            name = _name;
            auditory = _auditory;
            lectionInterval = _lectionNumber;
            lector =_lector;
            swapList = new ObservableCollection<LectionSwap>();
        }

        public void setParents(Day _parent)
        {
            day = _parent;
            week = day.week;
        }

        public Lection Clone()
        {
            Lection toReturn;
            toReturn = new Lection(name, lectionInterval, lector, auditory);
            toReturn.swapList = swapList;
            return toReturn;
        }

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
                    if (input < 10)
                    {
                        return "0" + input.ToString();
                    }
                    return input.ToString();
                }

                result = string.Format("{0}. {1}", correctDate(period.Day), correctDate(period.Month));
                return result;
            }
        }

                    public override string ToString()
        {
          return periodString+" ["+para+"]";
        }



    }


    public class Week: Entry
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
                name = type;
                Monday = new Day("Monday",this);
                Tuesday = new Day("Tuesday", this);
                Wednesday = new Day("Wednesday", this);
                Thursday = new Day("Thursday", this);
                Friday = new Day("Friday", this);
                Saturday = new Day("Saturday", this);
            
        }

        public void redraw()
        {
            Monday.reDraw();
            Tuesday.reDraw();
            Thursday.reDraw();
            Friday.reDraw();
            Saturday.reDraw();
        }
    }

    public class Day: Entry
    {
        public Week week;


        public ObservableCollection<Lection> lectionList { get; set; }
        public string lookname;
       
        public Day(string _name, Week parent)
        {
            week = parent;
            name = _name;

            lectionList = new ObservableCollection<Lection>();
            ListBox list;
            lookname = week.name +_name;

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
                duplicate.setParents(this);
            }
            else { _input.setParents(this);  lectionList.Add(_input);  }


             var look = lectionList.OrderBy(s => s.lectionInterval);
            lectionList = Collection.ToCollectionFromNum<Lection>(look);
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


       public void reDraw()
        {
            try
            {
                ObservableCollection<LectionControl> toShow = new ObservableCollection<LectionControl>();
      
                foreach (Lection c in lectionList)
                {
                    try
                    {
                        toShow.Add(new LectionControl(c));
                    }
                    catch (Exception e)
                    {
                        lectionList.Remove(c);
 
                    }
                }

            var list = (ListBox)Global.main.FindName(lookname);
            list.ItemsSource = toShow;
            list.Items.Refresh();
            }
            catch (InvalidOperationException exc)
            {
                reDraw();
            }
        }

    }

}
