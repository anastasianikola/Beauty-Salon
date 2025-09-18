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

namespace Beauty_Salon.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminLoginWindow.xaml
    /// </summary>
    public partial class AdminLoginWindow : Window
    {
        public bool IsAdmin { get; private set; } = false;
        public AdminLoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (pbCode.Password == "0000")
            {
                IsAdmin = true;
                MessageBox.Show("Вы вошли как администратор!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true; 
            }
            else
            {
                MessageBox.Show("Неверный код!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
