
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

        public static void resetForm()
        {
            foreach (ListBox c in main.days)
            {
                c.ItemsSource = null;
            }

            setEdits(null);

         
        }

        public static void setEdits(Lection input)
        {
            if (input != null)
            {
                Lector pickLector(Lector search)
                {
                    foreach(Lector l in main.lectorCombo.ItemsSource)
                    {
                        if (l.lastName == search.lastName)

                            if (l.middleName == search.middleName)
                                if (l.name == l.name)
                                    return l;
                        
                    }
                    return null;
                }

                main.lectorCombo.SelectedItem = pickLector(input.lector);
                main.paraCombo.SelectedItem = input.name;
                main.classCombo.SelectedItem = input.auditory;
                main.timeCombo.SelectedItem = Global.intervals.timeList[input.lectionInterval-1];

                main.swapListToAdd.Clear();
             //   main.swapGrid.ItemsSource = null;
                foreach (LectionSwap c in input.swapList)
                {
                    main.swapListToAdd.Add(c);
                }
                main.swapGrid.ItemsSource = main.swapListToAdd;


                

                

                switch (input.week)
                {
                    case "ch":
                        main.ChRadio.IsChecked = true;
                        break;
                    case "zm":
                        main.ZmRadio.IsChecked = true;
                        break;

                }


                switch (input.day)
                {
                    case "Monday":
                        main.dayInWeekCombo.SelectedItem = "Понедельник";
                        break;
                    case "Tuesday":
                        main.dayInWeekCombo.SelectedItem = "Вторник";
                        break;
                    case "Wednesday":
                        main.dayInWeekCombo.SelectedItem = "Среда";
                        break;
                    case "Thursday":
                        main.dayInWeekCombo.SelectedItem = "Четверг";
                        break;
                    case "Friday":
                        main.dayInWeekCombo.SelectedItem = "Пятница";
                        break;
                    case "Saturday":
                        main.dayInWeekCombo.SelectedItem = "Суббота";
                        break;
                }


                main.predmetSwapCombo.SelectedItem = null;
            }
            else
            {
                main.DeleteSelected.IsEnabled = false;
                main.DeleteToolTip.Visibility = Visibility.Collapsed;



                main.paraCombo.SelectedItem = null;
                main.lectorCombo.SelectedItem = null;
                main.classCombo.SelectedItem = null;
                main.timeCombo.SelectedItem = null;
                main.swapGrid.SelectedItem = null;
                main.predmetSwapCombo.SelectedItem = null;
                main.dayInWeekCombo.SelectedItem = null;
                
            }

        }

        public static void setSaveButton()
        {
            if (!string.IsNullOrWhiteSpace(FileInteraction.fileName)) main.saveButton.IsEnabled = true;
            else { main.saveButton.IsEnabled = false; }
        }

        public static void fixItemSource()
        {
            main.GroupListBox.ItemsSource = groupList;
            main.GroupListBox.SelectedItem = selectedGroup;

            main.lectorCombo.ItemsSource = lectorList;
            main.paraCombo.ItemsSource = predmetList;
            main.classCombo.ItemsSource = classList;
            main.timeCombo.ItemsSource = intervals.timeList;
            //main.swapGrid.ItemsSource = swapListToAdd;
            main.predmetSwapCombo.ItemsSource = predmetList;

        }

        public static MainWindow main;
        public static LectorsForm prepodWindow;
        public static PredmetsForm predmetWindow;
        public static ClassesForm classesWindow;
        public static IntervalsForm intervalWindow;
        public static CommentaryForm commentaryWindow;
        public static ServerSync syncForm;
        public static ServerSyncGroup syncGroupForm;

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

            foreach (T c in     _input)
            {
                output.Add(c);
            }
            return output;
        }

        public static ObservableCollection<T> ToCollectionFromNum<T>(IEnumerable<T> _input)
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
