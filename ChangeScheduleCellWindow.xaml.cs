

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using ScheduleChangeAppWpf.Models;

namespace ScheduleChangeAppWpf
{
    /// <summary>
    /// Логика взаимодействия для ChangeScheduleCellWindow.xaml
    /// </summary>
    public partial class ChangeScheduleCellWindow : Window
    {
        internal bool IsChange { get; set; } = false;
        internal DateTime SelectedDate { get; set; } = DateTime.MinValue;
        internal System.DayOfWeek SelectedDayOfWeek { get; set; }
        internal TimesPair? SelectedTimesPair { get; set; }
        internal Types.Enums.CellScheduleType SelectedTypeCell { get; set; }
        internal int SelectedNumberPair { get; set; }
        internal Group? SelectedGroup { get; set; }
        internal List<Teacher> SelectedTeachers { get; set; } = new List<Teacher>();
        internal List<string> SelectedAudiences { get; set; } = new List<string>();
        internal List<string> SelectedAcademicSubjects { get; set; } = new List<string>();


        internal Teacher[] Teachers { get; set; } = new Teacher[0];
        internal Group[] Groups { get; set; } = new Group[0];


        public ChangeScheduleCellWindow()
        {
            InitializeComponent();
            Init();

        }

        private async void Init()
        {
            Teachers = await Services.Background.Worker.GetTeachersAsync();
            Groups = await Services.Background.Worker.GetGroupsAsync();
            ComboBoxNameTeachers.ItemsSource = new BindingList<Teacher>(Teachers);
            ComboBoxGroups.ItemsSource = new BindingList<Group>(Groups);

            InitComboBoxDayOfWeek();
            InitComboBoxTypeCell();


        }

        private void UploadCellSchedule()
        {
            try { 
            Task.Run(async () =>
            {
                try
                {
                    if (SelectedGroup == null)
                        return;

                    if (IsChange)
                    {
                        if (SelectedDate == DateTime.MinValue)
                        {
                            return;
                        }
                        else
                        {
                            SelectedDayOfWeek = SelectedDate.DayOfWeek;

                            /*Dispatcher.Invoke(() =>
                            {
                                FillComboBoxNumberPair();
                            });*/
                        }

                    }

                    Dispatcher.Invoke(() =>
                    {
                        StackPanelCellsSchedule.Children.Clear();
                    });
                    
                    var cellSchedule = await Services.Background.Worker.GetCellScheduleByGroup(SelectedGroup, SelectedNumberPair, IsChange, SelectedTypeCell, SelectedDate, SelectedDayOfWeek);

                    if (cellSchedule != null)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            Border border = Services.Background.Worker.GetCellScheduleBorder(cellSchedule);

                            StackPanelCellsSchedule.Children.Add(border);
                        });
                    }
                }
                catch(Exception error)
                {
                    MessageBox.Show(error.ToString());
                }
            });
			}
			catch (Exception error)
			{
				MessageBox.Show(error.ToString());
			}
		}

        private void FillComboBoxNumberPair()
        {
            if (SelectedDayOfWeek == System.DayOfWeek.Saturday)
            {
                Services.Background.Worker.FillComboBoxNumberPair(ComboBoxNumberPair, 6);
            }
            else
            {
                Services.Background.Worker.FillComboBoxNumberPair(ComboBoxNumberPair, 8);
            }
        }

        private void InitComboBoxTypeCell()
        {
            ComboBoxTypeCell.ItemsSource = new BindingList<Models.TypeCellSchedule>(new List<Models.TypeCellSchedule>()
            {
                new TypeCellSchedule(){ Name = "Общий", Value= Types.Enums.CellScheduleType.common},
                new TypeCellSchedule(){ Name = "Числитель", Value= Types.Enums.CellScheduleType.numerator},
                new TypeCellSchedule(){ Name = "Знаменатель", Value= Types.Enums.CellScheduleType.denominator},
            });
        }

        private void InitComboBoxDayOfWeek()
        {
            Services.Background.Worker.FillComboboxDayOfWeek(ComboBoxDayOfWeek);
        }

     
        private void AddNamePairButton_Click(object sender, RoutedEventArgs e)
        {
            string text = TextBoxNamePair.Text.Trim();

            if (!string.IsNullOrEmpty(text))
            {
                if (!ListBoxNamesPair.Items.Contains(text))
                {
                    ListBoxNamesPair.Items.Add(text);
                    SelectedAcademicSubjects.Add(text);
                }

                TextBoxNamePair.Text = "";
            }
        }
        private void RemoveNamePairButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxNamesPair.SelectedItem != null)
            {
                SelectedAcademicSubjects.Remove((string)ListBoxNamesPair.SelectedItem);
                ListBoxNamesPair.Items.Remove(ListBoxNamesPair.SelectedItem);
            }
        }
        private void AddNameTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            var teacher = (Teacher)ComboBoxNameTeachers.SelectedItem;

            if (teacher != null)
            {
                if (!SelectedTeachers.Contains(teacher))
                {
                    SelectedTeachers.Add(teacher);
                    ListBoxTeachers.Items.Add(teacher);
                }

                ComboBoxNameTeachers.Text = "";
            }
        }
        private void RemoveNameTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxTeachers.SelectedItem != null)
            {
                SelectedTeachers.Remove((Teacher)ListBoxTeachers.SelectedItem);
                ListBoxTeachers.Items.Remove(ListBoxTeachers.SelectedItem);
            }
        }
        
        private void AddNameAudienceButton_Click(object sender, RoutedEventArgs e)
        {
            string text = TextBoxNameAudiences.Text.Trim();

            if (!string.IsNullOrEmpty(text))
            {
                if (!ListBoxNamesAudiences.Items.Contains(text))
                {
                    ListBoxNamesAudiences.Items.Add(text);
                    SelectedAudiences.Add(text);
                }

                TextBoxNameAudiences.Text = "";
            }
        }
        private void RemoveNameAudienceButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxNamesAudiences.SelectedItem != null)
            {
                SelectedAudiences.Remove((string)ListBoxNamesAudiences.SelectedItem);
                ListBoxNamesAudiences.Items.Remove(ListBoxNamesAudiences.SelectedItem);
            }
        }

        private async void DeleteScheduleCellButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CellSchedule cellSchedule = new CellSchedule()
                {
                    IsChange = this.IsChange,
                    Group = SelectedGroup,
                    Teachers = SelectedTeachers,
                    DayOfWeek = SelectedDayOfWeek,
                    NumberPair = SelectedNumberPair,
                    TypeCell = SelectedTypeCell,
                };

                if (this.IsChange)
                {
                    cellSchedule.Date = SelectedDate;
                    SelectedDayOfWeek = SelectedDate.DayOfWeek;
                    cellSchedule.DayOfWeek = SelectedDayOfWeek;
                }

                var statusOperation = await ScheduleChangeAppWpf.Services.Background.Worker.DeleteCellScheduleAsync(cellSchedule);

                if (statusOperation != null)
                {
                    MessageBox.Show(statusOperation.Message);

                    if (statusOperation.Status == Types.Enums.StatusOperation.Ok)
                    {
                        ClearAllFields();
                    }

                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Не удалось удалить. Trace ->\n{error.Message}");
            }
        }
        private async void SaveScheduleCellButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedGroup == null)
                {
                    MessageBox.Show("Не выбрана группа!");
                    return;
                }

                if (SelectedNumberPair <= 0)
                {
                    MessageBox.Show("Не выбран номер пары!");
                    return;
                }

                if (!IsChange)
                {
                    if (SelectedAcademicSubjects.Count == 0)
                    {
                        MessageBox.Show("Нет ни одного выбранного предмета!");
                        return;
                    }
                }



                if (IsChange)
                {
                    if (SelectedDate < DateTime.Now)
                    {
                        MessageBox.Show("Дата должна быть сегодня или позже!");
                        return;
                    }
                }


                CellSchedule cellSchedule = new CellSchedule()
                {
                    IsChange = this.IsChange,
                    AcademicSubjects = new List<AcademicSubject>(),
                    Audiences = new List<Audience>(),
                    Group = SelectedGroup,
                    Teachers = SelectedTeachers,
                    DayOfWeek = SelectedDayOfWeek,
                    NumberPair = SelectedNumberPair,
                    TypeCell = SelectedTypeCell,
                };

                if (this.IsChange)
                {
                    cellSchedule.Date = SelectedDate;
                    SelectedDayOfWeek = SelectedDate.DayOfWeek;
                    cellSchedule.DayOfWeek = SelectedDayOfWeek;
                }

                foreach (var academicSubject in SelectedAcademicSubjects)
                {
                    cellSchedule.AcademicSubjects.Add(new AcademicSubject()
                    {
                        Name = academicSubject
                    });
                }

                foreach (var audiences in SelectedAudiences)
                {
                    cellSchedule.Audiences.Add(new Audience()
                    {
                        Name = audiences
                    });
                }

                

                
                //!!!!!!!!!!!!!!!!
                // если общий тип кидаем мы должны проверить по неким данным
                // что если есть числитель и или знаменатель их удаляем и добавляем новый общий

                var statusOperation = await ScheduleChangeAppWpf.Services.Background.Worker.AddCellScheduleAsync(cellSchedule);

                if (statusOperation != null)
                {
                    MessageBox.Show(statusOperation.Message);
                    if(statusOperation.Status == Types.Enums.StatusOperation.Ok)
                    {
                        ClearAllFields();
                    }
                }
                
            }
            catch (Exception error) 
            {
                MessageBox.Show($"Не удалось сохранить. Trace ->\n{error.Message}");
            }
        }

        private void ClearAllFields()
        {
            CheckBoxIsChange.IsChecked = false;
            ComboBoxDayOfWeek.Text = "";
            ComboBoxTypeCell.Text = "";
            DatePickerDateChangeSchedule.Text = "";
            ComboBoxNumberPair.Text = "";
            ComboBoxGroups.Text = "";
            ListBoxNamesPair.Items.Clear();
            TextBoxNamePair.Text = "";
            ListBoxNamesAudiences.Items.Clear();
            TextBoxNameAudiences.Text = "";
            ListBoxTeachers.Items.Clear();
            ComboBoxNameTeachers.Text = "";

            SelectedAcademicSubjects.Clear();
            SelectedAudiences.Clear();
            SelectedTeachers.Clear();
            SelectedGroup = null;
            SelectedNumberPair = 0;
            SelectedTypeCell = Types.Enums.CellScheduleType.common;
            SelectedTimesPair = null;
            SelectedDayOfWeek = System.DayOfWeek.Monday;
            SelectedDate = DateTime.MinValue;
            IsChange = false;
        }

        private void CheckBoxIsChange_Checked(object sender, RoutedEventArgs e)
        {
            IsChange = true;

            DatePickerDateChangeSchedule.IsEnabled = true;

            ComboBoxTypeCell.IsEnabled = false;

            UploadCellSchedule();

        }
        private void CheckBoxIsChange_Unchecked(object sender, RoutedEventArgs e)
        {
            IsChange = false;

            DatePickerDateChangeSchedule.IsEnabled = false;

            ComboBoxTypeCell.IsEnabled = true;


            UploadCellSchedule();

        }

        private void ComboBoxDayOfWeek_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                var dayOfWeek = (Models.DayOfWeek)comboBox.SelectedItem;
                if (dayOfWeek != null)
                {
                    SelectedDayOfWeek = dayOfWeek.Value;
                    FillComboBoxNumberPair();
                    UploadCellSchedule();
                }
            }
        }

        private void DatePickerDateChangeSchedule_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DatePickerDateChangeSchedule.SelectedDate != null)
            {
                SelectedDate = (DateTime)DatePickerDateChangeSchedule.SelectedDate;
                UploadCellSchedule();
            }
        }

        private void ComboBoxNumberPair_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ComboBoxNumberPair.SelectedItem;

            if (item != null)
            {
                int numberPair = (int)item;

                if (numberPair > 0)
                {
                    SelectedNumberPair = numberPair;
                    UploadCellSchedule();
                }
            }
        }

        private void ComboBoxTypeCell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                var typeCell = (Models.TypeCellSchedule)comboBox.SelectedItem;
                if (typeCell != null)
                {
                    SelectedTypeCell = typeCell.Value;
                    UploadCellSchedule();
                }
            }
        }

        private void ComboBoxGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                var group = (Models.Group)comboBox.SelectedItem;
                if (group != null)
                {
                    SelectedGroup = group;
                    UploadCellSchedule();
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
