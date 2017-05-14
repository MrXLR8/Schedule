using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Commentary.xaml
    /// </summary>
    public partial class CommentaryForm : Window
    {
        Group selected;
        public CommentaryForm()
        {
            InitializeComponent();
        }

        public CommentaryForm(Group _input)
        {
            
            InitializeComponent();
            selected = _input;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
            */

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            groupName.Content = selected.name;
            Title = "Редактирование комменатриев для группы " + selected.name;
            LoadText();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string myText = new TextRange(CommentaryText.Document.ContentStart, CommentaryText.Document.ContentEnd).Text;
            selected.commentary = myText;
            SaveButton.IsEnabled = false;
        }

        private void CommentaryText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SaveButton.IsEnabled = true;
            }
            catch (NullReferenceException) { }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            
            LoadText();
            SaveButton.IsEnabled = false;

        }

        void LoadText()
        {
           var toSet = new TextRange(CommentaryText.Document.ContentStart, CommentaryText.Document.ContentEnd);
            
            toSet.Text = selected.commentary;
            SaveButton.IsEnabled = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Global.commentaryWindow = null;
        }
    }
}
