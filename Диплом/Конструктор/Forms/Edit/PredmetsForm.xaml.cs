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
    public  partial  class PredmetsForm : Window
    {
        
        public PredmetsForm()
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
            predmetGrid.ItemsSource = Global.predmetList;
            
        }



        private void addEntry_Click(object sender, RoutedEventArgs e)
        {
            Global.predmetList.Add(predmetName.Text);
            clearFields();
        }

        void clearFields()
        {
            predmetName.Clear();
        }

        private void deleteEntry_Click(object sender, RoutedEventArgs e)
        {
            Global.predmetList.Remove((string)predmetGrid.SelectedItem);
        }

    }
}
