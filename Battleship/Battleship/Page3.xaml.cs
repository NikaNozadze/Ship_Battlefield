using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Battleship
{
	public partial class Page3 : Page
	{
		private BattleField bs = MainWindow.page2.ReadBs();
		private bool gameEnd = false;
		private bool youWin = false;
		private bool turn = true; //visi jeria
		private int opponent = 0;
		private int you = 0;
		private int x = 0;
		private int y = 0;

		public Page3()
		{
			DataContext = bs;
			InitializeComponent();
		}

		public void SetBs(BattleField b)
		{
			bs = b;
			DataContext = bs;
		}

		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			var brd = sender as Border; //brd aris ujra romelzec moxda daklikeba
			var EnemyCell = brd.DataContext as Cell;

			if (EnemyCell.Miss == Visibility.Collapsed && EnemyCell.Shoot == Visibility.Collapsed && turn == true && gameEnd == false)
			{
				StepCount();
				EnemyCell.SetState(); // chven visvrit. tu gemi arsebobs mashin Shoot-shi chaiwereba Visible tu arada miss - shi
				bs.Se1.Explosion(EnemyCell, 2); //tu gemi afetqdeba cecxli gaekideba
				YourStepCount(EnemyCell);
				OpponentShoot();
			}
		}

		private void OpponentShoot()
		{
			(x, y) = bs.Engine.XY();
			bs.ShootToOurMap(x, y);
			bs.Se2.Explosion(bs.OurMap[y][x], 1);
			OpponentStepCount(x, y);
			turn = true;
		}

		private void StepCount() //svlebs itvlis
		{
			int steps = int.Parse(bs.Steps);
			bs.Steps = (steps + 1).ToString();
		}

		private void OpponentStepCount(int x, int y) //oponentis svlebs itvlis
		{
			if (bs.OurMap[y][x].Ship == true)
			{
				opponent = int.Parse(bs.Opponent);
				bs.Opponent = (opponent + 1).ToString();
				if (opponent == 19)
				{
					gameEnd = true;
					GameResult(youWin);
				}
			}
		}

		private void YourStepCount(Cell cell) //shens svlebs itvlis
		{
			if (cell.Ship == true)
			{
				you = int.Parse(bs.You);
				bs.You = (you + 1).ToString();
				if (you == 19)
				{
					gameEnd = true;
					youWin = true;
					GameResult(youWin);
				}
			}
			turn = false;
		}

		private void GameResult(bool youWin)
		{
			ResultPanel.Visibility = Visibility.Visible;
			NewGameButton.Visibility = Visibility.Collapsed;
			ExitButton.Visibility = Visibility.Collapsed;

			if (opponent == 19 && you == 19)
			{
				MainWindow.page3.MapTextBlock.Text = "It's a draw!";
			}
			else if (youWin)
			{
				MainWindow.page3.MapTextBlock.Text = "Congratulations! You won!";
			}
			else
			{
				MainWindow.page3.MapTextBlock.Text = "Game over! You lost.";
			}
		}

		private void Page2_Click(object sender, RoutedEventArgs e)
		{
			gameEnd = false;
			youWin = false;

			MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
			mainWindow.NavigateToPage2();
			MainWindow.page2.Button_Click_Simulation(MainWindow.page2.NewMap); //rom sheqmnas axali ruka Page2-ze
		}

		private void Exit(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
