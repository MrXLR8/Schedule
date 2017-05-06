using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public static class Data
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
            Global.classList.Clear();
            Global.groupList.Clear();
            Global.lectorList.Clear();
            Global.predmetList.Clear();
            Global.intervals.timeList.Clear();

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

            Global.selectedGroup = null;
            Global.selectedLection = null;
            // добавить сюда очищение ссылки на файл
        }


    }

    public class Schedule
    {
        public string encodedData;
        public string hash;

        public DateTime modified;
        public string pcName;

    }
}
