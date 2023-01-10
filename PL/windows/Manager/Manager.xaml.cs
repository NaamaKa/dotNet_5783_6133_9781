﻿using BO;
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
            OrdersList = bl!.Order.GetListOfOrders();
            IsReadOnly = isreadonly;
            ProductList = bl.Product.GetListOfProduct();
            Categorys = Enum.GetValues(typeof(BO.Enums.Category));
           
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(Manager));


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
      

        public Array Categorys { get; set; }

        public IEnumerable<BO.ProductForList?> ProductList
        {
            get { return (IEnumerable<BO.ProductForList?>)GetValue(ProductListProperty); }
            set { SetValue(ProductListProperty, value); }
        }
        public static readonly DependencyProperty ProductListProperty =
            DependencyProperty.Register("ProductList", typeof(IEnumerable<BO.ProductForList?>), typeof(Manager));

        public BO.ProductForList Selected
        {
            get { return (BO.ProductForList)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }
        public static readonly DependencyProperty SelectedProperty =
            DependencyProperty.Register("Selected", typeof(BO.ProductForList), typeof(Manager));

        public BO.ProductForList O_Selected
        {
            get { return (BO.ProductForList)GetValue(O_SelectedProperty); }
            set { SetValue(O_SelectedProperty, value); }
        }
        public static readonly DependencyProperty O_SelectedProperty =
            DependencyProperty.Register("O_Selected", typeof(BO.ProductForList), typeof(Manager));
        private void ProductListview_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BO.ProductForList p = Selected;
            new ProductMenu(p!.Id!, "update").ShowDialog();
        }
        public BO.Enums.Category Categoryselected { get; set; }
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? cat = Categoryselected.ToString();
            ProductList = bl!.Product!.GetProductForListByCategory(cat);
        }
      
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            int nextId = bl!.Product!.GetnextidFromDO();
            new ProductMenu(nextId, "add").ShowDialog();
        }

        private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
