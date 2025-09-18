using Beauty_Salon.Windows;
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

namespace Beauty_Salon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool IsAdminMode { get; private set; } = false;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Pages.PageServices());
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdminMode)
            {
                IsAdminMode = false;
                btn_AdmmimOrClient.Content = "Режим пользователя";
                btn_AddService.Visibility = Visibility.Hidden;
                btn_ListClient.Visibility = Visibility.Hidden;
            }
            else
            {
                var loginWindow = new AdminLoginWindow();
                loginWindow.Owner = this;

                if (loginWindow.ShowDialog() == true && loginWindow.IsAdmin)
                {
                    IsAdminMode = true;
                    btn_AdmmimOrClient.Content = "Режим администратора";
                    btn_AddService.Visibility = Visibility.Visible;
                    btn_ListClient.Visibility = Visibility.Visible;
                }
            }
        }

        private void btn_AddService_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.EditAndAddService(null));
        }

        private void btn_ListClient_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.UpcomingAppointments());
        }
    }
}
