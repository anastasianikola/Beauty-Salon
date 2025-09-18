using Beauty_Salon.Database;
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
        public static bool IsAdminMode { get; private set; } = false;

        public PageServices()
        {
            InitializeComponent();

            LoadServices();
            DataContext = this;
            
            UpdateData();

        }
        public string[] SortingList { get; set; } = 
        {
            "Без сортировки",
            "Стоимость по возростанию",
            "Стоимость по убыванию"
        };
        public string[] FilterList { get; set; } =
       {
            "Все диапозоны",
            "от 0 до 5%",
            "от 5% до 15%",
            "от 15% до 30%",
            "от 30% до 70%",
            "от 70% до 100%",
        };

        private void UpdateData() 
        { 
            var result = DatabaseHelper.GetContext().Services.ToList();
            if(cmbSorting.SelectedIndex == 1)
                result = result.OrderBy(p => p.Cost).ToList();
            if (cmbSorting.SelectedIndex == 2)
                result = result.OrderByDescending(p => p.Cost).ToList();

            if(cmbFilter.SelectedIndex == 1)
                result = result.Where(p => p.Discount > 0 && p.Discount < 5).ToList();
            if (cmbFilter.SelectedIndex == 2)
                result = result.Where(p => p.Discount >= 5 && p.Discount < 15).ToList();
            if (cmbFilter.SelectedIndex == 3)
                result = result.Where(p => p.Discount >= 15 && p.Discount < 30).ToList();
            if (cmbFilter.SelectedIndex == 4)
                result = result.Where(p => p.Discount >= 30 && p.Discount < 70).ToList();
            if (cmbFilter.SelectedIndex == 5)
                result = result.Where(p => p.Discount >= 70 && p.Discount < 100).ToList();

            result = result.Where(p => p.Title.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            LViewProduct.ItemsSource = result;
            tbCountRow.Text = $"{result.Count} из 100";
        }
        private void LoadServices()
        { 
            var services = DatabaseHelper.GetContext().Services.ToList();

            LViewProduct.ItemsSource = services;
        }

        private void ServiceCard_Loaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as Grid;
            if (grid?.DataContext is Services service)
            {
                var tbOldCost = grid.FindName("tbOldCost") as TextBlock;
                var tbNewCost = grid.FindName("tbNewCost") as TextBlock;
                var tbDiscount = grid.FindName("tbDiscount") as TextBlock;
                var btnEdit = grid.FindName("btn_Edit") as Button;
                var btnDelete = grid.FindName("btn_Delete") as Button;
                var btnClientService = grid.FindName("btn_ClientService") as Button;

                if (service.Discount > 0)
                {
                    if (tbOldCost != null)
                        tbOldCost.TextDecorations = TextDecorations.Strikethrough;

                    if (tbNewCost != null)
                        tbNewCost.Visibility = Visibility.Visible;

                    if (tbDiscount != null)
                        tbDiscount.Visibility = Visibility.Visible;
                }
                else
                {
                    if (tbOldCost != null)
                        tbOldCost.TextDecorations = null;

                    if (tbNewCost != null)
                        tbNewCost.Visibility = Visibility.Collapsed;

                    if (tbDiscount != null)
                        tbDiscount.Visibility = Visibility.Collapsed;
                }

                if (MainWindow.IsAdminMode)
                {
                    btnEdit.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                    btnClientService.Visibility = Visibility.Visible;
                }
                else
                {
                    btnEdit.Visibility = Visibility.Collapsed;
                    btnDelete.Visibility = Visibility.Collapsed;
                    btnClientService.Visibility = Visibility.Collapsed;
                }
            }
        }


        private void LViewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }


        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditAndAddService(LViewProduct.SelectedItem as  Services));
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            var service = (sender as Button)?.DataContext as Services;

            if (service == null)
                return;
           
            bool hasClients = DatabaseHelper.GetContext()
                                .ClientService.Any(cs => cs.ServiceID == service.ID);

            if (hasClients)
            {
                MessageBox.Show(
                    $"Невозможно удалить услугу \"{service.Title}\", так как она уже забронирована клиентами.",
                    "Удаление невозможно",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // Подтверждение удаления
            var result = MessageBox.Show(
                $"Вы действительно хотите удалить услугу \"{service.Title}\"?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    DatabaseHelper.GetContext().Services.Remove(service);
                    DatabaseHelper.GetContext().SaveChanges();

                    MessageBox.Show("Услуга успешно удалена!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);

                    UpdateData(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_ClientService(object sender, RoutedEventArgs e)
        {
            var service = (sender as Button)?.DataContext as Services;
            if (service != null)
            {
                NavigationService.Navigate(new AddClientService(service));
            }
        }

    }
}
