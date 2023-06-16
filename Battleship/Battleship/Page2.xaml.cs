using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace Battleship
{
	public partial class Page2 : Page
	{
		public BattleField bs = new BattleField();

		public Page2()
		{
			DataContext = bs;
			InitializeComponent();
		}

		public BattleField ReadBs()
		{
			return bs;
		}

		public void Button_Click_Simulation(Button button)
		{
			bs = new BattleField(); //qmnis axal tamashs
			DataContext = bs; //aaxlebs monacemebs
			ButtonAutomationPeer peer = new ButtonAutomationPeer(button);
			IInvokeProvider invokeProvider = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
			invokeProvider.Invoke(); //avtomaturi gilakze dachera
		}

		private void DisplayMap_Click(object sender, RoutedEventArgs e) //rukis chveneba, daxuruli rom ar iyos
		{
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					bs.ChooseYourMap[i][j].SetState();
				}
			}
		}

		private void Page3_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.page3.ResultPanel.Visibility = Visibility.Collapsed;
			MainWindow.page3.NewGameButton.Visibility = Visibility.Visible;
			MainWindow.page3.ExitButton.Visibility = Visibility.Visible;
			MainWindow.page3.SetBs(ReadBs()); //gadavcemt chvens axal rukas
			MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
			mainWindow.NavigateToPage3();
		}

		private void NewMap_Click(object sender, RoutedEventArgs e)
		{
			Button_Click_Simulation(DisplayMap);
		}

		private void Main_menu_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
			mainWindow.NavigateToPage1();
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
