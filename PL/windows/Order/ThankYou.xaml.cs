using System;
using System.Collections.Generic;
using System.IO.Pipes;
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

namespace Pl.windows.Order
{
    /// <summary>
    /// Interaction logic for ThankYou.xaml
    /// </summary>
    public partial class ThankYou : Window
    {


        public string CotumerName
        {
            get { return (string)GetValue(CotumerNameProperty); }
            set { SetValue(CotumerNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CotumerName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CotumerNameProperty =
            DependencyProperty.Register("CotumerName", typeof(string), typeof(ThankYou));


        public ThankYou(string name)
        {
            CotumerName = $"Thank you {name}!" +
                $"we got your order, and we will contect you about any progress..." +
                $"cant wait to see you again";
            InitializeComponent();
        }
    }
}
