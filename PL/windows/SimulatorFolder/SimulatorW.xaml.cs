using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pl.windows.SimulatorFolder
{
    /// <summary>
    /// Interaction logic for SimulatorW.xaml
    /// </summary>
    public partial class SimulatorW : Window
    {
        private bool ableToClose = false;
        bool t = false;
        BlApi.IBl bl;
        string timerText { get; set; }
        public string NextStatus { get; set; } = "cvbn";
        public string PreviousStatus { get; set; } = "dfghii";
        BackgroundWorker worker;
        // BO.Order MyOrder = new();
        Tuple<BO.Order, int, string, string, string> orderAndTime;
        public static readonly DependencyProperty MyTimerProperty = DependencyProperty.Register(nameof(MyTimer),
                                                                                              typeof(string),
                                                                                      typeof(SimulatorW));
        public string MyTimer
        {
            get { return (string)GetValue(MyTimerProperty); }
            set { SetValue(MyTimerProperty, value); }
        }
        public static readonly DependencyProperty MyOrderProperty = DependencyProperty.Register(nameof(MyOrder),
                                                                                       typeof(BO.Order),
                                                                               typeof(SimulatorW));
        public BO.Order MyOrder
        {
            get { return (BO.Order)GetValue(MyOrderProperty); }
            set { SetValue(MyOrderProperty, value); }
        }
        private Stopwatch stopWatch;
        private bool isTimerRun;
        //=======progressBar variables
        Duration duration;
        DoubleAnimation doubleanimation;
        ProgressBar ProgressBar;
        //=======countdown timer variables
        DispatcherTimer _timer;
        TimeSpan _time;
        public SimulatorW(BlApi.IBl Bl)
        {
            InitializeComponent();
            bl = Bl;
            TimerStart();
        }
        void countDownTimer(int sec)
        {
            _time = TimeSpan.FromSeconds(sec);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (t is true)
                    _timer.Stop();
                t = true;
                tbTime.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        void TimerStart()
        {
            stopWatch = new Stopwatch();
            worker = new BackgroundWorker();
            worker.DoWork += TimerDoWork;
            worker.ProgressChanged += TimerProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            stopWatch.Restart();
            isTimerRun = true;
            worker.RunWorkerAsync();
        }

        void TimerDoWork(object sender, DoWorkEventArgs e)
        {
            
            Simulator.Simulator.ProgressChange += changeOrder;
            Simulator.Simulator.StopSimulator += Stop;
            Simulator.Simulator.run();
            while (isTimerRun)
            {
                worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        private void changeOrder(object sender, EventArgs e)
        {
            if (!(e is Details))
                return;

            Details details = e as Details;
            PreviousStatus = (details.order.ShippingDate == null) ? BO.Enums.OrderStatus.Submitted.ToString() : BO.Enums.OrderStatus.Sent.ToString();
            NextStatus = (details.order.ShippingDate == null) ? BO.Enums.OrderStatus.Sent.ToString() : BO.Enums.OrderStatus.Arrived.ToString();

            orderAndTime = new Tuple<BO.Order, int, string, string, string>(details.order, details.seconds / 1000, PreviousStatus, NextStatus, timerText);
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(changeOrder, sender, e);
            }
            else
            {
                DataContext = orderAndTime;
                countDownTimer(details.seconds / 1000);

            }
        }
        void TimerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            SimulatorTXTB.Text = timerText;
        }
        private void StopSimulatorBTN_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
            Simulator.Simulator.stoping();
            this.Close();
        }
        public void Stop(object sender, EventArgs e)
        {
            Simulator.Simulator.ProgressChange -= changeOrder;
            Simulator.Simulator.StopSimulator -= Stop;
            /*   while (!CheckAccess())
             {
                 Dispatcher.BeginInvoke(Stop, sender, e);
             }
             MessageBox.Show("successfully finish updating all orders");
             this.Close();*/
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(Stop, sender, e);
            }
            else
            {
                ableToClose = true;
                MessageBox.Show("complete updating");
                this.Close();
                ableToClose = false;

            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!ableToClose)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
