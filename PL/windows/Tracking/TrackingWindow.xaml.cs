using BO;
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

namespace Pl.windows.Tracking
{
    /// <summary>
    /// Interaction logic for TrackingWindow.xaml
    /// </summary>
    public partial class TrackingWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public string ExceText
        {
            get { return (string)GetValue(ExceTextProperty); }
            set { SetValue(ExceTextProperty, value); }
        }
        public static readonly DependencyProperty ExceTextProperty = DependencyProperty.Register(nameof(ExceText),
                                                                                                               typeof(string),
                                                                                                       typeof(TrackingWindow));
        public static readonly DependencyProperty MyIdProperty = DependencyProperty.Register(nameof(MyId),
                                                                                             typeof(int),
                                                                                     typeof(TrackingWindow));
        public int MyId
        {
            get { return (int)GetValue(MyIdProperty); }
            set { SetValue(MyIdProperty, value); }
        }
        public TrackingWindow()
        {
            InitializeComponent();
        }

        private void getButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new OrderTracking(MyId).Show();
                Close();
            }
            catch (BO.OrderNotExistsException ex)
            {
                ExceText = ex.Message;
            }

        }
        private void id_TextChanged(object sender, TextChangedEventArgs e)
        {
            ExceText = "";
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
