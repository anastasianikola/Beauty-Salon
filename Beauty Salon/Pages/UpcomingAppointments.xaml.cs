using Beauty_Salon.Database;
using Beauty_Salon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Beauty_Salon.Pages
{
    public partial class UpcomingAppointments : Page
    {
        private DispatcherTimer _timer;

        public UpcomingAppointments()
        {
            InitializeComponent();
        }

        /*private void LoadAppointments()
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            var appointments = DatabaseHelper.GetContext().ClientService
                .Where(cs => cs.StartTime >= today && cs.StartTime < tomorrow.AddDays(1)) // записи на сегодня и завтра
                .OrderBy(cs => cs.StartTime)
                .Select(cs => new AppointmentView
                {
                    ServiceTitle = cs.Services.Title,   // название услуги
                    ClientName = cs.Client.LastName + " " + cs.Client.FirstName + " " + cs.Client.Patronymic,
                    ClientContact = $"Тел: {cs.Client.Phone} | Email: {cs.Client.Email}",
                    StartTime = cs.StartTime
                })
                .ToList();

            lvAppointments.ItemsSource = appointments;
        }*/

    }
}
