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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Beauty_Salon.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageServices.xaml
    /// </summary>
    public partial class PageServices : Page
    {
        public PageServices()
        {
            InitializeComponent();
            LoadServices();

        }
        private void LoadServices()
        { 
            var services = DatabaseHelper.GetContext().Services.ToList();

            LViewProduct.ItemsSource = services;
        }

        private void LViewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
