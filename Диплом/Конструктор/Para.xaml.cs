using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Builder
{
    /// <summary>
    /// Логика взаимодействия для Para.xaml
    /// </summary>
    public partial class Para : UserControl
    {
       
        public Para()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty pairNumberProperty =
            DependencyProperty.Register("pairNumber", typeof(int), typeof(Para), new FrameworkPropertyMetadata(new PropertyChangedCallback(pairNumberChanged)));

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("startH", typeof(Time), typeof(Para), new FrameworkPropertyMetadata(new PropertyChangedCallback(TimeChanged)));


        public Time time
        {
            get { return (Time)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }


        public int pairNumber
        {
            get { return (int)GetValue(pairNumberProperty); }
            set { SetValue(pairNumberProperty, value); }
        }

        private static void pairNumberChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            Para window = (Para)sender;
            window.pairLabel.Content = window.pairNumber;
        }


        private static void TimeChanged(DependencyObject sender,
     DependencyPropertyChangedEventArgs e)
        {
            Para window = (Para)sender;

           window.startHText.Text = window.time.start.Hours.ToString();
            window.startMText.Text = window.time.start.Minutes.ToString();

            window.endHText.Text = window.time.end.Hours.ToString();
            window.endMText.Text = window.time.end.Hours.ToString();
        }


        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(startHText.Text) && !string.IsNullOrEmpty(startMText.Text) && !string.IsNullOrEmpty(endHText.Text) && !string.IsNullOrEmpty(endMText.Text))
            {
                
                TimeSpan start, end;
                start = new TimeSpan(Convert.ToInt32(startHText.Text), Convert.ToInt32(startMText.Text), 0);
                end = new TimeSpan(Convert.ToInt32(endHText.Text), Convert.ToInt32(endMText.Text), 0);

                Time toChange = new Time(pairNumber, start, end);

                time = toChange;
            }
        }
    }
}
