using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{

    public class Intervals
    {

        // 1 - 9:00 - 10:20
        // 2 - 10:20 - 11:50
        // 3 - INSERT (12:10 - 13:30)
        //4 -  13:40 - 15:00
        public ObservableCollection<Time> timeList = new ObservableCollection<Time>() { null, null, null, null, null, null, null, null, null, null, null };
        public bool checkCorrect(Time _input)
        {
            int index = _input.index;
            if (_input.start > _input.end)
            {
                return false;
            }

            for (int i = 1; i < index; i++) // проверка были ли раньше пары у которых время раньше
            {
                Time c = timeList[i];
                if (c != null)
                {
                    if (_input.start <= c.start)
                    { // если у ранних начальная дата больше
                        return false;
                    }

                    if (_input.end <= c.end)
                    { // если у ранних конечная дата больше
                        return false;
                    }
                }
            }

            for (int i = index + 1; i < 10; i++)
            {
                Time c = timeList[i];
                if (c != null)
                {
                    if (_input.start >= c.start)
                    { // если у поздних начальная дата меньше
                        return false;
                    }

                    if (_input.end >= c.end)
                    { // если у поздних конечная дата меньше
                        return false;
                    }
                }

            }
            return true;

        }


        public bool setTime(Time _input)
        {
            if (checkCorrect(_input))
            {
                timeList[_input.index] = _input;
                return true;
            }
            else return false;

        }
    }

    public class Time
    {
        public int index;
        public TimeSpan start;
        public TimeSpan end;
        public Time(int _index, TimeSpan _start, TimeSpan _end)
        {
            index = _index;
            start = _start;
            end = _end;
        }
        public override string ToString()
        {
            string startH, startM;
            string endH, endM;
             if (start.Hours < 10) { startH= "0" + start.Hours; } else { startH= start.Hours.ToString(); };
            if (start.Minutes < 10) { startM = "0" + start.Minutes; } else { startM = start.Minutes.ToString(); };

            if (end.Hours < 10) { endH = "0" + end.Hours; } else { endH = end.Hours.ToString(); };
            if (end.Minutes < 10) { endM = "0" + end.Minutes; } else { endM = end.Minutes.ToString(); };

            return index+" (" + startH+ ":" + startM + " - " + endH + ":" + endM + ")";
        }
    }
}
    
