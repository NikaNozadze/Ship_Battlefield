using System;
using System.Windows;

namespace Battleship
{
	public class Cell : ViewModelBase //es aris konkretuli ujra da inaxavs informacias aris tu ara masshi gemi da tu aris giaa tu dafaruli
	{
		private Visibility visibility = Visibility.Collapsed; //anu ujra dafarulia
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
			Ship = state != '*'; //tu state == 'X' mashin ship = true tuarada false
			Shipstate = state;
			X = j; Y = i;
		}
		public Visibility Miss //tu am klasshi shemosuli ujra gemiani ar aris mashi miss-idan visible gadaketdeba visibleti da amavdroulad ekranze daixateba wertili
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
		public void Visibilityy(int number) //romel dafaze moxda gemis afetqeba
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

		public void SetState() //tu gemi arsebobs mashin Shoot-shi chaiwereba Visible tu arada miss-shi
		{
			if (Ship)
				Shoot = Visibility.Visible; //es amowmebs shoot-shi chawerili Visibility aris tu ara Visible. tu ar aris mashin cvlilebebi moxdeba
			else
				Miss = Visibility.Visible;
		}
	}
}