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

namespace Pl.windows.Order
{
    /// <summary>
    /// Interaction logic for CartList.xaml
    /// </summary>
    public partial class CartList : Window
    {

        public CartList(BO.Cart myCart)
        {
            MyCartItems = myCart.Items;
            InitializeComponent();
        }
        public IEnumerable<BO.OrderItem?> MyCartItems
        {
            get { return (IEnumerable<BO.OrderItem?>)GetValue(MyCartItemsProperty); }
            set { SetValue(MyCartItemsProperty, value); }
        }
        public static readonly DependencyProperty MyCartItemsProperty =
            DependencyProperty.Register("MyCartItems", typeof(IEnumerable<BO.OrderItem?>), typeof(CartList));
    }
}
