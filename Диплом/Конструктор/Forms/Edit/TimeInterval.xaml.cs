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
    public partial class TimeInterval : Window
    {
        public ObservableCollection<Time> listOfTimes = new ObservableCollection<Time>() { null, null, null, null, null, null, null, null, null, null, null } ;
        Button[] listOfButtons;

        Para test = new Para();

        public TimeInterval()
        {
            InitializeComponent();
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;

        }

 
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox target = (TextBox)sender;
            Grid group  =(Grid) target.Parent;
            GroupBox groupBox = (GroupBox) group.Parent;
            Grid globalGrid = (Grid)groupBox.Parent;
            Label text = (Label) Global.getFromGrid(globalGrid, 0, 0);

            Grid withButtons = (Grid)Global.getFromGrid(globalGrid, 3, 0);
            Button saveButton = (Button)Global.getFromGrid(withButtons, 0, 0);

            int pairNumber = Convert.ToInt32(text.Content.ToString());

            foreach (Button c in listOfButtons)
            {
                if(c!=null)
                buttonCheck(c);
            }

            GroupBox Pair;
            Grid inPair;
            TextBox localTargetH,localTargetM;
            string hours, minutes;
            TimeSpan from=new TimeSpan(), to= new TimeSpan();
            bool first=false, second=false; 
            //-------------- первая группа
            Pair = (GroupBox)Global.getFromGrid(globalGrid, 1, 0);
            inPair = (Grid)Pair.Content;
            localTargetH = ((TextBox)Global.getFromGrid(inPair, 0, 0));
            localTargetM = ((TextBox)Global.getFromGrid(inPair, 2, 0));

            hours = localTargetH.Text;
            minutes = localTargetM.Text;

            
            
            if (!string.IsNullOrEmpty(hours) & !string.IsNullOrEmpty(minutes))
            {
                try
                {
                    from = TimeSpan.Parse(hours + ":" + minutes);
                }
                catch (Exception ex)
            {
                localTargetH.Text = "";
                localTargetM.Text = "";
                return;
            }
            first = true;
            }
            else { saveButton.IsEnabled = false; }
            //-------------- вторая группа
            Pair = (GroupBox)Global.getFromGrid(globalGrid, 2, 0);
            inPair = (Grid)Pair.Content;
            localTargetH = ((TextBox)Global.getFromGrid(inPair, 0, 0));
            localTargetM = ((TextBox)Global.getFromGrid(inPair, 2, 0));

            hours = localTargetH.Text;
            minutes = localTargetM.Text;

            if (!string.IsNullOrEmpty(hours) & !string.IsNullOrEmpty(minutes))
            {
                try
                {
                    to = TimeSpan.Parse(hours + ":" + minutes);
                }
                catch (Exception ex)
                {
                    localTargetH.Text = "";
                    localTargetM.Text = "";
                    return;
                }
                second = true;
            }
            else { saveButton.IsEnabled = false; }

            if(first&second)
            {
                Time toAdd = new Time(pairNumber, from, to);
                if(Global.interval.checkCorrect(toAdd))
                {
                    saveButton.IsEnabled = true;
                    listOfTimes[pairNumber] = toAdd;
                }
                else { saveButton.IsEnabled = false; }
            }
            
        }

        void buttonCheck(Button target)
        {
            Grid group = (Grid)target.Parent;
            Grid globalGrid = (Grid)group.Parent;
            Label text = (Label)Global.getFromGrid(globalGrid, 0, 0);
            int pairNumber = Convert.ToInt32(text.Content.ToString());
            Time tocheck = listOfTimes[pairNumber];
            Intervals test=new Intervals();
            test.timeList = listOfTimes;

            if (tocheck != null)
            {
                if (!test.checkCorrect(tocheck))
                {
                    target.IsEnabled = false;
                }
            }
            else { target.IsEnabled = false; }
        }

        private void saveInterval_Click(object sender, RoutedEventArgs e)
        {

            foreach (Button c in listOfButtons)
            {
                if (c != null)
                    buttonCheck(c);
            }

            Button target = (Button)sender;
            Grid group = (Grid)target.Parent;
            Grid globalGrid = (Grid)group.Parent;
            Label text = (Label)Global.getFromGrid(globalGrid, 0, 0);
            int pairNumber = Convert.ToInt32(text.Content.ToString());
            if (!Global.interval.setTime(listOfTimes[pairNumber]))
            {
                target.IsEnabled = false;
            }
        }

        private void deletePair_Click(object sender, RoutedEventArgs e)
        {
            Button target = (Button)sender;
            Grid group = (Grid)target.Parent;
            Grid globalGrid = (Grid)group.Parent;
            Label text = (Label)Global.getFromGrid(globalGrid, 0, 0);
            int pairNumber = Convert.ToInt32(text.Content.ToString());
            Global.interval.timeList[pairNumber] = null;
            listOfTimes[pairNumber] = null;
            foreach (Button c in listOfButtons)
            {
                if (c != null)
                    buttonCheck(c);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            MessageBox.Show("load");
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(test.time.ToString());
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            pairs.Items.Add(test);
            test.pairNumber = 2;
            test.time = new Time(2, new TimeSpan(9, 1, 0), new TimeSpan(10, 20, 0));
            pairs.Items.Refresh();
            
        }
    }
}
