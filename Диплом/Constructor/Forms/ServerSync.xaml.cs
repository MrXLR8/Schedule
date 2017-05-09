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

namespace Builder
{
    /// <summary>
    /// Логика взаимодействия для ServerSync.xaml
    /// </summary>
    public partial class ServerSync : Window
    {

        TextBox[] ips;

        string ip = "";
        int portNumber;

        public ServerSync()
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
            if (portNumber <= 0) { Check.IsEnabled = false; port.Text = "0"; return; }
            if (portNumber > 65535) { Check.IsEnabled = false; port.Text = "0"; return; }
            if (string.IsNullOrWhiteSpace(ip)) {Check.IsEnabled = false; return; }

            Check.IsEnabled = true;

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

        private void Check_Click(object sender, RoutedEventArgs e)
        {

            Schedule toSend = new Schedule();
            toSend.fillMe();
            bool answer;
            try
            {
                answer = NetFunctions.compareSchedule(toSend);
            }
            catch (Exception exc)
            {
                Status.Content = "[Не удалось соединиться]";
                return;
            }
            Send.IsEnabled = answer;
            Get.IsEnabled = answer;

            if (answer) Status.Content = "[Версии расписаний отличаються]";
            else { Status.Content = "[Версии расписаний идентичны]"; }

        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            Schedule toSend = new Schedule();
            toSend.fillMe();
            bool answer;
            try
            {
                answer = NetFunctions.sendSchedule(toSend);
            }
            catch (Exception exc)
            {
                Status.Content = "[Не удалось соединиться]";
                return;
            }

            if (answer) Status.Content = "[Расписание успешно загружено]";
            else { Status.Content = "[Расписание не установлено]"; }


        }

        private void Get_Click(object sender, RoutedEventArgs e)
        {
            Schedule recive = NetFunctions.getSchedule();
            recive?.applyMe();
        }
    }
}
