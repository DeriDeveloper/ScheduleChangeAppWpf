using ScheduleChangeAppWpf.Models;
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

namespace ScheduleChangeAppWpf
{
    public partial class WindowChangeTimesPairs : Window
    {
        private int SelectedNumberPair { get; set; }
        private System.DayOfWeek SelectedDayOfWeek { get; set; }
        private int SelectedTimePairStartHour { get; set; }
        private int SelectedTimePairStartMinutes { get; set; }
        private int SelectedTimePairEndHour { get; set; }
        private int SelectedTimePairEndMinutes { get; set; }
        private bool IsChange { get; set; }
        private System.DateTime SelectedDate { get; set; }


        public WindowChangeTimesPairs()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Services.Background.Worker.FillComboboxHour(TimePairStartHour);
            Services.Background.Worker.FillComboboxHour(TimePairEndHour);
            Services.Background.Worker.FillComboboxMinutes(TimePairStartMinutes);
            Services.Background.Worker.FillComboboxMinutes(TimePairEndMinutes);
            Services.Background.Worker.FillComboboxDayOfWeek(ComboBoxDayOfWeek);

            RestartFields();
        }



        private void ComboBoxDayOfWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (ComboBox)sender;

            if (combobox != null)
            {
                SelectedDayOfWeek = ((Models.DayOfWeek)combobox.SelectedItem).Value;

                if (SelectedDayOfWeek == System.DayOfWeek.Saturday)
                {
                    Services.Background.Worker.FillComboBoxNumberPair(ComboBoxNumberPair, 6);
                }
                else
                {
                    Services.Background.Worker.FillComboBoxNumberPair(ComboBoxNumberPair, 8);
                }
            }
        }

        private void ComboBoxNumberPair_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (ComboBox)sender;

            if (combobox != null)
            {
                SelectedNumberPair = (int)combobox.SelectedValue;
            }
        }

        private void TimePairStartHour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (ComboBox)sender;

            if (combobox != null)
            {
                string numberString = (string)combobox.SelectedValue;

                if (numberString.Length > 0)
                {
                    if (numberString[0].Equals("0"))
                    {
                        numberString = numberString[1].ToString();
                    }

                    SelectedTimePairStartHour = int.Parse(numberString);
                }
            }
        }

        private void TimePairStartMinutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (ComboBox)sender;

            if (combobox != null)
            {
                string numberString = (string)combobox.SelectedValue;

                if (numberString.Length > 0)
                {
                    if (numberString[0].Equals("0"))
                    {
                        numberString = numberString[1].ToString();
                    }

                    SelectedTimePairStartMinutes = int.Parse(numberString);
                }
            }
        }

        private void TimePairEndHour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (ComboBox)sender;

            if (combobox != null)
            {
                string numberString = (string)combobox.SelectedValue;

                if (numberString.Length > 0)
                {
                    if (numberString[0].Equals("0"))
                    {
                        numberString = numberString[1].ToString();
                    }

                    SelectedTimePairEndHour = int.Parse(numberString);
                }
            }
        }

        private void TimePairEndMinutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (ComboBox)sender;

            if (combobox != null)
            {
                string numberString = (string)combobox.SelectedValue;

                if (numberString.Length > 0)
                {
                    if (numberString[0].Equals("0"))
                    {
                        numberString = numberString[1].ToString();
                    }

                    SelectedTimePairEndMinutes = int.Parse(numberString);
                }
            }
        }

        private void CheckBoxIsChange_Checked(object sender, RoutedEventArgs e)
        {
            IsChange = true;

            if (ComboBoxDayOfWeek != null)
            {
                ComboBoxDayOfWeek.IsEnabled = false;
            }

            if (DatePickerDateChange != null)
            {
                DatePickerDateChange.IsEnabled = false;
            }

            if (DeleteButton != null)
            {
                DeleteButton.IsEnabled = true;
            }

        }

        private void CheckBoxIsChange_Unchecked(object sender, RoutedEventArgs e)
        {
            IsChange = false;

            if (ComboBoxDayOfWeek != null)
            {
                ComboBoxDayOfWeek.IsEnabled = true;
            }

            if (DeleteButton != null)
            {
                DeleteButton.IsEnabled = false;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var timesPair = new TimesPair();

            timesPair.IsChange = IsChange;

            if (timesPair.IsChange)
            {
                if (SelectedDate == DateTime.MinValue)
                {
                    MessageBox.Show("Не выбрана дата для изменения");
                    return;
                }


                timesPair.Date = SelectedDate;
                timesPair.DayOfWeek = SelectedDate.DayOfWeek;
            }
            else
            {
                timesPair.DayOfWeek = SelectedDayOfWeek;
            }

            timesPair.NumberPair = SelectedNumberPair;
            timesPair.TimeStart = DateTime.Parse($"{SelectedTimePairStartHour}:{SelectedTimePairStartMinutes}");
            timesPair.TimeEnd = DateTime.Parse($"{SelectedTimePairEndHour}:{SelectedTimePairEndMinutes}");

            var statusOperation = await Services.Background.Worker.SaveTimesPairAsync(timesPair);

            if(statusOperation.Status == Types.Enums.StatusOperation.Ok)
            {
                MessageBox.Show("Успешно сохранено!");
            }
            else
            {
                MessageBox.Show(statusOperation.Message);
            }

            RestartFields();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var timesPair = new TimesPair();

            timesPair.IsChange = IsChange;

            if (timesPair.IsChange)
            {
                if (SelectedDate == DateTime.MinValue)
                {
                    MessageBox.Show("Не выбрана дата для изменения");
                    return;
                }


                timesPair.Date = SelectedDate;
                timesPair.DayOfWeek = SelectedDate.DayOfWeek;
            }
            else
            {
                MessageBox.Show("Нельзя удалить НЕ изменение!");
                return;
            }

            timesPair.NumberPair = SelectedNumberPair;

            var statusOperation = await Services.Background.Worker.DeleteTimesPairAsync(timesPair);

            if (statusOperation.Status == Types.Enums.StatusOperation.Ok)
            {
                MessageBox.Show("Успешно удалено!");
            }
            else
            {
                MessageBox.Show(statusOperation.Message);
            }
        }

        private void RestartFields()
        {
            CheckBoxIsChange.IsChecked = false;
            DatePickerDateChange.Text = "";
            ComboBoxDayOfWeek.SelectedIndex = 0;
            ComboBoxNumberPair.SelectedIndex = 0;
            TimePairStartHour.SelectedIndex = 0;
            TimePairStartMinutes.SelectedIndex = 0;
            TimePairEndHour.SelectedIndex = 0;
            TimePairEndMinutes.SelectedIndex = 0;
        }

        private void DatePickerDateChange_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerDateChange.SelectedDate != null)
            {
                SelectedDate = (DateTime)DatePickerDateChange.SelectedDate;
            }
        }

        private void ShowMainMenuWindow_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
