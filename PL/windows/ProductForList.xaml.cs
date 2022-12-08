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
using BlImplementation;
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

    }
}
