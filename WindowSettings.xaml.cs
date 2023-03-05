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
	/// <summary>
	/// Логика взаимодействия для WindowSettings.xaml
	/// </summary>
	public partial class WindowSettings : Window
	{
		public WindowSettings()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Init();
        }

		private void Init()
		{
			//создать локальную бд мб json файл с настройками
		}

		private void TypeServerCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			TypeServerCheckBox.Content = "Локальный";
			//сохранять в настройки локальные
			Config.RestartUrlWeb(true);
		}

		private void TypeServerCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			TypeServerCheckBox.Content = "Удаленный";
			//сохранять в настройки локальные
			Config.RestartUrlWeb(false);
		}

		private void ShowMainMenuWindow_Click(object sender, RoutedEventArgs e)
		{
			new MainWindow().Show();
			Close();
		}
	}
}
