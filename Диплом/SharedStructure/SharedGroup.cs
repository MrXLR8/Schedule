using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace Builder
{

    public  abstract class Entry
    {
        public string name { get; set; }
    }


    public partial class Week : Entry
    {


        public Week() { }


        public Day Monday { get; set; }
        public Day Tuesday { get; set; }
        public Day Wednesday { get; set; }
        public Day Thursday { get; set; }
        public Day Friday { get; set; }
        public Day Saturday { get; set; }
    }
    public partial class Group : Entry
    {
        public Group() { }


        public Week chuslutel;
        public Week znamenatel;

        public string commentary;



    }
    public partial class Lector : Entry
    {
        public string lastName { get; set; }
        public string middleName { get; set; }

        public Lector() { }

           }
    public partial class LectionSwap
    {
        public string para { get; set; } 
        public  DateTime period { get; set; }

        public LectionSwap() { }
      


    }
    public partial class Day : Entry
    {

        public string week;
        public ObservableCollection<Lection> lectionList { get; set; }
        public string lookname;

        public Day() { }



    }
    public partial class Lection : Entry
    {


        public int auditory { get; set; }
        public Lector lector { get; set; }
        public int lectionInterval { get; set; } // номер пары в дне
        public ObservableCollection<LectionSwap> swapList { get; set; }

        
        public string week;

        
        public string day;

        public Lection() { }

     

    }

}
