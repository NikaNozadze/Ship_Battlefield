using System.Windows;

namespace Battleship
{
	public partial class MainWindow : Window
	{
		public static Page1 page1 = new Page1();
		public static Page2 page2 = new Page2();
		public static Page3 page3 = new Page3();

		public MainWindow()
		{
			InitializeComponent();
			MyFrame.Content = page1;
		}

		public void NavigateToPage1()
		{
			MyFrame.Content = page1;
		}

		public void NavigateToPage2()
		{
			MyFrame.Content = page2;
		}

		public void NavigateToPage3()
		{
			MyFrame.Content = page3;
		}
	}
}