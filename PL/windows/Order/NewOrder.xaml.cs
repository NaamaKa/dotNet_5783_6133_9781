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

namespace Pl.windows.Order
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        
        public NewOrder(BO.Cart myCart)
        {
            MyCart=myCart;
            Address = null;
            Mail=null;
            Name= null; 
            Id = bl.Order!.GetnextidFromDO();
            InitializeComponent();
        }

        public BO.Cart MyCart
        {
            get { return (BO.Cart)GetValue(MyCartProperty); }
            set { SetValue(MyCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCartProperty =
            DependencyProperty.Register("MyCart", typeof(BO.Cart), typeof(NewOrder));


        public string? Name
        {
            get { return (string?)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(NewOrder));


        public string? Address
        {
            get { return (string?)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register("Address", typeof(string), typeof(NewOrder));



        public string? Mail
        {
            get { return (string?)GetValue(MailProperty); }
            set { SetValue(MailProperty, value); }
        }

        public static readonly DependencyProperty MailProperty =
            DependencyProperty.Register("Mail", typeof(string), typeof(NewOrder));



        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public static readonly DependencyProperty IdProperty =
        DependencyProperty.Register("Id", typeof(int), typeof(NewOrder));
        private BO.Cart CreateCart()
        {
            MyCart.CostumerName = Name;
            MyCart.CostumerAddress = Address;
            MyCart.CostumerEmail = Mail; 
            return MyCart; 
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.Cart!.OpenCart(CreateCart());
            }
            catch (EmptyNameException p)
            {
                Label EmptyNameExceptionLable = new Label()
                {
                    Name = "NegativeIdExceptionLable",
                    Margin = new Thickness(358, 108, 0, 0),
                    Content = p.Message,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Foreground = new SolidColorBrush(Colors.Red),
                };
                Grid.SetRow(EmptyNameExceptionLable, 1);
                MainGrid.Children.Add(EmptyNameExceptionLable);
                return;
            }
            catch (EmptyAddressException p)
            {
                Label EmptyAddressExceptionLable = new Label()
                {
                    Name = "NegativeIdExceptionLable",
                    Margin = new Thickness(358, 160, 0, 0),
                    Content = p.Message,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Foreground = new SolidColorBrush(Colors.Red),
                };
                Grid.SetRow(EmptyAddressExceptionLable, 1);
                MainGrid.Children.Add(EmptyAddressExceptionLable);
                return;
            }
            catch (EmptyEmailException p)
            {
                Label EmptyEmailExceptionLable = new Label()
                {
                    Name = "NegativeIdExceptionLable",
                    Margin = new Thickness(358, 224, 0, 0),
                    Content = p.Message,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Foreground = new SolidColorBrush(Colors.Red),
                };
                Grid.SetRow(EmptyEmailExceptionLable, 1);
                MainGrid.Children.Add(EmptyEmailExceptionLable);
                return;
            }
            try
            {
                bl!.Cart!.SubmitOrder(MyCart);
            }
            catch(WrongEmailException p)
            {
                Label WrongEmailExceptionLabel = new Label()
                {
                    Name = "WrongEmailExceptionLabel",
                    Margin = new Thickness(358, 224, 0, 0),
                    Content = p.Message,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Foreground = new SolidColorBrush(Colors.Red),
                };
                Grid.SetRow(WrongEmailExceptionLabel, 1);
                MainGrid.Children.Add(WrongEmailExceptionLabel);
                return;
            }
            new ThankYou(Name).Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
