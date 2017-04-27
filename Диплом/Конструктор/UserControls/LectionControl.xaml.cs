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
    /// Логика взаимодействия для LectionControl.xaml
    /// </summary>
    public partial class LectionControl : UserControl
    {
        public LectionControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty lectionProperty =
          DependencyProperty.Register("Lection", typeof(Lection), typeof(LectionControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(LectionChanged)));

        public Lection Lection
        {
            get { return (Lection)GetValue(lectionProperty); }
            set { SetValue(lectionProperty, value); }
        }

        private static void LectionChanged(DependencyObject sender,
   DependencyPropertyChangedEventArgs e)
        {
            LectionControl window = (LectionControl)sender;
            window.pairIndexLabel.Content = window.Lection.lectionInterval.index;

            window.startTimeLabel.Content = Global.SpanToString(window.Lection.lectionInterval.start);

            window.endTimeLabel.Content = Global.SpanToString(window.Lection.lectionInterval.end);

            window.predmetLabel.Text = window.Lection.name;

            window.prepodLabel.Text = window.Lection.lector.ToString();

            window.classLabel.Text = window.Lection.auditory.ToString();

            if(window.Lection.swapList.Count>0)
            {
                window.swapCountLabel.Content = window.Lection.swapList.Count.ToString();
                window.swapButton.Visibility = Visibility.Visible;
                window.swapButton.IsEnabled = true;
            }
            else
            {
                window.swapButton.Visibility = Visibility.Collapsed;
                window.swapButton.IsEnabled = false;
            }

        }


    }
}
