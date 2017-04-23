using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Builder
{
    /// <summary>
    /// Логика взаимодействия для TimeInterval.xaml
    /// </summary>
    public partial class IntervalsForm : Window
    {


        IntervalCollection test = new IntervalCollection();

        public IntervalsForm()
        {
            InitializeComponent();
            
        }

        public bool checkAll ()
        {
            ItemCollection look = pairs.Items;

            foreach (Interval c in look)
            {
  
                if(!test.setTime(c)) { return false; }
            }
            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;

        }

 




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            test = new IntervalCollection();
            test = Global.intervals.Clone();
            pairs.ItemsSource = test.timeList;
            pairs.Items.Refresh();
        }



        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            int index = Global.intervals.last - 1;
            if (index >= 0)
            {
                Global.intervals.timeList.RemoveAt(index);
                index = Global.intervals.last - 1;
                if (index < 0)
                {
                    ((Button)sender).IsEnabled = false;
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        { 
            MessageBox.Show(checkAll().ToString());
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {

            //Global.intervals.timeList .Add( new Interval(Global.intervals.last+1, new TimeSpan(9, 1, 0), new TimeSpan(10, 20, 0)));
            test.timeList.Add(new Interval(test.last + 1));
            removeButton.IsEnabled = true;
        }

        private void pairs_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {

            test = Global.intervals.Clone();
            pairs.ItemsSource = test.timeList;
            pairs.Items.Refresh();
            if(pairs.Items.Count==0) { removeButton.IsEnabled = false; }
        }
    }
}
