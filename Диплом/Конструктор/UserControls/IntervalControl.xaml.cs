﻿using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Builder
{
    /// <summary>
    /// Логика взаимодействия для Para.xaml
    /// </summary>
    public partial class IntervalControl : UserControl
    {
       
        public IntervalControl()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("time", typeof(Interval), typeof(IntervalControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(TimeChanged)));


        public Interval time
        {
            get { return (Interval)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }





        private static void TimeChanged(DependencyObject sender,
     DependencyPropertyChangedEventArgs e)
        {
            IntervalControl window = (IntervalControl)sender;

            window.pairLabel.Content = window.time.index;

            window.startHText.Text = window.time.start.Hours.ToString();
            window.startMText.Text = window.time.start.Minutes.ToString();

            window.endHText.Text = window.time.end.Hours.ToString();
            window.endMText.Text = window.time.end.Minutes.ToString();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(startHText.Text) && !string.IsNullOrEmpty(startMText.Text) && !string.IsNullOrEmpty(endHText.Text) && !string.IsNullOrEmpty(endMText.Text))
            {
                
                TimeSpan start, end;
                start = new TimeSpan(Convert.ToInt32(startHText.Text), Convert.ToInt32(startMText.Text), 0);
                end = new TimeSpan(Convert.ToInt32(endHText.Text), Convert.ToInt32(endMText.Text), 0);

                Interval toChange = new Interval(time.index, start, end);

                time = toChange;
            }
        }
    }
}
