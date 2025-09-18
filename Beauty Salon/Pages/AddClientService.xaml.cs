using Beauty_Salon.Database;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Beauty_Salon.Pages
{
    public partial class AddClientService : Page
    {
        private Services _service;

        public AddClientService(Services service)
        {
            InitializeComponent();
            _service = service;

            // Отображаем данные услуги
            tbServiceTitle.Text = _service.Title;
            tbDuration.Text = $"{_service.DurationInSeconds / 60} минут";

            // Загружаем клиентов
            cmbClients.ItemsSource = DatabaseHelper.GetContext().Clients
                .Select(c => new
                {
                    c.ID,
                    FullName = c.LastName + " " + c.FirstName + " " + c.Patronymic
                })
                .ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Проверка клиента
            if (cmbClients.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента!");
                return;
            }

            // Проверка даты
            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату!");
                return;
            }

            // Проверка времени
            if (string.IsNullOrWhiteSpace(txtTime.Text) || !TimeSpan.TryParse(txtTime.Text, out TimeSpan startTime))
            {
                MessageBox.Show("Введите корректное время в формате ЧЧ:ММ!");
                return;
            }

            DateTime startDateTime = dpDate.SelectedDate.Value.Date + startTime;
            DateTime endDateTime = startDateTime.AddSeconds(_service.DurationInSeconds);

            var clientService = new ClientService
            {
                ServiceID = _service.ID,
                ClientID = (int)cmbClients.SelectedValue,
                StartTime = startDateTime
            };

            try
            {
                DatabaseHelper.GetContext().ClientService.Add(clientService);
                DatabaseHelper.GetContext().SaveChanges();
                MessageBox.Show($"Клиент успешно записан на услугу {_service.Title}.\n" +
                                $"Начало: {startDateTime}\nОкончание: {endDateTime}");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
