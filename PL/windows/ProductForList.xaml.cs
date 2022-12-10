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
using BlApi;
using Bllmplementation;

namespace Pl.windows;

/// <summary>
/// Interaction logic for ProductForList.xaml
/// </summary>
public partial class ProductForList : Window
{
    IBl bl = new Bl();
    public ProductForList()
    {
        InitializeComponent();
        ProductListview.ItemsSource = bl!.Product!?.GetListOfProduct();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
    }
    public void UptadeListView()
    {
        ProductListview.ItemsSource = bl.Product?.GetListOfProduct();
    }
    private void ProductListview_MouseDoubleClick(object sender, MouseEventArgs e)
    {

        BO.ProductForList p = (BO.ProductForList)ProductListview.SelectedValue;
        new ProductMenu(p!.Id!, "update").Show();
        ProductListview.ItemsSource = bl.Product?.GetListOfProduct();
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CategorySelector.SelectedItem.ToString() != null)
        {
            string cat = CategorySelector.SelectedItem.ToString();
            ProductListview.ItemsSource = bl.Product!.GetProductForListByCategory(cat);
        }

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        int nextId = bl.Product!.GetnextidFromDO();
        new ProductMenu(nextId, "add").Show();

    }
}
