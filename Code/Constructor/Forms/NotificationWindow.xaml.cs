using System;
using System.Media;
using System.Windows;
using System.Windows.Threading;

public partial class NotificationWindow : Window
{
    public NotificationWindow()
    {
        InitializeComponent();

        Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
        {
            System.Drawing.Rectangle workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            System.Windows.Media.Matrix transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
            Point corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

            Left = corner.X - ActualWidth - 100;
            Top = corner.Y - ActualHeight;
        }));

        SoundPlayer player = new SoundPlayer("Resources/notif.wav");
        player.Load();
        player.Play();
    }

    private void Storyboard_Completed(object sender, EventArgs e)
    {
        Close();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}