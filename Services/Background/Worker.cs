using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ScheduleChangeAppWpf.Models;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Media;
using System.ComponentModel;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace ScheduleChangeAppWpf.Services.Background
{
    internal class Worker
    {
        internal async static Task<Models.Json.StatusOperation?> AddCellScheduleAsync(CellSchedule cellSchedule)
        {
            try
            {
                string responseString = string.Empty;

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ScheduleChangeAppWpf.Config.TokenChangeSchedule); ;
                request.RequestUri = new Uri(Config.UrlWebApi + "schedule");
                request.Method = HttpMethod.Post;

                var cellScheduleJson = JsonConvert.SerializeObject(cellSchedule);
                request.Content = new StringContent(cellScheduleJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    responseString = await responseContent.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Models.Json.StatusOperation>(responseString);
                }
                else
                {
                    return new Models.Json.StatusOperation()
                    {
                        Status = Types.Enums.StatusOperation.Error,
                        Message = response.StatusCode.ToString()
                    };
                }
            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);
                return new Models.Json.StatusOperation()
                {
                    Status = Types.Enums.StatusOperation.Error,
                    Message = error.ToString()
                };
            }
        }
        internal async static Task<Models.Json.StatusOperation?> AddCellScheduleExamAsync(CellScheduleExam cellScheduleExam)
        {
            try
            {
                string responseString = string.Empty;

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ScheduleChangeAppWpf.Config.TokenChangeSchedule); ;
                request.RequestUri = new Uri(Config.UrlWebApi + "ScheduleExam");
                request.Method = HttpMethod.Post;

                var cellScheduleJson = JsonConvert.SerializeObject(cellScheduleExam);
                request.Content = new StringContent(cellScheduleJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    responseString = await responseContent.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Models.Json.StatusOperation>(responseString);
                }
                else
                {
                    return new Models.Json.StatusOperation()
                    {
                        Status = Types.Enums.StatusOperation.Error,
                        Message = response.StatusCode.ToString()
                    };
                }
            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);
                return new Models.Json.StatusOperation()
                {
                    Status = Types.Enums.StatusOperation.Error,
                    Message = error.ToString()
                };
            }
        }

        internal static async Task<Models.Json.StatusOperation> DeleteCellScheduleAsync(CellSchedule cellSchedule)
        {
            try
            {
                string responseString = string.Empty;

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ScheduleChangeAppWpf.Config.TokenChangeSchedule); ;
                request.RequestUri = new Uri(Config.UrlWebApi + "schedule");
                request.Method = HttpMethod.Delete;

                var cellScheduleJson = JsonConvert.SerializeObject(cellSchedule);
                request.Content = new StringContent(cellScheduleJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    responseString = await responseContent.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Models.Json.StatusOperation>(responseString);
                }
                else
                {
                    return new Models.Json.StatusOperation()
                    {
                        Status = Types.Enums.StatusOperation.Error,
                        Message = response.StatusCode.ToString()
                    };
                }
            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);
                return new Models.Json.StatusOperation()
                {
                    Status = Types.Enums.StatusOperation.Error,
                    Message = error.ToString()
                };
            }
        }
        internal static async Task<Models.Json.StatusOperation> DeleteCellScheduleExamAsync(CellScheduleExam cellScheduleExam)
        {
            try
            {
                string responseString = string.Empty;

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ScheduleChangeAppWpf.Config.TokenChangeSchedule); ;
                request.RequestUri = new Uri(Config.UrlWebApi + "ScheduleExam");
                request.Method = HttpMethod.Delete;

                var cellScheduleJson = JsonConvert.SerializeObject(cellScheduleExam);
                request.Content = new StringContent(cellScheduleJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    responseString = await responseContent.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Models.Json.StatusOperation>(responseString);
                }
                else
                {
                    return new Models.Json.StatusOperation()
                    {
                        Status = Types.Enums.StatusOperation.Error,
                        Message = response.StatusCode.ToString()
                    };
                }
            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);
                return new Models.Json.StatusOperation()
                {
                    Status = Types.Enums.StatusOperation.Error,
                    Message = error.ToString()
                };
            }
        }

        internal static Border GetCellScheduleBorder(CellSchedule cellSchedule)
        {
            Border borderContainer = new Border()
            {
                CornerRadius = new CornerRadius(10),
                Background = (SolidColorBrush)App.Current.Resources["ContentLevel1BackgroundColorThemeLight"],
                Padding = new Thickness(0)
            };

            Grid grid = new Grid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition() {Width = new GridLength(40, GridUnitType.Pixel)},
                    new ColumnDefinition(),
                }
            };

            borderContainer.Child = grid;



            Label numberPairLabel = new Label()
            {
                VerticalAlignment =  VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = cellSchedule.NumberPair.ToString(),
                 Padding = new Thickness(0)
            };

            Border borderNumberPair = new Border()
            {
                Background = (SolidColorBrush)App.Current.Resources["ContentLevel1BackgroundColorThemeLight"],
                CornerRadius = new CornerRadius(10),
                Child = numberPairLabel
            };

            grid.Children.Add(borderNumberPair);



            StackPanel stackPanelPairInfo = new StackPanel()
            {
                Margin = new Thickness(10),
            };

            Grid.SetColumn(stackPanelPairInfo, 1);



            Grid gridTopContainer = new Grid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(){ Width = new GridLength(50, GridUnitType.Pixel)},
                    new ColumnDefinition()
                }
            };
            Grid gridBottomContainer = new Grid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(){ Width = new GridLength(50, GridUnitType.Pixel)},
                    new ColumnDefinition()
                }
            };

            stackPanelPairInfo.Children.Add(gridTopContainer);
            stackPanelPairInfo.Children.Add(gridBottomContainer);


            grid.Children.Add(stackPanelPairInfo);


            StackPanel stackPanelTimes = new StackPanel();

            gridTopContainer.Children.Add(stackPanelTimes);


            TextBlock timeStartTextBlock = new TextBlock()
            {
                Text = cellSchedule.TimesPair.TimeStart.ToString("HH:mm"),
                Padding = new Thickness(0),
                TextWrapping = TextWrapping.Wrap
            };
            stackPanelTimes.Children.Add(timeStartTextBlock);

            TextBlock timeEndTextBlock = new TextBlock()
            {
                Text = cellSchedule.TimesPair.TimeEnd.ToString("HH:mm"),
                Padding = new Thickness(0),
                TextWrapping = TextWrapping.Wrap
            };
            stackPanelTimes.Children.Add(timeEndTextBlock);


            StackPanel stackPanelAudiences = new StackPanel();

            gridBottomContainer.Children.Add(stackPanelAudiences);


            if (cellSchedule.Audiences != null)
            {
                foreach (var audience in cellSchedule.Audiences)
                {
                    TextBlock audienceTextBlock = new TextBlock()
                    {
                        Text = audience.Name,
                        Padding = new Thickness(0),
                        TextWrapping = TextWrapping.Wrap
                    };
                    stackPanelAudiences.Children.Add(audienceTextBlock);
                }
            }



            StackPanel stackPanelAcademicSubjects = new StackPanel();

            Grid.SetColumn(stackPanelAcademicSubjects, 1);

            gridTopContainer.Children.Add(stackPanelAcademicSubjects);

            if (cellSchedule.AcademicSubjects != null)
            {
                foreach (var academicSubject in cellSchedule.AcademicSubjects)
                {
                    TextBlock academicSubjectTextBlock = new TextBlock()
                    {
                        Text = academicSubject.Name,
                        Padding = new Thickness(0),
                        TextWrapping = TextWrapping.Wrap
                    };
                    stackPanelAcademicSubjects.Children.Add(academicSubjectTextBlock);
                }
            }




            StackPanel stackPanelTeachers = new StackPanel();

            Grid.SetColumn(stackPanelTeachers, 1);

            gridBottomContainer.Children.Add(stackPanelTeachers);


            if (cellSchedule.Teachers != null)
            {
                foreach (var teacher in cellSchedule.Teachers)
                {
                    TextBlock teacherTextBlock = new TextBlock()
                    {
                        Text = teacher.FullName,
                        Padding = new Thickness(0),
                        TextWrapping = TextWrapping.Wrap
                    };
                    stackPanelTeachers.Children.Add(teacherTextBlock);
                }
            }



            return borderContainer;
        }

        public static void FillComboboxHour(ComboBox comboBox)
        {
            for (int i = 0; i < 24; i++)
            {
                string hour = i < 10 ? $"0{i}" : $"{i}";

                comboBox.Items.Add(hour);
            }
        }

        internal static async Task<CellSchedule?> GetCellScheduleByGroup(Models.Group group, int numberPair, bool isChange, Types.Enums.CellScheduleType cellType , DateTime date, System.DayOfWeek dayOfWeek)
        {
            CellSchedule cellSchedule = null;

            try
            {
                if (group == null)
                    return null;


                string responseString = string.Empty;



                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(Config.UrlWebApi + $"schedule/cell?group_id={group.Id}&day_of_week={(int)dayOfWeek}&is_chnage={isChange}&number_pair={numberPair}&date={date.ToString("yyyy.MM.dd")}&cell_type={(int)cellType}");
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    responseString = await responseContent.ReadAsStringAsync();
                }



                if (!string.IsNullOrEmpty(responseString))
                {
                    var responseDeserializeString = JsonConvert.DeserializeObject(responseString).ToString();
                    var root = JsonConvert.DeserializeObject<Models.Json.JsonCellSchedule.Root>(responseDeserializeString);

                    if (root != null)
                    {
                        cellSchedule = root.CellSchedule;
                    }
                }

            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }

            return cellSchedule;
        }

        static internal async Task<Models.Group[]> GetGroupsAsync()
        {
            Models.Group[] groups = new Models.Group[0];
            try
            {
                string responseString = string.Empty;


                try
                {
                    HttpClient client = new HttpClient();
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(Config.UrlWebApi + "groups");
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("Accept", "application/json");

                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        HttpContent responseContent = response.Content;
                        responseString = await responseContent.ReadAsStringAsync();
                    }
                }
                catch (Exception error)
                {
                    //Services.Background.Worker.ErrorNotify(error);
                }



                if (!string.IsNullOrEmpty(responseString))
                {
                    var root = JsonConvert.DeserializeObject<ScheduleChangeAppWpf.Models.Json.JsonGroups.Root>(JsonConvert.DeserializeObject(responseString).ToString());

                    groups = root.Groups.ToArray();
                }
            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);
            }

            return groups;
        }

        internal static async Task<Teacher[]> GetTeachersAsync()
        {
            Teacher[] teachers = new Teacher[0];

            try
            {
                string responseString = string.Empty;


                try
                {
                    HttpClient client = new HttpClient();
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(Config.UrlWebApi + "teachers");
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("Accept", "application/json");

                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        HttpContent responseContent = response.Content;
                        responseString = await responseContent.ReadAsStringAsync();
                    }
                }
                catch (Exception error)
                {
                    //Services.Background.Worker.ErrorNotify(error);
                }



                if (!string.IsNullOrEmpty(responseString))
                {
                    var root = JsonConvert.DeserializeObject<ScheduleChangeAppWpf.Models.Json.JsonTeachers.Root>(JsonConvert.DeserializeObject(responseString).ToString());

                    teachers = root.Teachers.ToArray();
                }
            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);
            }

            return teachers;
        }
        internal static async Task<Audience[]> GetAudiencesAsync()
        {
            Audience[] audiences = new Audience[0];

            try
            {
                string responseString = string.Empty;


                try
                {
                    HttpClient client = new HttpClient();
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(Config.UrlWebApi + "Audiences");
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("Accept", "application/json");

                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        HttpContent responseContent = response.Content;
                        responseString = await responseContent.ReadAsStringAsync();
                    }
                }
                catch (Exception error)
                {
                    //Services.Background.Worker.ErrorNotify(error);
                }



                if (!string.IsNullOrEmpty(responseString))
                {
                    var root = JsonConvert.DeserializeObject<ScheduleChangeAppWpf.Models.Json.JsonAudiences.Root>(JsonConvert.DeserializeObject(responseString).ToString());

                    audiences = root.Audiences.ToArray();
                }
            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);
            }

            return audiences;
        }

        internal static void FillComboboxMinutes(ComboBox comboBox)
        {
            for(int i = 0; i < 60; i++)
            {
                string minutes = i < 10 ? $"0{i}" : $"{i}";

                comboBox.Items.Add(minutes);
            }
        }

        internal static void FillComboboxDayOfWeek(ComboBox comboBox)
        {
            comboBox.ItemsSource = new BindingList<Models.DayOfWeek>(new List<Models.DayOfWeek>()
            {
                 new Models.DayOfWeek(){ Name = "Понедельник", Value = System.DayOfWeek.Monday },
                 new Models.DayOfWeek(){ Name = "Вторник", Value = System.DayOfWeek.Tuesday },
                 new Models.DayOfWeek(){ Name = "Среда", Value = System.DayOfWeek.Wednesday },
                 new Models.DayOfWeek(){ Name = "Четверг", Value = System.DayOfWeek.Thursday },
                 new Models.DayOfWeek(){ Name = "Пятница", Value = System.DayOfWeek.Friday },
                 new Models.DayOfWeek(){ Name = "Суббота", Value = System.DayOfWeek.Saturday }
            });
        }

        internal static void FillComboBoxNumberPair(ComboBox comboBox, int lastNum)
        {
            comboBox.Items.Clear();

            for (int i = 1; i <= lastNum; i++)
            {
                comboBox.Items.Add(i);
            }
        }

        internal static async Task<Models.Json.StatusOperation?> SaveTimesPairAsync(TimesPair timesPair)
        {
            try
            {
                string responseString = string.Empty;

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ScheduleChangeAppWpf.Config.TokenChangeSchedule); ;
                request.RequestUri = new Uri(Config.UrlWebApi + "TimesPairs");
                request.Method = HttpMethod.Post;

                var cellScheduleJson = JsonConvert.SerializeObject(timesPair);
                request.Content = new StringContent(cellScheduleJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    responseString = await responseContent.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Models.Json.StatusOperation>(responseString);
                }
                else
                {
                    return new Models.Json.StatusOperation()
                    {
                        Status = Types.Enums.StatusOperation.Error,
                        Message = response.StatusCode.ToString()
                    };
                }
            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);
                return new Models.Json.StatusOperation()
                {
                    Status = Types.Enums.StatusOperation.Error,
                    Message = error.ToString()
                };
            }
        }


        internal static async Task<Models.Json.StatusOperation?> DeleteTimesPairAsync(TimesPair timesPair)
        {
            try
            {
                string responseString = string.Empty;

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ScheduleChangeAppWpf.Config.TokenChangeSchedule); ;
                request.RequestUri = new Uri(Config.UrlWebApi + "TimesPairs");
                request.Method = HttpMethod.Delete;

                var cellScheduleJson = JsonConvert.SerializeObject(timesPair);
                request.Content = new StringContent(cellScheduleJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    responseString = await responseContent.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Models.Json.StatusOperation>(responseString);
                }
                else
                {
                    return new Models.Json.StatusOperation()
                    {
                        Status = Types.Enums.StatusOperation.Error,
                        Message = response.StatusCode.ToString()
                    };
                }
            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);
                return new Models.Json.StatusOperation()
                {
                    Status = Types.Enums.StatusOperation.Error,
                    Message = error.ToString()
                };
            }
        }

        internal static async Task<List<Models.InformationSchedule>?> GetInformationsScheduleAsync(DateTime date)
        {
            try
            {
                string responseString = string.Empty;



                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ScheduleChangeAppWpf.Config.TokenChangeSchedule); ;
                request.RequestUri = new Uri(Config.UrlWebApi + $"informationsSchedule?date={date.ToString("yyyy.MM.dd")}");
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    responseString = await responseContent.ReadAsStringAsync();
                }



                if (!string.IsNullOrEmpty(responseString))
                {
                    var responseDeserializeString = JsonConvert.DeserializeObject(responseString).ToString();
                    var root = JsonConvert.DeserializeObject<Models.Json.JsonInformationsSchedule.Root>(responseDeserializeString);

                    if (root?.InformationsSchedule != null)
                    {
                        return root.InformationsSchedule;
                    }
                }

            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);

            }

            return null;
        }

        internal static async Task<Models.Json.StatusOperation> AddInformationScheduleAsync(InformationSchedule informationSchedule)
        {
            try
            {
                if (informationSchedule == null)
                {
                    return new Models.Json.StatusOperation()
                    {
                        Status = Types.Enums.StatusOperation.InvalidFormat,
                        Message = "informationsSchedule == null!"
                    };
                }


                string responseString = string.Empty;

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ScheduleChangeAppWpf.Config.TokenChangeSchedule); ;
                request.RequestUri = new Uri(Config.UrlWebApi + "InformationsSchedule");
                request.Method = HttpMethod.Post;

                var cellScheduleJson = JsonConvert.SerializeObject(informationSchedule);
                request.Content = new StringContent(cellScheduleJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    responseString = await responseContent.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Models.Json.StatusOperation>(responseString);
                }
                else
                {
                    return new Models.Json.StatusOperation()
                    {
                        Status = Types.Enums.StatusOperation.Error,
                        Message = response.StatusCode.ToString()
                    };
                }
            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);
                return new Models.Json.StatusOperation()
                {
                    Status = Types.Enums.StatusOperation.Error,
                    Message = error.ToString()
                };
            }
        }

        internal static async Task<Models.Json.StatusOperation> DeleteInformationSchedule(InformationSchedule informationSchedule)
        {
            try
            {
                string responseString = string.Empty;

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ScheduleChangeAppWpf.Config.TokenChangeSchedule); ;
                request.RequestUri = new Uri(Config.UrlWebApi + "InformationsSchedule");
                request.Method = HttpMethod.Delete;

                var cellScheduleJson = JsonConvert.SerializeObject(informationSchedule);
                request.Content = new StringContent(cellScheduleJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    responseString = await responseContent.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Models.Json.StatusOperation>(responseString);
                }
                else
                {
                    return new Models.Json.StatusOperation()
                    {
                        Status = Types.Enums.StatusOperation.Error,
                        Message = response.StatusCode.ToString()
                    };
                }
            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);
                return new Models.Json.StatusOperation()
                {
                    Status = Types.Enums.StatusOperation.Error,
                    Message = error.ToString()
                };
            }
        }

        internal static async Task<CellScheduleExam[]?> GetCellsScheduleExamsForGroupAsync(int groupId)
        {
            try
            {
                string responseString = string.Empty;



                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ScheduleChangeAppWpf.Config.TokenChangeSchedule); ;
                request.RequestUri = new Uri(Config.UrlWebApi + $"CellsScheduleExams?group_id={groupId}");
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    responseString = await responseContent.ReadAsStringAsync();
                }



                if (!string.IsNullOrEmpty(responseString))
                {
                    var responseDeserializeString = JsonConvert.DeserializeObject(responseString).ToString();
                    var root = JsonConvert.DeserializeObject<Models.Json.JsonCellsScheduleExams.Root>(responseDeserializeString);

                    if (root?.CellsScheduleExams != null)
                    {
                        return root.CellsScheduleExams.ToArray();
                    }
                }

            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);

            }

            return null;
        }

        internal static async Task<CellScheduleExam[]?> GetCellsScheduleExamsAsync()
        {
            try
            {
                string responseString = string.Empty;



                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ScheduleChangeAppWpf.Config.TokenChangeSchedule); ;
                request.RequestUri = new Uri(Config.UrlWebApi + $"CellsScheduleExams");
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    responseString = await responseContent.ReadAsStringAsync();
                }



                if (!string.IsNullOrEmpty(responseString))
                {
                    var responseDeserializeString = JsonConvert.DeserializeObject(responseString).ToString();
                    var root = JsonConvert.DeserializeObject<Models.Json.JsonCellsScheduleExams.Root>(responseDeserializeString);

                    if (root?.CellsScheduleExams != null)
                    {
                        return root.CellsScheduleExams.ToArray();
                    }
                }

            }
            catch (Exception error)
            {
                //Services.Background.Worker.ErrorNotify(error);

            }

            return null;
        }

        public static string GetCellScheduleExamType(Types.Enums.CellScheduleExamType cellScheduleExamType)
        {
            switch (cellScheduleExamType)
            {
                case Types.Enums.CellScheduleExamType.Сonsultation: return  "Консультация";
                case Types.Enums.CellScheduleExamType.Exam: return  "Экзамен";
                default: return "Нет данных";
            }
        }

        
    }
}
