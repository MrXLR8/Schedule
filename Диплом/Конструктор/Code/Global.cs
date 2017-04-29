
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Builder
{
    public static class Global
    {


        public static UIElement getFromGrid(Grid _toLook, int column, int row)
        {
            return _toLook.Children
          .Cast<UIElement>()
          .First(f => Grid.GetRow(f) == 0 && Grid.GetColumn(f) == column);
        }

        public static string SpanToString(TimeSpan input)
        {

            string startH, startM;
            if (input.Hours < 10) { startH = "0" + input.Hours; } else { startH = input.Hours.ToString(); };
            if (input.Minutes < 10) { startM = "0" + input.Minutes; } else { startM = input.Minutes.ToString(); };

            return startH + ":" + startM;
        
    }

        public static MainWindow main;
        public static LectorsForm prepodWindow;
        public static PredmetsForm predmetWindow;
        public static ClassesForm classesWindow;
        public static IntervalsForm intervalWindow;

        public static ObservableCollection<Lector> lectorList { get; set; }
        public static ObservableCollection<string> predmetList { get; set; }
        public static ObservableCollection<Group> groupList { get; set; }
        public static ObservableCollection<int> classList { get; set; }
        public static IntervalCollection intervals = new IntervalCollection();



        public static Group selectedGroup; // текущая выбранная группа
        public static Lection selectedLection; // текущая выбранная лекция из всех списков



    }





    public static class Collection
    {
        public static List<T> ToList<T>(ObservableCollection<T> _input)
        {
            List<T> output = new List<T>();

            foreach (T c in _input)
            {
                output.Add(c);
            }
            return output;
        }

        public static ObservableCollection<T> ToCollection<T>(List<T> _input)
        {
            ObservableCollection<T> output = new ObservableCollection<T>();

            foreach (T c in _input)
            {
                output.Add(c);
            }
            return output;
        }


    }

}
