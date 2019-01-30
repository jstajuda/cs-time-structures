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

namespace TimeAppClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Time time = new Time();
        private TimePeriod second = new TimePeriod(1);
        private TimePeriod minute = new TimePeriod(0, 1);
        private TimePeriod hour = new TimePeriod(1, 0, 0);
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            initClock();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            time += second;
            updateTimeLabel(time);
        }

        private void initClock()
        {
            var currentTime = DateTime.Now;
            time = new Time( (byte)currentTime.Hour, 
                             (byte)currentTime.Minute, 
                             (byte)currentTime.Second);

            updateTimeLabel(time);
        }

        private void updateTimeLabel(Time time)
        {
            TimeLabel.Content = time.ToString();
        }

        private void AddHoursButton_Click(object sender, RoutedEventArgs e)
        {
            time += hour;
            updateTimeLabel(time);
        }

        private void AddMinutesButton_Click(object sender, RoutedEventArgs e)
        {
            time += minute;
            updateTimeLabel(time);
        }

        private void AddSecondsButton_Click(object sender, RoutedEventArgs e)
        {
            time += second;
            updateTimeLabel(time);
        }

        private void SubHoursButton_Click(object sender, RoutedEventArgs e)
        {
            time -= hour;
            updateTimeLabel(time);
        }

        private void SubMinutesButton_Click(object sender, RoutedEventArgs e)
        {
            time -= minute;
            updateTimeLabel(time);
        }

        private void SubSecondsButton_Click(object sender, RoutedEventArgs e)
        {
            time -= second;
            updateTimeLabel(time);
        }
    }
}
