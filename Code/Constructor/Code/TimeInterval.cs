﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{

    public partial class IntervalCollection
    {
        public bool checkCorrect(Interval _input)
        {
            TimeSpan empty = new TimeSpan();
            int index = _input.index - 1;
            if (_input.start == new TimeSpan() | _input.end == new TimeSpan()) return false;
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
                            if (c.end != empty & c.start != empty)
                                return false;
                        }

                        if (_input.end <= c.end)
                        { // если у ранних конечная дата больше
                            if (c.end != empty & c.start != empty)
                                return false;
                        }

                        if (_input.start <= c.end)
                        {
                            if (c.end != empty & c.start != empty)
                                //если старт находиться внутри промежутка
                                return false;
                        }
                    }
                }
            }
            for (int i = index + 1; i < last - 1; i++)
            {
                Interval c = timeList[i];
                if (c != null)
                {
                    if (_input.start > c.start)
                    { // если у поздних начальная дата меньше
                        if (c.start != empty)
                            return false;
                    }

                    if (_input.end >= c.end)
                    { // если у поздних конечная дата меньше
                        if (c.end != empty)
                            return false;
                    }

                    if (_input.end >= c.start)
                    { // его конец не может залазить в его старт
                        if (c.end != empty & c.start != empty)
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
                catch (Exception) { timeList.Add(_input); }

                return true;
            }
            else return false;

        }

        public IntervalCollection Clone()
        {
            IntervalCollection result = new IntervalCollection()
            {
                timeList = Collection.ToCollection<Interval>(Collection.ToList<Interval>(timeList))
            };
            return result;

        }

    }

    public partial class Interval
    {
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
            if (start.Hours < 10) { startH = "0" + start.Hours; } else { startH = start.Hours.ToString(); };
            if (start.Minutes < 10) { startM = "0" + start.Minutes; } else { startM = start.Minutes.ToString(); };

            if (end.Hours < 10) { endH = "0" + end.Hours; } else { endH = end.Hours.ToString(); };
            if (end.Minutes < 10) { endM = "0" + end.Minutes; } else { endM = end.Minutes.ToString(); };

            return index + " (" + startH + ":" + startM + " - " + endH + ":" + endM + ")";
        }

    }
}

