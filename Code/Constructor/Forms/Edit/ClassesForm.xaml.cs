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
    /// Логика взаимодействия для ListsEditor.xaml
    /// </summary>
    public  partial  class ClassesForm : Window
    {
        
        public ClassesForm()
        {
            
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void prepodName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            classGrid.ItemsSource = Global.classList;
            
        }



        private void addEntry_Click(object sender, RoutedEventArgs e)
        {
            Global.classList.Add(Convert.ToInt32(classNumber.Text));
            clearFields();
        }

        void clearFields()
        {
            classNumber.Clear();
        }

        private void deleteEntry_Click(object sender, RoutedEventArgs e)
        {
            Global.classList.Remove((int)classGrid.SelectedItem);
        }

    }
}
