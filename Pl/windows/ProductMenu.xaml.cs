
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using static System.Collections.Specialized.BitVector32;

namespace Pl.windows;


/// <summary>
/// Interaction logic for ProductMenu.xaml
/// </summary>
public partial class ProductMenu : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    public ProductMenu(int _id, string _buttoncategory, Action<BO.ProductForList?> action)
    {
        MyProduct = new();
        InitializeComponent();
        this.Action = action;
        ID= _id;
        AddOrUpdateButton.Content = _buttoncategory;
        id.Text = _id.ToString();
      
        CategoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
      
    }
    public ProductMenu(int idToUpdate)
    {

        try
        {
            MyProduct = bl.Product.GetProductItem(idToUpdate);
            Index4ComboBox = (int)MyProduct.Category;
        }
        catch (ProductNotExistsException ex)
        {
            MessageBox.Show(ex.Message.ToString());
        }
        InitializeComponent();
        AddOrUpdateButton.Content = "update";
        CategoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));


    }

    public int ID { get; set; }
    private Action<BO.ProductForList?> Action;

    public Product MyProduct
    {
        get { return (Product)GetValue(MyProductProperty); }
        set { SetValue(MyProductProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyProductProperty =
        DependencyProperty.Register("MyProduct", typeof(Product), typeof(ProductMenu));

    public int Index4ComboBox { get; set; }

    private void AddOrUpdateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (AddOrUpdateButton.Content != null)
            {

                if ((string)AddOrUpdateButton.Content == "add")
                {
                    bl!.Product!.AddProduct(CreateProduct());
                    Action(bl.Product.GetProductForList(ID));

                    MessageBox.Show("the product " + name.Text + " add");

                }

                else
                {
                    bl!.Product!.UpdateProduct(CreateProduct());
                    MessageBox.Show("the product " + name.Text + " update");

                }

            }
            this.Close();
        }
        catch(NegativeIdException p)
        {
            Label NegativeIdExceptionLable = new Label()
            {
                Name = "NegativeIdExceptionLable",
                Margin = new Thickness(290, 105, 0, 0),
                Content = p.Message,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Foreground = new SolidColorBrush(Colors.White),
            };
            Grid.SetRow(NegativeIdExceptionLable, 1);
            MainGrid.Children.Add(NegativeIdExceptionLable);
        }
        catch (ProductAlreadyExistsException p)
        {
            Label ProductAlreadyExistsLable = new Label()
            {
                Name = "ProductAlreadyExistsLabel",
                Margin = new Thickness(290, 105, 0, 0),
                Content = p.Message,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Foreground = new SolidColorBrush(Colors.White),
            };
            Grid.SetRow(ProductAlreadyExistsLable, 1);
            MainGrid.Children.Add(ProductAlreadyExistsLable);
        }
        catch (EmptyNameException p)
        {
            Label EmptyNameExceptionLable = new Label()
            {
                Name = "EmptyNameExceptionLable",
                Margin = new Thickness(288, 157, 0, 0),
                Content = p.Message,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Foreground = new SolidColorBrush(Colors.White),
            };
            Grid.SetRow(EmptyNameExceptionLable, 1);
            MainGrid.Children.Add(EmptyNameExceptionLable);
        }
        catch (NegativePriceException p)
        {

            Label NegativePriceExceptionLable = new Label()
            {
                Name = "NegativePriceExceptionLabel",
                Margin = new Thickness(299, 246, 0, 0),
                Content = p.Message,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Foreground = new SolidColorBrush(Colors.White),
            };
            Grid.SetRow(NegativePriceExceptionLable, 1);
            MainGrid.Children.Add(NegativePriceExceptionLable);
        }
        catch (NegativeStockException p)
        {
            Label NegativeStockExceptionLable = new Label()
            {
                Name = "NegativeStockExceptionLable",
                Margin = new Thickness(299, 288, 0, 0),
                Content = p.Message,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Foreground = new SolidColorBrush(Colors.White),
            };
            Grid.SetRow(NegativeStockExceptionLable, 1);
            MainGrid.Children.Add(NegativeStockExceptionLable);
        }

    }
    private void id_TextChanged(object sender, TextChangedEventArgs e)
    {

        var child = MainGrid.Children.OfType<Control>().Where(x => x.Name == "NegativeIdExceptionLable" || x.Name == "ProductAlreadyExistsLabel").FirstOrDefault();
        if (child != null)
            MainGrid.Children.Remove(child);
    }
    private void name_TextChanged(object sender, TextChangedEventArgs e)
    {
        var child = MainGrid.Children.OfType<Control>().Where(x => x.Name == "NegativePriceExceptionLabel").FirstOrDefault();
        if (child != null)
            MainGrid.Children.Remove(child);
    }

    private void price_TextChanged(object sender, TextChangedEventArgs e)
    {
        var child = MainGrid.Children.OfType<Control>().Where(x => x.Name == "NegativePriceExceptionLabel").FirstOrDefault();
        if (child != null)
            MainGrid.Children.Remove(child);
    }


    private void inStock_TextChanged(object sender, TextChangedEventArgs e)
    {
        var child = MainGrid.Children.OfType<Control>().Where(x => x.Name == "NegativeStockExceptionLable").FirstOrDefault();
        if (child != null)
            MainGrid.Children.Remove(child);

    }
    private BO.Product CreateProduct()
    {
        BO.Product tempP = new()
        {
            ID = int.Parse(id.Text),
            Price = float.Parse(price.Text),
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




