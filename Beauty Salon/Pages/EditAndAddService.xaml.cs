using Beauty_Salon.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для EditAndAddService.xaml
    /// </summary>
    public partial class EditAndAddService : Page
    {
        Services services = new Services();
        public EditAndAddService(Services currentService)
        {
            InitializeComponent();
            if (currentService != null) 
            {
                services = currentService;

            }
            DataContext = services;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string imagesPath = "C:\\Users\\Anastasia\\source\\repos\\Beauty Salon\\Beauty Salon\\Resources";
            OpenFileDialog getImageDialog = new OpenFileDialog();

            getImageDialog.Filter = "Файды изображений: (*.png, *.jpg, *.jpeg)| *.png; *.jpg; *.jpeg";
            getImageDialog.InitialDirectory = $"{imagesPath}";
            if (getImageDialog.ShowDialog() == true)
            {
                string targetPath = System.IO.Path.Combine(imagesPath, getImageDialog.SafeFileName);
                File.Copy(getImageDialog.FileName, targetPath, true);


                services.MainImagePath = $"{getImageDialog.SafeFileName}";
                img.Source = new BitmapImage(new Uri(targetPath, UriKind.Absolute));
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            
            if (string.IsNullOrWhiteSpace(services.Title))
                errors.AppendLine("Название услуги обязательно!");
            
            bool isDuplicate = DatabaseHelper.GetContext().Services
                .Any(s => s.Title == services.Title && s.ID != services.ID);

            if (isDuplicate)
                errors.AppendLine("Услуга с таким названием уже существует!");
            
            if (services.Cost < 0)
                errors.AppendLine("Стоимость не может быть отрицательной!");
            
            int durationInMinutes = services.DurationInSeconds / 60;

            if (durationInMinutes <= 0)
                errors.AppendLine("Время услуги должно быть больше 0 минут!");

            if (durationInMinutes > 240)
                errors.AppendLine("Время услуги не может превышать 4 часа!");
            
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {                
                if (services.ID == 0)
                    DatabaseHelper.GetContext().Services.Add(services);

                DatabaseHelper.GetContext().SaveChanges();
                MessageBox.Show("Услуга успешно сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new PageServices());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageServices());
        }

        private void txtBox_Title_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBox_Time_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBox_Cost_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBox_Discount_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBox_Description_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
