using System.Windows;
using System.Windows.Controls;

namespace Battleship
{
	public partial class Page1 : Page
	{
		public Page1()
		{
			InitializeComponent();
		}

		private void Page2_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
			mainWindow.NavigateToPage2();
			MainWindow.page2.Button_Click_Simulation(MainWindow.page2.NewMap);
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
