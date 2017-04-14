
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


namespace Builder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Group> testList { get; set; }
        public ObservableCollection<LectionSwap> swapList { get; set; }

        public MainWindow()
        {
            #region Инициализая глобальных списков
            Global.editorWindow = new ListsEditor();
           // Global.editorWindow.prepodGrid.ItemsSource = Global.lectorList;

            testList = new ObservableCollection<Group>();

            Global.lectorList = new ObservableCollection<Lector>();

            #endregion


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
            swapList = new ObservableCollection<LectionSwap>();
            LectionSwap first = new LectionSwap(DateTime.Now,new Lection("Empir", new Lector("Dubinskuy", "Slava", "Gendiyovuch"), 305,2));
            LectionSwap second = new LectionSwap(DateTime.Now, new Lection("Tryd",new Lector("Fedorov","Andrey","Viacheslavovuch"), 108, 4));

            swapList.Add(first);
            swapList.Add(second);

            swapGrid.ItemsSource = null;
            swapGrid.ItemsSource = swapList;

          
           

        }

        private void deleteGroup_Click(object sender, RoutedEventArgs e)
        {
            /* Button sent = (Button)sender;
         Grid myGrid = (Grid)sent.Parent;
         int rowIndex = Grid.GetColumn(sent);
         ColumnDefinition rowDef = myGrid.ColumnDefinitions[rowIndex];

         Label asd = (Label)GlobalMethods.getFromGrid(myGrid, 1, 0);

         MessageBox.Show(asd.Content.ToString());
         */
           // swapList = new ObservableCollection<LectionSwap>();
            LectionSwap first = new LectionSwap(DateTime.Now, new Lection("zzzz", new Lector("Dubinskuy", "Slava", "Gendiyovuch"), 305, 2));
            LectionSwap second = new LectionSwap(DateTime.Now, new Lection("xxxxx", new Lector("Fedorov", "Andrey", "Gendiyovuch"), 108, 4));

            swapList.Add(first);
            swapList.Add(second);
            
            //swapGrid.ItemsSource = null;
           // swapGrid.ItemsSource = swapList;

        }

        private void ListsButton_Click(object sender, RoutedEventArgs e)
        {
            
            Global.editorWindow.Show();
        }
    }

}
