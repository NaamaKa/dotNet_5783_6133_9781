using BO;
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

namespace Pl.windows.Manager;

/// <summary>
/// Interaction logic for OrderForList.xaml
/// </summary>
public partial class Orders : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    
    public Orders()
    {
        OrdersList = bl!.Order.GetListOfOrders();
        InitializeComponent();
       
    }

    public IEnumerable<OrderForList> OrdersList
    {
        get { return (IEnumerable<OrderForList>)GetValue(OrdersListProperty); }
        set { SetValue(OrdersListProperty, value); }
    } 

    // Using a DependencyProperty as the backing store for OrdersList.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OrdersListProperty =
        DependencyProperty.Register("OrdersList", typeof(IEnumerable<OrderForList>), typeof(Orders));

}
