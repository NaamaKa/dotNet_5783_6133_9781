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
using static BO.Enums;
using Pl.windows.Order;
using System.Collections.ObjectModel;
using DO;

namespace Pl.windows.Manager
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        static BlApi.IBl? bl = BlApi.Factory.Get();
        public Manager(bool isreadonly)
        {
            try
            {
                ProductList = new ObservableCollection<BO.ProductForList?>(bl.Product.GetListOfProduct().Cast<BO.ProductForList?>());
            }
            catch (DO.RequestedItemNotFoundException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            OrdersList = new(bl!.Order.GetListOfOrders());
            IsReadOnly = isreadonly;
            //ProductList = bl.Product.GetListOfProduct();
            Categorys = Enum.GetValues(typeof(BO.Enums.Category));

            InitializeComponent();
        }
        public ObservableCollection<OrderForList?> ?OrdersList
        {
            get { return (ObservableCollection<OrderForList?>)GetValue(OrdersListProperty); }
            set { SetValue(OrdersListProperty, value); }
        }
        public static readonly DependencyProperty? OrdersListProperty =
            DependencyProperty.Register(nameof(OrdersList), typeof(ObservableCollection<OrderForList?>), typeof(Orders));
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(Manager));


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        public Array Categorys { get; set; }

        public ObservableCollection<BO.ProductForList?>? ProductList
        {
            get { return (ObservableCollection<BO.ProductForList?>)GetValue(ProductListProperty); }
            set { SetValue(ProductListProperty, value); }
        }
        public static readonly DependencyProperty? ProductListProperty =
            DependencyProperty.Register("ProductList", typeof(ObservableCollection<BO.ProductForList?>), typeof(Manager));

        public BO.ProductForList Selected
        {
            get { return (BO.ProductForList)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }
        public static readonly DependencyProperty SelectedProperty =
            DependencyProperty.Register("Selected", typeof(BO.ProductForList), typeof(Manager));

        public BO.OrderForList O_Selected
        {
            get { return (OrderForList)GetValue(O_SelectedProperty); }
            set { SetValue(O_SelectedProperty, value); }
        }
        public static readonly DependencyProperty O_SelectedProperty =
            DependencyProperty.Register("O_Selected", typeof(OrderForList), typeof(Manager));
        public void updateProduct(BO.ProductForList? product)
        {
           
            
            int index = ProductList.IndexOf(product) + 1;
            ProductList[index] = product;
        }
        private void ProductListview_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BO.ProductForList p = Selected;
            new ProductMenu(p!.Id!, "update").ShowDialog();
        }
        public BO.Enums.Category? Categoryselected { get; set; } = null;
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? cat = Categoryselected.ToString();
            ProductList = new ObservableCollection<BO.ProductForList?>( bl!.Product!.GetProductForListByCategory(cat).Cast<BO.ProductForList?>());
        }
        public void addProduct(BO.ProductForList? pro) => ProductList.Insert(ProductList.Count, pro);

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            int nextId = bl!.Product!.GetnextidFromDO();
            new ProductMenu(nextId, "add",addProduct).ShowDialog();
        }

        private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderForList o = O_Selected;
            new OrderWindow(false, o.ID).ShowDialog();
        }

       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            try
            {
                OrdersList = new(bl.Order.GetListOfOrders());
            }
            catch (RequestedItemNotFoundException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            this.Close();
        }
    }
}
