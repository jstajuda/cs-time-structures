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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TimeStructures;

namespace TimeAppStoper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Time time = new Time();
        private TimePeriod tick = new TimePeriod(1);
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.WidthAndHeight;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void TimerStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Start();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.time = this.time + this.tick;
            TimeLabel.Content = this.time.ToString();
        }

        private void TimerStopButton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void TimerRestartButton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            this.time = new Time();
            TimeLabel.Content = this.time.ToString();
        }
    }
}
