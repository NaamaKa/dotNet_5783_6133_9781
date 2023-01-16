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
            MyCart= myCart;
            if (MyCart.Items == null)
                MesaggeToshow = true;
            else
                MesaggeToshow = false;
            InitializeComponent();
        }
        
        public BO.Cart MyCart
        {
            get { return (BO.Cart)GetValue(MyCartProperty); }
            set { SetValue(MyCartProperty, value); }
        }
        public static readonly DependencyProperty MyCartProperty =
            DependencyProperty.Register("MyCart", typeof(BO.Cart), typeof(CartList));


        
        public static bool MesaggeToshow { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new NewOrder(MyCart).Show();
        }
    }
}
