﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Builder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        System.Windows.Forms.NotifyIcon ni;
        bool VIEWMODE { get => Global.VIEWMODE; set => Global.VIEWMODE = value; }

        public ObservableCollection<LectionSwap> swapListToAdd = new ObservableCollection<LectionSwap>();
        public List<ListBox> days;

        public MainWindow()
        {




            Global.prepodWindow = new LectorsForm();
            Global.predmetWindow = new PredmetsForm();
            Global.classesWindow = new ClassesForm();
            Global.intervalWindow = new IntervalsForm();
            Global.syncForm = new ServerSync();
            Global.syncGroupForm = new ServerSyncGroup();
            Global.aboutForm = new About();
            Global.main = this;


            #region Инициализая глобальных списков

            // Global.editorWindow.prepodGrid.ItemsSource = Global.lectorList;


            Global.lectorList = new ObservableCollection<Lector>();
            Global.predmetList = new ObservableCollection<string>();
            Global.classList = new ObservableCollection<int>();
            Global.groupList = new ObservableCollection<Group>();
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
            dayInWeekCombo.ItemsSource = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };
            days = new List<ListBox>() { chMonday, chTuesday, chThursday, chWednesday, chTuesday, chFriday, chSaturday, zmMonday, zmTuesday, zmWednesday, zmThursday, zmFriday, zmSaturday };

            try
            {

                Schedule get;
                string encrypted = FileInteraction.openFile("last.schd");
                string decrypted = Cipher.transcript(encrypted);
                get = JsonConvert.DeserializeObject<Schedule>(decrypted);
                get.applyMe();


            }
            catch (Exception) { }

            try
            {
                FileInteraction.loadNetSettings();
            }
            catch (Exception) { }

#if VIEWMODE
			VIEWMODE = true;
#endif

            if (Global.VIEWMODE) ViewModeFunc();

            Global.selectedGroup?.massReDraw();

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }


        void ViewModeFunc()
        {
            Grid left = (Grid)LeftPanel.Parent;
            Grid right = (Grid)RightPanel.Parent;
            Title = "Обозреватель расписаний. Федоров А.";
            left.Children.Remove(LeftPanel);
            right.Children.Remove(RightPanel);

            Global.RefreshTimer = new System.Windows.Threading.DispatcherTimer();
            Global.RefreshTimer.Tick += new EventHandler(RefreshTimer_Tick);
            Global.RefreshTimer.Interval = new TimeSpan(0, 0, 60);         // REFRESH INTERVAL
                                                                           //Global.RefreshTimer.Interval = new TimeSpan(6, 0, 0);
            Global.RefreshTimer.Start();

            Grid holder = (Grid)MenuHold.Parent;
            holder.RowDefinitions[1].Height = new GridLength(0);

            left.Children.Add(Global.syncGroupForm);
            commentary.Visibility = Visibility.Visible;

            #region tray


            ni = new System.Windows.Forms.NotifyIcon()
            {
                Icon = new System.Drawing.Icon("Resources/AppICO.ico"),
                Visible = true
            };
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    Show();
                    WindowState = System.Windows.WindowState.Normal;
                };

            StateChanged += Window_StateChanged;
            #endregion


            try
            {
                Global.selectedGroup = Global.groupList[0];
            }
            catch (ArgumentOutOfRangeException) { }
            //right.Children.Add(toAdd);

        }

        private async void RefreshTimer_Tick(object sender, EventArgs e)
        {

            Global.RefreshTimer.IsEnabled = false;
            Task<Group> task = Task.Run(() => Tick());
            Group Tick()
            {
                {
                    try
                    {
                        if (Global.selectedGroup != null)
                        {
                            if (NetFunctions.ip != "" & NetFunctions.portNumber != 0)
                            {
                                if (NetFunctions.compareGroup(Global.selectedGroup))
                                {
                                    return NetFunctions.GroupDownload(Global.selectedGroup.name);
                                }
                            }
                        }
                    }
                    catch (Exception) { return null; }
                }
                return null;
            }
            Group downloaded = await task;
            Global.RefreshTimer.IsEnabled = true;
            if (downloaded == null) return;

            NotificationWindow notif = new NotificationWindow();
            notif.Show();
            Global.syncGroupForm.installOneGroup(downloaded);
            Global.selectedGroup.massReDraw();

        }
        private void addGroup_Click(object sender, RoutedEventArgs e)
        {
            string text = groupNameText.Text;
            Group toAdd;
            if (!string.IsNullOrEmpty(text))
            {
                toAdd = new Group(text);
                Global.groupList.Add(toAdd);
                GroupListBox.SelectedItem = toAdd;
                groupNameText.Clear();
            }
        }

        private void deleteGroup_Click(object sender, RoutedEventArgs e)
        {
            Global.groupList.Remove((Group)GroupListBox.SelectedItem);
        }

        private void GroupListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Global.resetForm();
            Global.selectedGroup = (Group)GroupListBox.SelectedItem;

            Global.commentaryWindow?.Close();
            //   Global.commentaryWindow = null;

            try
            {
                Title = "Конструктор расписаний. Выбранная группа: " + Global.selectedGroup.name;
                RightPanel.IsEnabled = true;
                //   saveAsButton.IsEnabled = true;
                deleteGroupButton.IsEnabled = true;
                Global.selectedGroup.massReDraw();
            }
            catch (NullReferenceException) //если не выбранна ни одна группа, или была недавно удленна
            {
                Title = "Конструктор расписаний";
                Global.resetForm();
                deleteGroupButton.IsEnabled = false;
                RightPanel.IsEnabled = false;
                //  saveAsButton.IsEnabled = false;
            }
        }

        #region Редактирование / Создание 

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
            Global.intervalWindow.removeButton.IsEnabled = (Global.intervals.timeList.Count > 0);
            Global.intervalWindow.Activate();
        }

        private void timeCombo_MouseEnter(object sender, MouseEventArgs e)
        {
            timeCombo.ItemsSource = Global.intervals.timeList;
            ((ComboBox)sender).Items.Refresh();
        }

        private void createSwapButton_Click(object sender, RoutedEventArgs e)
        {
            if (swapDatePicker.SelectedDate != null)
            {
                if (predmetSwapCombo.SelectedItem != null)
                {
                    LectionSwap toAdd = new LectionSwap((DateTime)swapDatePicker.SelectedDate, predmetSwapCombo.SelectedItem.ToString());
                    swapListToAdd.Add(toAdd);
                    swapGrid.ItemsSource = swapListToAdd;
                }
            }
        }

        private void DeleteSwap_Click(object sender, RoutedEventArgs e)
        {
            LectionSwap toDel = (LectionSwap)swapGrid.SelectedItem;
            swapListToAdd.Remove(toDel);
        }

        private void AcceptChanges_Click(object sender, RoutedEventArgs e)
        {
            string radioPick()
            {
                if (ChRadio.IsChecked == true) return "ch";
                if (ZmRadio.IsChecked == true) return "zm";
                return "both";
            }
            try
            {
                string lection_name = paraCombo.SelectedItem.ToString();
                int classNumber = (int)classCombo.SelectedItem;
                Lector lector = (Lector)lectorCombo.SelectedItem;
                int interval = ((Interval)timeCombo.SelectedItem).index;
                string dayOfWeek = dayInWeekCombo.SelectedItem.ToString();
                string weekType = radioPick();
                Lection toAdd = new Lection(lection_name, interval, lector, classNumber)
                {
                    swapList = swapListToAdd
                };
                swapListToAdd = new ObservableCollection<LectionSwap>();

                foreach (LectionSwap c in toAdd.swapList)
                {
                    swapListToAdd.Add(c);
                }

                swapGrid.ItemsSource = swapListToAdd;

                if (weekType == "ch")
                {
                    Global.selectedGroup.chuslutel.pick(dayOfWeek).add(toAdd);

                }
                else if (weekType == "zm")
                {
                    Global.selectedGroup.znamenatel.pick(dayOfWeek).add(toAdd);
                }
                else
                {

                    Global.selectedGroup.chuslutel.pick(dayOfWeek).add(toAdd.Clone());
                    Global.selectedGroup.znamenatel.pick(dayOfWeek).add(toAdd.Clone());
                }
                Global.selectedGroup.massReDraw();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Одно или несколько полей незаполнено, изменения не внесены.");
            }

        }

        private void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            Lection target = Global.selectedLection;
            Week week = null;
            if (target.week == "ch") week = Global.selectedGroup.chuslutel;
            if (target.week == "zm") week = Global.selectedGroup.znamenatel;

            Day day = null;

            switch (target.day)
            {
                case "Monday":
                    day = week.Monday;
                    break;
                case "Tuesday":
                    day = week.Tuesday;
                    break;
                case "Wednesday":
                    day = week.Wednesday;
                    break;
                case "Thursday":
                    day = week.Thursday;
                    break;
                case "Friday":
                    day = week.Friday;
                    break;
                case "Saturday":
                    day = week.Saturday;
                    break;
            }


            day.lectionList.Remove(Global.selectedLection);
            Global.selectedLection = null;
            Global.setEdits(null);
            Global.selectedGroup.massReDraw();

        }

        private void CommentaryButton_Click(object sender, RoutedEventArgs e)
        {
            if (Global.commentaryWindow == null)
                Global.commentaryWindow = new CommentaryForm(Global.selectedGroup);

            Global.commentaryWindow.Show();
            Global.commentaryWindow.Focus();
        }
        #endregion



        private void days_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListBox caller = (ListBox)sender;
            LectionControl picked = (LectionControl)caller.SelectedItem;
            if (picked != null)
            {
                foreach (ListBox c in days)
                {
                    if (c != caller)
                        c.SelectedItem = null;
                }

                caller.SelectedItem = picked;
                Global.selectedLection = picked.Lection;
                Global.setEdits(Global.selectedLection);

                DeleteSelected.IsEnabled = true;
                DeleteToolTip.Lection = picked.Lection;
                DeleteToolTip.Visibility = Visibility.Visible;
            }
        }




        #region Кнопки нижней панели
        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            Data.reset();
            FileInteraction.fileName = "";
            FileInteraction.filePath = "";
            Global.setSaveButton();
        }

        public Schedule test = new Schedule(); //DELETE!
        public string json;



        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            Global.syncForm.Show();
            Global.syncForm.Activate();
        }
        private void openButton_Click(object sender, RoutedEventArgs e)
        {

            Schedule toSet = new Schedule();
            try
            {

                string fileText = FileInteraction.openFile();
                //toSet = Schedule.getSchedule(fileText);
                toSet = Schedule.fillMe(fileText);
                toSet.applyMe();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось занести данные с файла. Детали ошибки : \n" + exc.Message);
                Data.reset();
            }


        }
        private void saveAsButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule prepareToWire = new Schedule();
            prepareToWire.fillMe();

            FileInteraction.saveToFile(prepareToWire.json());
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule prepareToWire = new Schedule();
            prepareToWire.fillMe();

            FileInteraction.saveToSavedPath(prepareToWire.json());
        }

        #endregion

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                if (Visibility != Visibility.Hidden)
                    Hide();
            //    base.OnStateChanged(e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ni != null)
                ni.Visible = false;
        }

        public void Dispose()
        {
            ni.Dispose();
            Dispose();
            GC.SuppressFinalize(this);
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Global.aboutForm.Show();
            }
            catch (Exception)
            {
                Global.aboutForm = new About();
            }
            Global.aboutForm.Activate();
        }

        private void ManualButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("User_Manual_UA.pdf");
        }
    }



}


