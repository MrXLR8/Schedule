using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public static partial class Data
    {



        public static void reset()
        {
            void clearWeek(Week input)
            {
                void clearDay(Day _input)
                {
                    _input.lectionList.Clear();
                }

                clearDay(input.Monday);
                clearDay(input.Tuesday);
                clearDay(input.Wednesday);
                clearDay(input.Thursday);
                clearDay(input.Friday);
                clearDay(input.Saturday);
            }

            Global.setEdits(null);

            Global.classList = new ObservableCollection<int>();

            Global.lectorList = new ObservableCollection<Lector>();
            Global.predmetList = new ObservableCollection<string>();
            Global.intervals.timeList = new ObservableCollection<Interval>();

            Global.classesWindow.Close();
            Global.predmetWindow.Close();
            Global.prepodWindow.Close();

            Global.commentaryWindow?.Close();
            // Global.commentaryWindow = null;

            Global.intervalWindow.Close();
            Global.intervalWindow = new IntervalsForm();

            foreach (Group c in Global.groupList)
            {
                clearWeek(c.chuslutel);
                clearWeek(c.znamenatel);
            }
            Global.groupList = new ObservableCollection<Group>();

            Global.selectedGroup = null;
            Global.selectedLection = null;

            Global.fixItemSource();





        }


    }



    public partial class Schedule
    {




        public void fillMe()
        {

            intervals = Global.intervals;
            lectorList = Global.lectorList;
            predmetList = Global.predmetList;
            classList = Global.classList;
            groupList = Global.groupList;

            pcName = Environment.MachineName;

        }



        public void applyMe()
        {


            Data.reset();


            Global.intervals = intervals;
            Global.lectorList = lectorList;
            Global.predmetList = predmetList;
            Global.classList = classList;
            Global.groupList = groupList;

            Global.syncGroupForm.groupCombo.SelectedItem = Global.selectedGroup?.name;

            try
            {
                Global.selectedGroup = Global.groupList[0];
                Global.selectedGroup.massReDraw();
            }
            catch (Exception) { }
            Global.fixItemSource();
        }



    }



}
