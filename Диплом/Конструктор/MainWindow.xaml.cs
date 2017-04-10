using Libary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
namespace Конструктор
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Group> testList { get; set; }

        public MainWindow()
        {
            testList =  new ObservableCollection<Group>();
            
            InitializeComponent();
            DataContext = this;
 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
           
            testList.Add(new Group("ПІ-1320"));
            testList.Add(new Group("ПР-19"));
            testList.Add(new Group("8-Б"));


            //GroupList.ItemsSource = testList;
           // GroupList.ItemsSource = new[] { new { nameOfTheGroup = "TEST1" }, new { nameOfTheGroup = "TEST2" } };
           
            System.Diagnostics.Debug.Write("\n loaded");
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            testList.Add(new Group("NEW ONE"));
            
        }
    }

}
