using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pl.windows;
using Pl.windows.Manager;
using Pl.windows.Order;

namespace Pl;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

    }

    private void Manager_Button_Click(object sender, RoutedEventArgs e) {
        //new ProductForList().Show();
        new Manager(true).Show();

        this.Close();
    }

    private void Order_Button_Click(object sender, RoutedEventArgs e)
    {
        new ProductForList().ShowDialog();
        this.Close();
    }

    private void Tracking_Button_Click(object sender, RoutedEventArgs e)
    {

    }
}

