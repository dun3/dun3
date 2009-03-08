using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Com.Hertkorn.FilterByExample.MV;

namespace Com.Hertkorn.FilterByExample
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Filter : Window
    {
        AddressFilterViewModel m_model = new AddressFilterViewModel();

        public Filter()
        {
            InitializeComponent();

            this.DataContext = m_model;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            m_model.SetFilter(txtStreet.Text, txtStreetNumber.Text, txtZip.Text, txtCity.Text, cmbCountry.SelectionBoxItem);
        }
    }
}
