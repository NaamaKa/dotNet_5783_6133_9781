using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
using System.Xml.Linq;

namespace Pl.windows.Order
{
    /// <summary>
    /// Interaction logic for CartList.xaml
    /// </summary>
    public partial class CartList : Window
    {
        public CartList(BO.Cart myCart)
        {
            MyItemList =new( myCart.Items);
            MyCart = myCart;
            InitializeComponent();
            MYToTalSum = myCart.Price;
        }
        #region dependency proporties
        public static bool MesaggeToshow { get; set; }

        public double SelectedItemAmount
        {
            get { return (double)GetValue(SelectedItemAmountProperty); }
            set { SetValue(SelectedItemAmountProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemAmountProperty =
            DependencyProperty.Register("SelectedItemAmount", typeof(double), typeof(CartList));



        public ObservableCollection<BO.OrderItem?> MyItemList
        {
            get { return (ObservableCollection<BO.OrderItem?>)GetValue(MyItemListProperty); }
            set { SetValue(MyItemListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyItemListProperty =
            DependencyProperty.Register("MyItemList", typeof(ObservableCollection<BO.OrderItem?>), typeof(CartList));


        public BO.Cart MyCart
        {
            get { return (BO.Cart)GetValue(MyCartProperty); }
            set { SetValue(MyCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCartProperty =
            DependencyProperty.Register("MyCart", typeof(BO.Cart), typeof(CartList));

        public double MYToTalSum
        {
            get { return (double)GetValue(MYToTalSumProperty); }
            set { SetValue(MYToTalSumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MYToTalSum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MYToTalSumProperty =
            DependencyProperty.Register("MYToTalSum", typeof(double), typeof(CartList));
        #endregion
        #region methods
        private void plus_Button_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem obj = ((FrameworkElement)sender).DataContext as BO.OrderItem;
            int num = obj.NumInOrder;
            UpdatePlus(num - 1);
        }
        private void minus_Button_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem obj = ((FrameworkElement)sender).DataContext as BO.OrderItem;
            int num = obj.NumInOrder;
            UpdateMinus(num - 1);
        }

        public void UpdatePlus(int index)
        {
            MyItemList[index].Amount += 1;
            MyItemList[index].TotalPrice += MyItemList[index].Price;
            MyCart.Price += MyItemList[index].Price;
            MYToTalSum += MyItemList[index].Price;
            MyItemList.Insert(index, MyItemList[index]);
            MyItemList.RemoveAt(index + 1);
        }
        public void UpdateMinus(int index)
        {
            if (MyItemList[index].Amount >= 1)
            {
                MyItemList[index].Amount -= 1;
                MyItemList[index].TotalPrice -= MyItemList[index].Price;
                MyCart.Price -= MyItemList[index].Price;
                MYToTalSum -= MyItemList[index].Price;
                MyItemList.Insert(index, MyItemList[index]);
                MyItemList.RemoveAt(index + 1);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new NewOrder(MyCart).Show();
        }
        #endregion
    }
}
