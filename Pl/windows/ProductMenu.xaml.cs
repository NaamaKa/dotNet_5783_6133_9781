using BlApi;
using Bllmplementation;
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

namespace Pl.windows;


/// <summary>
/// Interaction logic for ProductMenu.xaml
/// </summary>
public partial class ProductMenu : Window
{
    IBl bl = new Bl();

    public ProductMenu(int _id, string _buttoncategory)
    {
        InitializeComponent();
        addOrUpdateButton.Content = _buttoncategory;
        id.Text = _id.ToString();
        if (_buttoncategory == "update")
        {
            BO.Product p = new();
            p = bl.Product!.GetProductItem(_id)!;
            name.Text = p!.Name!;
            price.Text = p!.Price!.ToString();
            inStock.Text = p!.InStock!.ToString();
            Category.SelectedIndex = (int)p!.Category!;
        }
        Category.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
    }
    private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void addOrUpdateButton_Click(object sender, RoutedEventArgs e)
    {
        if (addOrUpdateButton.Content != null)
        {
            if (addOrUpdateButton.Content == "add")
                bl.Product!.AddProduct(createProduct());
            else
            {
                bl.Product!.UpdateProduct(createProduct());
            }
            this.Close();
        }
    }

    private BO.Product createProduct()
    {
        BO.Product tempP = new BO.Product();
        tempP.ID = int.Parse(id.Text);
        tempP.Price = int.Parse(price.Text);
        tempP.Name = name.Text;
        tempP.InStock = int.Parse(inStock.Text);
        tempP.Category = BO.Enums.Category.Office;
        return tempP;
    }
}
