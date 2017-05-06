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

namespace Builder
{
    /// <summary>
    /// Логика взаимодействия для ServerSync.xaml
    /// </summary>
    public partial class ServerSync : Window
    {
        public ServerSync()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void port_TextChanged(object sender, TextChangedEventArgs e)
        {
            int portNumber = Convert.ToInt32(port.Text);

            if (portNumber <= 0) port.Clear();
            if (portNumber > 65535) port.Clear();
;
        }
    }
}
