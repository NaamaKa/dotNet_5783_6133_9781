using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

    public partial class OrderWindow : Window
    {
        static readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public OrderWindow(bool isreadonly, int id)
        {
            
            ID = id;
            IsReadOnly = isreadonly;
            MyOrderForList = bl!.Order!.GetOrderDetails(id);
            ShipDate = null;
            if (MyOrderForList?.ShippingDate == null)
            {
                VisibilityShip = true;
                VisibiltyArrieved = false;
                LabelShip=false;
                LabelArrivle=false;
            }
            else
            {
                VisibilityShip = false;
                if (MyOrderForList.DeliveryDate == null)
                {
                    LabelShip=true;
                    VisibilityShip = false;
                    VisibiltyArrieved = true;
                    LabelArrivle = false;
                }
                else
                {
                    VisibilityShip = false;
                    VisibiltyArrieved = false;
                    LabelShip = true;
                    LabelArrivle=true;
                }
                ShipDate = MyOrderForList.ShippingDate;
                ArriveDate = MyOrderForList.DeliveryDate;

            }
            if (isreadonly == true)
            {
                VisibilityShip = false;
                VisibiltyArrieved = false;
                if(MyOrderForList?.ShippingDate == null)
                {
                    LabelShip = false;
                    LabelArrivle = false;
                }
                else if(MyOrderForList?.DeliveryDate == null)
                {
                    LabelShip = true;
                    LabelArrivle = false;
                }
                else
                {
                    LabelShip = true;
                    LabelArrivle = true;
                }
                ArriveDate = MyOrderForList.DeliveryDate;
                ShipDate = MyOrderForList.ShippingDate;
            }

            InitializeComponent();
        }
        #region Dependency propoties




        public bool LabelShip
        {
            get { return (bool)GetValue(LabelShipProperty); }
            set { SetValue(LabelShipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelShip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelShipProperty =
            DependencyProperty.Register("LabelShip", typeof(bool), typeof(OrderWindow));



        public bool LabelArrivle
        {
            get { return (bool)GetValue(LabelArrivleProperty); }
            set { SetValue(LabelArrivleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelArrivle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelArrivleProperty =
            DependencyProperty.Register("LabelArrivle", typeof(bool), typeof(OrderWindow));




        public bool VisibilityShip
        {
            get { return (bool)GetValue(VisibilityShipProperty); }
            set { SetValue(VisibilityShipProperty, value); }
        }

        public static readonly DependencyProperty VisibilityShipProperty =
            DependencyProperty.Register("VisibilityShip", typeof(bool), typeof(OrderWindow));



        public bool VisibiltyArrieved
        {
            get { return (bool)GetValue(VisibiltyArrievedProperty); }
            set { SetValue(VisibiltyArrievedProperty, value); }
        }

        public static readonly DependencyProperty VisibiltyArrievedProperty =
            DependencyProperty.Register("VisibiltyArrieved", typeof(bool), typeof(OrderWindow));





        public int ID { get; set; }
        public BO.Order? MyOrderForList
        {
            get { return (BO.Order?)GetValue(MyOrderForListProperty); }
            set { SetValue(MyOrderForListProperty, value); }
        }
        public static readonly DependencyProperty MyOrderForListProperty =
            DependencyProperty.Register("MyOrderForList", typeof(BO.Order), typeof(OrderWindow));


        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(OrderWindow));


        public DateTime? ShipDate
        {
            get { return (DateTime?)GetValue(ShipDateProperty); }
            set { SetValue(ShipDateProperty, value); }
        }


        public static readonly DependencyProperty ShipDateProperty =
            DependencyProperty.Register("ShipDate", typeof(DateTime?), typeof(OrderWindow));


        public DateTime? ArriveDate
        {
            get { return (DateTime?)GetValue(ArriveDateProperty); }
            set { SetValue(ArriveDateProperty, value); }
        }

        public static readonly DependencyProperty ArriveDateProperty =
            DependencyProperty.Register("ArriveDate", typeof(DateTime?), typeof(OrderWindow));




        #endregion


        public bool Prop { get; set; }
        #region methods
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new ItemsInCart(MyOrderForList!.ID).ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ShipDate = DateTime.Now;
            MyOrderForList= bl!.Order!.UpdateShipDate(ID);
            VisibilityShip = false;
            VisibiltyArrieved = true;
        }


        #endregion

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MyOrderForList= bl!.Order!.UpdateDeliveryDate(ID);
            VisibiltyArrieved = false;
            ArriveDate = DateTime.Now;
        }

      
    }
}
