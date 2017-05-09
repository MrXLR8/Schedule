using System;
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
using System.Windows.Shapes;
using Share;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Builder
{
    /// <summary>
    /// Логика взаимодействия для ServerSync.xaml
    /// </summary>
    public partial class ServerSyncGroup : Window
    {

        TextBox[] ips;

        string ip = "";
        int portNumber;
        ObservableCollection<string> groupList;
        public ServerSyncGroup()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;

        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void port_TextChanged(object sender, TextChangedEventArgs e)
        {
            int test;
            try
            {
                test = Convert.ToInt32(port.Text);
            }
            catch(FormatException exc)
            {
                port.Text = "0";
                return;
            }

            if (test <= 0) {port.Text = "0"; return; }
            if (test > 65535) { port.Text = "0"; return; }

            portNumber = test;

            checkBoth();
;
        }

        void checkBoth()
        {
            if (portNumber <= 0) { port.Text = "0"; RefreshGroups.IsEnabled = false; GetGroup.IsEnabled = false; return; }
            if (portNumber > 65535) { port.Text = "0"; RefreshGroups.IsEnabled = false; GetGroup.IsEnabled = false; return; }
            if (string.IsNullOrWhiteSpace(ip)) { RefreshGroups.IsEnabled = false; GetGroup.IsEnabled = false; return;  }

            RefreshGroups.IsEnabled = true;
            groupCombo_SelectionChanged(null, null);

            NetFunctions.ip = ip;
            NetFunctions.portNumber = portNumber;
            
        }
        private void ip_TextChanged(object sender, TextChangedEventArgs e)
        {
            ip = "";
            TextBox target = (TextBox)sender;
            int value;
            try
            {
                value = Convert.ToInt32(target.Text);
            }
            catch(FormatException exc) { return; }
            if (value < 0 || value > 255) { target.Text="0"; return; }

            try
            {
                
                foreach (TextBox t in ips)
                {
                    var octat = Convert.ToInt32(t.Text);
                    ip += octat;
                    ip += ".";
                }


                ip=ip.Remove(ip.Length-1);
                
            }
            catch (Exception exc) { }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ips = new TextBox[4] { ip1, ip2, ip3, ip4 };
        }


        Group group;
        private void GetGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Global.selectedGroup != null)
            {
                if (NetFunctions.compareGroup(Global.selectedGroup))
                {
                    Global.selectedGroup = NetFunctions.GroupDownload(groupCombo.SelectedItem as string);
                 
                }
            }
                else
                {
                    Global.selectedGroup = NetFunctions.GroupDownload(groupCombo.SelectedItem as string);
                   
                }
                
            }
            catch (Exception exc) { Status.Content = "[Не удалось получить группу]"; }
            Global.selectedGroup.massReDraw();
        }

        private void RefreshGroups_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> recived = NetFunctions.GroupListDownload();
                groupList = Collection.ToCollection<string>(recived);
                if (groupList.Count > 0) { groupCombo.ItemsSource = groupList; groupCombo.IsEnabled = true; }
            }
            catch (Exception exc) { Status.Content = "[Не удалось обновить список групп]"; }
           
        }

        private void groupCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selection = groupCombo.SelectedItem as string;
            if (string.IsNullOrEmpty(selection)) { GetGroup.IsEnabled = false; return;  }
            GetGroup.IsEnabled = true;
        }
    }
}
