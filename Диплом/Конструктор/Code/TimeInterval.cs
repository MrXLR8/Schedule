using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{

    public class IntervalCollection 
    {

        // 1 - 9:00 - 10:20
        // 2 - 10:20 - 11:50
        // 3 - INSERT (12:10 - 13:30)
        //4 -  13:40 - 15:00
        public int last { get { return timeList.Count; } }

        public ObservableCollection<Interval> timeList = new ObservableCollection<Interval>();



        public bool checkCorrect(Interval _input)
        {
            int index = _input.index-1;
            if (_input.start > _input.end)
            {
                return false;
            }
            if (last > 0)
            {
                for (int i = 0; i < index; i++) // проверка были ли раньше пары у которых время раньше
                {
                    Interval c = timeList[i];
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

                        if(_input.start<=c.end)
                        {
                            //если старт находиться внутри промежутка
                            return false;
                        }
                    }
                }
            }
            for (int i = index + 1; i < last-1; i++)
            {
                Interval c = timeList[i];
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

                    if (_input.end >= c.start)
                    { // его конец не может залазить в его старт
                        return false;
                    }
                }

            }
            return true;

        }

        public bool setTime(Interval _input)
        {
            if (checkCorrect(_input))
            {
                try { timeList[_input.index - 1] = _input; }
                catch (Exception e) { timeList.Add(_input); }
                
                return true;
            }
            else return false;

        }

        public IntervalCollection Clone()
        {
            IntervalCollection result = new IntervalCollection();
            result.timeList=Collection.ToCollection<Interval>(Collection.ToList<Interval>(timeList));
            return result;
            
        }

    }

    public class Interval 
    {
        public int index;
        public TimeSpan start;
        public TimeSpan end;
        public Interval(int _index, TimeSpan _start, TimeSpan _end)
        {
            index = _index;
            start = _start;
            end = _end;
        }

        public Interval(int _index)
        {
            index = _index;
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
    
