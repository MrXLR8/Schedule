using System;
using System.Windows;
using System.Windows.Threading;

public partial class NotificationWindow : Window
{
    public NotificationWindow()
    {
        InitializeComponent();

        Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
        {
            var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
            var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

            this.Left = corner.X - this.ActualWidth - 100;
            this.Top = corner.Y - this.ActualHeight;
        }));
    }

    private void Storyboard_Completed(object sender, EventArgs e)
    {
        this.Close();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}