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
    readonly IBl bl = new Bl();

    public ProductMenu(int _id, string _buttoncategory)
    {
        InitializeComponent();
        AddOrUpdateButton.Content = _buttoncategory;
        id.Text = _id.ToString();
        if (_buttoncategory == "update")
        {
            BO.Product p = bl.Product!.GetProductItem(_id)!;
            name.Text = p!.Name!;
            price.Text = p!.Price!.ToString();
            inStock.Text = p!.InStock!.ToString();
            CategoryComboBox.SelectedIndex = (int)p!.Category!;
        }
        CategoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
    }
    private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
    
    private void AddOrUpdateButton_Click(object sender, RoutedEventArgs e)
    {
        if (AddOrUpdateButton.Content != null)
        {

            if ((string)AddOrUpdateButton.Content == "add")
                bl.Product!.AddProduct(CreateProduct());
            else
            {
                bl.Product!.UpdateProduct(CreateProduct());
            }
        }
        this.Close();

    }

    private BO.Product CreateProduct()
    {
        BO.Product tempP = new()
        {
            ID = int.Parse(id.Text),
            Price = int.Parse(price.Text),
            Name = name.Text,
            InStock = int.Parse(inStock.Text)
        };
        String selected = CategoryComboBox.Text;
        switch (selected)
        {
            case "Home":
                tempP.Category = BO.Enums.Category.Home;
                break;
            case "Office":
                tempP.Category = BO.Enums.Category.Office;
                break;
            case "Kitchen":
                tempP.Category = BO.Enums.Category.Kitchen;
                break;
            case "Toys":
                tempP.Category = BO.Enums.Category.Toys;
                break;
            case "Textile":
                tempP.Category = BO.Enums.Category.Textile;
                break;
            case "Garden":
                tempP.Category = BO.Enums.Category.Garden;
                break;
        }

        return tempP;
    }

    
}




