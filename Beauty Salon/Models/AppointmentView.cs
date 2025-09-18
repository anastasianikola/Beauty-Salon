using System;
using System.Windows.Media;

namespace Beauty_Salon.Models
{
    public class AppointmentView
    {
        public string ServiceTitle { get; set; }
        public string ClientName { get; set; }
        public string ClientContact { get; set; }
        public DateTime StartTime { get; set; }

        public string TimeLeft
        {
            get
            {
                var diff = StartTime - DateTime.Now;
                if (diff.TotalMinutes <= 0)
                    return "Услуга уже началась или завершена";

                return $"{(int)diff.TotalHours} ч {diff.Minutes} мин до начала";
            }
        }

        public Brush TimeLeftColor
        {
            get
            {
                var diff = StartTime - DateTime.Now;
                return diff.TotalMinutes < 60 ? Brushes.Red : Brushes.Black;
            }
        }
    }
}
