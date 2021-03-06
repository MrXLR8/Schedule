﻿using System;
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
    public  partial  class LectorsForm : Window
    {
        
        public LectorsForm()
        {
            
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
            
        }



        private void prepodName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            prepodGrid.ItemsSource = Global.lectorList;

        }



        private void addEntry_Click(object sender, RoutedEventArgs e)
        {
            Global.lectorList.Add(new Lector(prepodLastName.Text,prepodName.Text, prepodMiddleName.Text));
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
