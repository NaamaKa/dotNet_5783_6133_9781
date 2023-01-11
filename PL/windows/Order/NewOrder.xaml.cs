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
        
        public NewOrder()
        {
            Id = bl.Order!.GetnextidFromDO();
            InitializeComponent();
        }
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public static readonly DependencyProperty IdProperty =
        DependencyProperty.Register("Id", typeof(int), typeof(NewOrder));
        private BO.Cart CreateCart()
        {
            return new BO.Cart()
            {
            CostumerName =costumerName.Text,
            CostumerAddress=costumerAddress.Text,
            CostumerEmail=costumerEmail.Text,
            };
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.Cart!.OpenCart(CreateCart());
                new ProductForList().Show();
                this.Close();
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
            } 
        }
    }
}
