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
using System.Windows.Shapes;

namespace Builder
{
    /// <summary>
    /// Логика взаимодействия для ListsEditor.xaml
    /// </summary>
    public  partial  class Lectors : Window
    {
        
        public Lectors()
        {
            
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
            
        }



        private void prepodName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            prepodGrid.ItemsSource = Global.lectorList;
            Global.lectorList.Add(new Lector("Dubinskuy", "Slava", "Gendiyovuch"));

            Global.lectorList.Add(new Lector("Fedorov", "Andre", "Vyach"));

            Global.lectorList.Add(new Lector("Bondar", "Ivan", "Fugegoznaevich"));

        }



        private void addEntry_Click(object sender, RoutedEventArgs e)
        {
            Global.lectorList.Add(new Lector(prepodName.Text, prepodMiddleName.Text, prepodMiddleName.Text));
            clearFields();
        }
        void clearFields()
        {
            prepodName.Clear();
            prepodLastName.Clear();
            prepodMiddleName.Clear();
        }

        private void deleteEntry_Click(object sender, RoutedEventArgs e)
        {
            Global.lectorList.Remove((Lector)prepodGrid.SelectedItem);
        }
    }
}
