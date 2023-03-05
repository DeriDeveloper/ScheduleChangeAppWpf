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
using System.Windows.Shell;

namespace ScheduleChangeAppWpf
{
    public partial class WindowChangeCellsScheduleExams : Window
    {
        public ObservableCollection<CellScheduleExam> CellsScheduleExams { get; set; } = new ObservableCollection<CellScheduleExam>();
        private CellScheduleExam? SelectedCellScheduleExam { get; set; }


        public WindowChangeCellsScheduleExams()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private async void Init()
        {
            Services.Background.Worker.FillComboboxHour(NumberHourComboBox);
            NumberHourComboBox.SelectedIndex = 0;
            Services.Background.Worker.FillComboboxMinutes(NumberMinutesComboBox);
            NumberMinutesComboBox.SelectedIndex = 0;


            var cellsScheduleExams = await Services.Background.Worker.GetCellsScheduleExamsAsync();
            if (cellsScheduleExams?.Length > 0)
            {
                foreach (var cellScheduleExam in cellsScheduleExams)
                {

                    CellsScheduleExams.Add(cellScheduleExam);
                }

                CellsScheduleExamsDataGrid.ItemsSource = CellsScheduleExams;
            }
            var teachers = await Services.Background.Worker.GetTeachersAsync();
            if (teachers?.Length > 0)
            {
                TeachersComboBox.ItemsSource = new BindingList<Teacher>(teachers);
            }

            var audiences = await Services.Background.Worker.GetAudiencesAsync();
            if (audiences?.Length > 0)
            {
                AudienceComboBox.ItemsSource = new BindingList<Audience>(audiences);
            }

            var groups = await Services.Background.Worker.GetGroupsAsync();
            if (groups?.Length > 0)
            {
                GroupComboBox.ItemsSource = new BindingList<Group>(groups);
            }


            var cellScheduleExamTypes = new List<CellScheduleExamType>()
            {
                new CellScheduleExamType()
                {
                     Type = Types.Enums.CellScheduleExamType.Сonsultation
                },
                new CellScheduleExamType()
                {
                     Type = Types.Enums.CellScheduleExamType.Exam
                }
            };

            CellScheduleExamTypeComboBox.ItemsSource = new BindingList<CellScheduleExamType>(cellScheduleExamTypes);


            ClearFieldForCellScheduleExam();
        }

        private void ShowMainMenuWindow_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void DeleteCellScheduleExamButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCellScheduleExam != null)
            {
                if (CellsScheduleExams != null)
                {
                    CellsScheduleExams.Remove(SelectedCellScheduleExam);
                    Services.Background.Worker.DeleteCellScheduleExamAsync(SelectedCellScheduleExam);
                    SelectedCellScheduleExam = null;
                }
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var dataGrid = (DataGrid)sender;
            var selectedCellScheduleExam = (CellScheduleExam)dataGrid.SelectedItem;

            if(selectedCellScheduleExam!= null)
            {
                SelectedCellScheduleExam = selectedCellScheduleExam;
            }
        }

        private async void AddCellsScheduleExamButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tempAudience = (Audience)AudienceComboBox.SelectedItem;

                if (tempAudience == null)
                {
                    MessageBox.Show("Аудитория не выбрана!");
                    return;
                }

                var group = (Group)GroupComboBox.SelectedItem;

                if (group == null)
                {
                    MessageBox.Show("Группа не выбрана!");
                    return;
                }

                var scheduleExamType = (CellScheduleExamType)CellScheduleExamTypeComboBox.SelectedItem;

                if (scheduleExamType == null)
                {
                    MessageBox.Show("Тип не выбрана!");
                    return;
                }

                DateTime date = DateTime.MinValue;

                DateTime.TryParse(DatePicker.Text, out date);

                if (date == DateTime.MinValue)
                {
                    MessageBox.Show("Тип не выбрана!");
                    return;
                }

                var title = TitleTextBox.Text;

                if (string.IsNullOrEmpty(title))
                {
                    MessageBox.Show("Нет названия!");
                    return;
                }

                var teacher = (Teacher)TeachersComboBox.SelectedItem;

                if (teacher == null)
                {
                    MessageBox.Show("Преподавтель не выбран!");
                    return;
                }

                var timeHour = NumberHourComboBox.Text;
                var timeMinutes = NumberMinutesComboBox.Text;


                DateTime time = DateTime.Parse(DateTime.MinValue.ToString("dd.MM.yyyy")+$" {timeHour}:{timeMinutes}:00");


                CellScheduleExam cellScheduleExam = new CellScheduleExam()
                {
                    Audiences = tempAudience,
                    AudienceId = tempAudience.Id,
                    CellScheduleExamType = scheduleExamType.Type,
                    Date = date,
                    Group = group,
                    GroupId = group.Id,
                    Teacher = teacher,
                    TeacherId = teacher.Id,
                    Title = title,
                    Time = time
                };

                var statusOperation =await Services.Background.Worker.AddCellScheduleExamAsync(cellScheduleExam);

                if (statusOperation != null)
                {
                    MessageBox.Show(statusOperation.Message);

                    if (statusOperation.Status == Types.Enums.StatusOperation.Ok)
                    {
                        if (CellsScheduleExams != null)
                        {
                            CellsScheduleExams.Add(cellScheduleExam);
                            ClearFieldForCellScheduleExam();
                        }
                    }
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void ClearFieldForCellScheduleExam()
        {
            DatePicker.Text = "";
            NumberHourComboBox.Text = "";
            NumberMinutesComboBox.Text = "";
            TitleTextBox.Text = "";
            TeachersComboBox.Text = "";
            CellScheduleExamTypeComboBox.Text = "";
            GroupComboBox.Text = "";
            AudienceComboBox.Text = "";
        }
    }
}
