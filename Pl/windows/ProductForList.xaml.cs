using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;





namespace Pl.windows;

/// <summary>
/// Interaction logic for ProductForList.xaml
/// </summary>
public partial class ProductForList : Window
{
    static BlApi.IBl? bl = BlApi.Factory.Get();
    BO.Cart ThisCart = new Cart();
    
   
    public ProductForList()
    {
        
        ProductList = new(bl!.Product!.GetProductItemList());
        Categorys = Enum.GetValues(typeof(BO.Enums.Category));

        InitializeComponent();
    }
   
   
    public Array Categorys { get; set; } 
  
    public ObservableCollection<BO.ProductItem?> ProductList
    {
        get { return (ObservableCollection<BO.ProductItem?>)GetValue(ProductListProperty); }
        set { SetValue(ProductListProperty, value); }
    }
    public static readonly DependencyProperty ProductListProperty =
        DependencyProperty.Register("ProductList", typeof(ObservableCollection<BO.ProductItem?>), typeof(ProductForList));
    
    public BO.ProductItem Selected
    {
        get { return (BO.ProductItem)GetValue(SelectedProperty); }
        set { SetValue(SelectedProperty, value); }
    }
    public static readonly DependencyProperty SelectedProperty =
        DependencyProperty.Register("Selected", typeof(BO.ProductItem), typeof(ProductForList));

    private void ProductListview_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        BO.ProductItem p = Selected;
        new Order.ProductForCart(p!.Id!, ThisCart).ShowDialog();
    }
    public BO.Enums.Category? Categoryselected { get; set; }
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Categoryselected is not null)
            try
            {
                ProductList = new(bl.Product.GetProductItemList(e => (bool)(e?.productCategory.Equals((DO.Enums.Category)Categoryselected))));
            }
            catch (RequestedItemNotFoundException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new Order.CartList(ThisCart).ShowDialog();
    }

    private void Back_Button_Click_1(object sender, RoutedEventArgs e)
    {

        new MainWindow().Show();
        Close();
    }

    private void chkEnable_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ProductList = new(bl!.Product!.GetProductItemList());
        }
        catch (RequestedItemNotFoundException ex)
        {
            MessageBox.Show(ex.Message.ToString());
        }
        if (chkEnable.IsChecked == true)
        {

            var GropupingProducts = (from p in ProductList
                                     group p by p.Category into catGroup
                                     from pr in catGroup
                                     select pr).ToList();
            ProductList = new(GropupingProducts);
        }
    }

    private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.ProductItem p = Selected;
        new Order.ProductForCart(p!.Id!, ThisCart).ShowDialog();
    }
}
