using BO;
using System;
using System.Collections.Generic;
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
        
        ProductList = bl!.Product.GetListOfProduct();
        Categorys = Enum.GetValues(typeof(BO.Enums.Category));
        InitializeComponent();
    }
   
   
    public Array Categorys { get; set; } 
  
    public IEnumerable<BO.ProductForList?> ProductList
    {
        get { return (IEnumerable<BO.ProductForList?>)GetValue(ProductListProperty); }
        set { SetValue(ProductListProperty, value); }
    }
    public static readonly DependencyProperty ProductListProperty =
        DependencyProperty.Register("ProductList", typeof(IEnumerable<BO.ProductForList?>), typeof(ProductForList));
    //public BO.Cart ThisCart
    //{
    //    get { return (BO.Cart)GetValue(ThisCartProperty); }
    //    set { SetValue(ThisCartProperty, value); }
    //}
    //public static readonly DependencyProperty ThisCartProperty =
    //    DependencyProperty.Register("ThisCart", typeof(BO.Cart), typeof(ProductForList));
    public BO.ProductForList Selected
    {
        get { return (BO.ProductForList)GetValue(SelectedProperty); }
        set { SetValue(SelectedProperty, value); }
    }
    public static readonly DependencyProperty SelectedProperty =
        DependencyProperty.Register("Selected", typeof(BO.ProductForList), typeof(ProductForList));

    private void ProductListview_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        BO.ProductForList p = Selected;
        new Order.ProductForCart(p!.Id!, ThisCart).ShowDialog();
    }
    public BO.Enums.Category Categoryselected { get; set; }
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string? cat = Categoryselected.ToString();
        ProductList = bl!.Product!.GetProductForListByCategory(cat);
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new Order.CartList(ThisCart).ShowDialog();
    }

    private void Back_Button_Click_1(object sender, RoutedEventArgs e)
    {
          new MainWindow().Show();
    }
}
