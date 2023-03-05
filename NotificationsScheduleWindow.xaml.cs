using ScheduleChangeAppWpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ScheduleChangeAppWpf
{
    /// <summary>
    /// Логика взаимодействия для NotificationsScheduleWindow.xaml
    /// </summary>
    public partial class NotificationsScheduleWindow : Window
    {
        private ObservableCollection<Models.InformationSchedule> InformationsSchedule { get; } = new ObservableCollection<InformationSchedule>();

        private DateTime SelectedDate { get; set; } = DateTime.Now;

        public NotificationsScheduleWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListBoxNotificationSchedule.ItemsSource = InformationsSchedule;

            var informationsSchedule = await Services.Background.Worker.GetInformationsScheduleAsync(SelectedDate);

            InformationsSchedule.Clear();

            if (informationsSchedule != null)
            {
                foreach (var informationSchedule in informationsSchedule)
                {
                    InformationsSchedule.Add(informationSchedule);
                }
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var  dateTemp= DatePicker.SelectedDate;
            
            if(dateTemp != null)
            {
                SelectedDate = dateTemp ?? DateTime.Now;
            }
        }

        private async void AddNotificationScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var title = TitleNotificationScheduleTextBox.Text;
            var description = DescriptionNotificationScheduleTextBox.Text;

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Название не может быть пустым!");
                return;
            }

            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Описание не может быть пустым!");
                return;
            }

            if (SelectedDate < DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))
            {
                MessageBox.Show("Дата должна быть сегодня или позднее!");
                return;
            }


            var informationSchedule = new InformationSchedule() {
                Title = title,
                Description = description,
                Date = SelectedDate
            };

            var statusOperation= await Services.Background.Worker.AddInformationScheduleAsync(informationSchedule);

            if (statusOperation != null)
            {
                if (statusOperation.Status == Types.Enums.StatusOperation.Ok)
                {
                    MessageBox.Show("Успешно добавлено!");
                    InformationsSchedule.Add(informationSchedule);
                }
                else
                {
                    MessageBox.Show(statusOperation.Message);
                }

            }


        }

        private async void RemoveNotificationScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ListBoxNotificationSchedule.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (InformationsSchedule.Count > 0)
                {
                    var informationSchedule = InformationsSchedule[selectedIndex];

                    var statusOperation = await Services.Background.Worker.DeleteInformationSchedule(informationSchedule);

                    if (statusOperation.Status == Types.Enums.StatusOperation.Ok)
                    {
                        InformationsSchedule.Remove(informationSchedule);
                        MessageBox.Show(statusOperation.Message);
                    }
                    else
                    {
                        MessageBox.Show(statusOperation.Message);
                    }
                }
            }
        }

        private void ShowMainMenuWindow_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
