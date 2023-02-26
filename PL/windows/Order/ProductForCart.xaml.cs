using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Printing.IndexedProperties;
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
    /// Interaction logic for ProductForCart.xaml
    /// </summary>
    public partial class ProductForCart : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ProductForCart(int _id,BO.Cart myCart)
        {
            BO.Product p = bl.Product!.GetProductItem(_id)!;
            ID= p.ID;
            PName=p!.Name;
            PCategory = p.Category;
            Price = p.Price;
            if (p!.InStock! > 0)
                InStock = "true";
            else
                InStock = "false";
            MyCart =myCart;
            NumInTheCart = 1;
            InitializeComponent();
        }
        public string PName
        {
            get { return (string)GetValue(PNameProperty); }
            set { SetValue(PNameProperty, value); }
        }
        public static readonly DependencyProperty PNameProperty =
            DependencyProperty.Register("PName", typeof(string), typeof(ProductForCart));
        public double Price
        {
            get { return (double)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(double), typeof(ProductForCart));
        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(ProductForCart));
        public string InStock
        {
            get { return (string)GetValue(InStockProperty); }
            set { SetValue(InStockProperty, value); }
        }
        public static readonly DependencyProperty InStockProperty =
            DependencyProperty.Register("Instock", typeof(string), typeof(ProductForCart));
        public BO.Enums.Category PCategory
        {
            get { return (BO.Enums.Category)GetValue(PCategoryProperty); }
            set { SetValue(PCategoryProperty, value); }
        }
        public static readonly DependencyProperty PCategoryProperty =
            DependencyProperty.Register("PCategory", typeof(BO.Enums.Category), typeof(ProductForCart));
        public int Amount
        {
            get { return (int)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register("Amount", typeof(int), typeof(ProductForCart));
        public int NumInTheCart
        {
            get { return (int)GetValue(NumInTheCartProperty); }
            set { SetValue(NumInTheCartProperty, value); }
        }
        public static readonly DependencyProperty NumInTheCartProperty =
            DependencyProperty.Register("NumInTheCart", typeof(int), typeof(ProductForCart));
        public BO.Cart MyCart
        {
            get { return (BO.Cart)GetValue(MyCartProperty); }
            set { SetValue(MyCartProperty, value); }
        }
        public static readonly DependencyProperty MyCartProperty =
            DependencyProperty.Register("MyCart", typeof(BO.Cart), typeof(ProductForCart));
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem item = new BO.OrderItem()
            {
                ID = ID,
                Name = PName,
                Amount = Amount,
                Price = Price,
                TotalPrice = Price * Amount,
                NumInOrder = NumInTheCart
            };
            NumInTheCart++;
            
            MyCart!.Price += item.TotalPrice;
            try 
            {
                bl!.Cart!.AddItemToCart(MyCart, item);
                this.Close();
            }
            catch(ProductNotInStockException p) 
            {
                MessageBox.Show(p.Message);
            }
            
        }
    }
   
}
