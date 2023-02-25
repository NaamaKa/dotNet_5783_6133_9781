using Pl.windows.Order;
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

namespace Pl.windows.Tracking
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public int orderId { get; set; }
        public BO.OrderTracking? orderTrackingToUp { get; set; } = new();

        public OrderTracking(int id)
        {
            orderId = id;

            orderTrackingToUp = bl.Order!.GetOrderTracking(orderId);

            InitializeComponent();
        }

        private void orderButton_Click(object sender, RoutedEventArgs e)
        {
            new Order.OrderWindow(true,orderId).Show();
            Close();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new TrackingWindow().Show();
            Close();
        }
    }
}
