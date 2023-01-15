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
    /// Interaction logic for ItemsInCart.xaml
    /// </summary>
    public partial class ItemsInCart : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ItemsInCart(int id)
        {
            ListOfItemsINCart = bl!.Order!.GetAllItemsToOrder(id);
            InitializeComponent();
        }


        public List<BO.OrderItem> ListOfItemsINCart
        {
            get { return (List<BO.OrderItem>)GetValue(ListOfItemsINCartProperty); }
            set { SetValue(ListOfItemsINCartProperty, value); }
        }

        public static readonly DependencyProperty ListOfItemsINCartProperty =
            DependencyProperty.Register("ListOfItemsINCart", typeof(List<BO.OrderItem>), typeof(ItemsInCart));


    }
}
