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

namespace FBABoxContentLabeler
{
    /// <summary>
    /// Interaction logic for WarehouseWindow.xaml
    /// </summary>
    public partial class WarehouseWindow : Window
    {
        #region Full Properties and Fields
        private AMZWarehouse whse;

        public AMZWarehouse Whse
        {
            get { return whse; }
            set { whse = value; }
        }

        #endregion

        #region Constructors
    

        /// <summary>
        /// Overloaded constructor takes a parameter.
        /// </summary>
        /// <param name="iwhse"> A AMZWarehouse object is passed to be filled by the Window's controls.</param>
        public WarehouseWindow(AMZWarehouse iwhse)
        {
            InitializeComponent();
            whse = iwhse;
            txb_WhseCode.Text= whse.WhseCode;
            txb_WhseName.Text= whse.Name ;
            txb_WhseAddress.Text=whse.Address ;
            txb_WhseCityStateZip.Text=whse.CityStateZip ;

        }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public WarehouseWindow() : this(new AMZWarehouse())
        {

        }


        #endregion

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            whse.WhseCode= txb_WhseCode.Text ;
            whse.Name=txb_WhseName.Text ;
            whse.Address= txb_WhseAddress.Text;
            whse.CityStateZip=txb_WhseCityStateZip.Text ;
            DialogResult = true;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
