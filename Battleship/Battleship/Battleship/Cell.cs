using System;
using System.Windows;

namespace Battleship
{
	public class Cell : ViewModelBase
	{
		private Visibility visibility = Visibility.Collapsed;
		private Visibility burn1 = Visibility.Collapsed;
		private Visibility burn2 = Visibility.Collapsed;
		private Random random = new Random();
		private double burn1Start = 0.1;
		private double burn2Start = 0.1;
		private char shipstate;
		private bool ship;
		private int x;
		private int y;

		public Cell(char state, int i, int j)
		{
			Ship = state != '*';
			Shipstate = state;
			X = j; Y = i;
		}
		public Visibility Miss
		{
			get => visibility;
			set => Set(ref visibility, value);
		}
		public Visibility Shoot
		{
			get => visibility;
			set => Set(ref visibility, value);
		}
		public Visibility Burn1
		{
			get => burn1;
			set => Set(ref burn1, value);
		}
		public Visibility Burn2
		{
			get => burn2;
			set => Set(ref burn2, value);
		}
		public double Burn1Start
		{
			get => burn1Start;
			set => Set(ref burn1Start, value);
		}
		public double Burn2Start
		{
			get => burn2Start;
			set => Set(ref burn2Start, value);
		}
		public char Shipstate
		{
			get => shipstate;
			set => shipstate = value;
		}
		public bool Ship
		{
			get => ship;
			set => ship = value;
		}
		public int X
		{
			get => x;
			set => x = value;
		}
		public int Y
		{
			get => y;
			set => y = value;
		}
		public void Visibilityy(int number)
		{
			if (number == 1)
			{
				Burn1 = Visibility.Visible;
				Burn1Start = 0.05 * random.Next(1, 8);
			}
			else if (number == 2)
			{
				Burn2 = Visibility.Visible;
				Burn2Start = 0.05 * random.Next(1, 8);
			}
		}

		public void SetState()
		{
			if (Ship)
				Shoot = Visibility.Visible;
			else
				Miss = Visibility.Visible;
		}
	}
}