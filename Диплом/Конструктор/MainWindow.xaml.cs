﻿
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
        public Lection selected = null;
        public ObservableCollection<LectionSwap> swapListToAdd = new ObservableCollection<LectionSwap>();
        public ObservableCollection<Group> testList { get; set; }
        

        public MainWindow()
        {
            Global.prepodWindow = new LectorsForm();
            Global.predmetWindow = new PredmetsForm();
            Global.classesWindow = new ClassesForm();
            Global.intervalWindow = new IntervalsForm();
            Global.main = this;
            #region Инициализая глобальных списков

            // Global.editorWindow.prepodGrid.ItemsSource = Global.lectorList;

            testList = new ObservableCollection<Group>();

            Global.lectorList = new ObservableCollection<Lector>();
            Global.predmetList = new ObservableCollection<string>();
            Global.classList = new ObservableCollection<int>();
            Global.groupList = new ObservableCollection<Group>();
            #endregion

            #region ТЕСТОВЫЕ ЗАПОЛНЕНИЯ В СПИСКАХ

            #region lectors
            Global.lectorList.Add(new Lector("Дубинский", "Вячеслав", "Генадийович"));

            Global.lectorList.Add(new Lector("Федоров", "Андрей", "Вячеславович"));

            Global.lectorList.Add(new Lector("Бондарь", "Иван", "ФигЕгоЗнаевич"));
            #endregion
            #region paru
            Global.predmetList.Add("Эмпирические Методы");
            Global.predmetList.Add("Информатика");
            Global.predmetList.Add("English");
            #endregion 

            #region class
            Global.classList.Add(105);
            Global.classList.Add(308);
            Global.classList.Add(224);
            #endregion


            #endregion

            InitializeComponent();
            DataContext = this;
 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lectorCombo.ItemsSource = Global.lectorList;
            paraCombo.ItemsSource = Global.predmetList;
            classCombo.ItemsSource = Global.classList;
            timeCombo.ItemsSource = Global.intervals.timeList;
            swapGrid.ItemsSource = swapListToAdd;
            predmetSwapCombo.ItemsSource = Global.predmetList;
            GroupListBox.ItemsSource = Global.groupList;
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            LectionControl toAdd = new LectionControl(new Lection("TEST", 1, new Lector("Fedorov", "Andrey", "Viacheslavovuch"), 305));
            chMonday.Items.Add(toAdd);

            LectionControl toAdd2 = new LectionControl(new Lection("SWAP", 2, new Lector("Fedorov", "Andrey", "Viacheslavovuch"), 305));
            toAdd2.Lection.swapList.Add(new LectionSwap(DateTime.Now, "Informatuka"));
            chMonday.Items.Add(toAdd2);
            toAdd2.reDraw();
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {


          
           

        }





        private void prepodEdit_Click(object sender, RoutedEventArgs e)
        {
            Global.prepodWindow.Show();
            Global.prepodWindow.Activate();
        }

        private void predmetEdit_Click(object sender, RoutedEventArgs e)
        {
            Global.predmetWindow.Show();
            Global.predmetWindow.Activate();
        }

        private void classEdit_Click(object sender, RoutedEventArgs e)
        {
            Global.classesWindow.Show();
            Global.classesWindow.Activate();
        }

        private void paraEdit_Click(object sender, RoutedEventArgs e)
        {
            Global.intervalWindow.Show();
            Global.intervalWindow.Activate();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void timeCombo_MouseEnter(object sender, MouseEventArgs e)
        {
            timeCombo.ItemsSource = Global.intervals.timeList;
            ((ComboBox)sender).Items.Refresh();
        }

        private void DeleteSwap_Click(object sender, RoutedEventArgs e)
        {
            LectionSwap toDel = (LectionSwap)swapGrid.SelectedItem;
            swapListToAdd.Remove(toDel);
        }

        private void createSwapButton_Click(object sender, RoutedEventArgs e)
        {
            if (swapDatePicker.SelectedDate != null)
            { if (predmetSwapCombo.SelectedItem != null)
                {
                    LectionSwap toAdd = new LectionSwap((DateTime)swapDatePicker.SelectedDate, predmetSwapCombo.SelectedItem.ToString());
                    swapListToAdd.Add(toAdd);
                }
            }
        }

        private void addGroup_Click(object sender, RoutedEventArgs e)
        {
            string text = groupNameText.Text;
            Group toAdd;
            if(!string.IsNullOrEmpty(text))
            {
                toAdd = new Group(text);
                Global.groupList.Add(toAdd);
                groupNameText.Clear();
            }
        }

        private void deleteGroup_Click(object sender, RoutedEventArgs e)
        {



            Global.groupList.Remove((Group)GroupListBox.SelectedItem);


        }

    }

}
