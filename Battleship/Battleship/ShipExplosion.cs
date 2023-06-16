using System.Collections.Generic;
using koordinate = System.ValueTuple<int, int>;

namespace Battleship
{
	public class ShipExplosion
	{
		private List<koordinate> fourPointShip = new List<koordinate>();
		private List<koordinate> threePointShip = new List<koordinate>();
		private List<koordinate> twoPointShip = new List<koordinate>();

		public void Explosion(Cell cell, int number)
		{
			char shipstate = cell.Shipstate;
			int x = cell.X;
			int y = cell.Y;

			if (shipstate == '1')
			{
				cell.Visibilityy(number);
			}
			else if (shipstate == '2')
			{
				if (twoPointShip.Count > 0)
				{
					int c = twoPointShip.Count;
					for (int i = 0; i < twoPointShip.Count; i++)
					{
						if ((y - 1, x) == twoPointShip[i] || (y + 1, x) == twoPointShip[i])
						{
							(int yy, int xx) = twoPointShip[i];
							cell.Visibilityy(number);
							ExplosionOnMap(number, yy, xx);
							twoPointShip.RemoveAt(i);
							break;
						}
						else if ((y, x - 1) == twoPointShip[i] || (y, x + 1) == twoPointShip[i])
						{
							(int yy, int xx) = twoPointShip[i];
							cell.Visibilityy(number);
							ExplosionOnMap(number, yy, xx);
							twoPointShip.RemoveAt(i);
							break;
						}
					}
					if (c == twoPointShip.Count)
					{
						twoPointShip.Add((y, x));
					}
				}
				else
				{
					twoPointShip.Add((y, x));
				}
			}
			else if (shipstate == '3')
			{
				if (threePointShip.Count > 0)
				{
					int c = threePointShip.Count;
					for (int i = 0; i < threePointShip.Count; i++)
					{

						if (c == threePointShip.Count)
						{
							if ((y - 1, x) == threePointShip[i])
							{
								ExplosionShip(cell, number, (y - 1) - 1, x, y - 1, x);
							}
						}
						if (c == threePointShip.Count)
						{
							if ((y + 1, x) == threePointShip[i])
							{
								ExplosionShip(cell, number, (y + 1) + 1, x, y + 1, x);
							}
						}
						if (c == threePointShip.Count)
						{
							if ((y + 1, x) == threePointShip[i])
							{
								ExplosionShip(cell, number, y - 1, x, y + 1, x);
							}
						}
						if (c == threePointShip.Count)
						{
							if ((y, x - 1) == threePointShip[i])
							{
								ExplosionShip(cell, number, y, (x - 1) - 1, y, x - 1);
							}
						}
						if (c == threePointShip.Count)
						{
							if ((y, x + 1) == threePointShip[i])
							{
								ExplosionShip(cell, number, y, (x + 1) + 1, y, x + 1);
							}
						}
						if (c == threePointShip.Count)
						{
							if ((y, x + 1) == threePointShip[i])
							{
								ExplosionShip(cell, number, y, x - 1, y, x + 1);
							}
						}
					}
					if (c == threePointShip.Count)
					{
						threePointShip.Add((y, x));
					}
				}
				else
				{
					threePointShip.Add((y, x));
				}
			}
			else if (shipstate == '4')
			{
				fourPointShip.Add((y, x));
				if (fourPointShip.Count == 4)
				{
					(int yy, int xx) = fourPointShip[0];
					(int yyy, int xxx) = fourPointShip[1];
					(int yyyy, int xxxx) = fourPointShip[2];
					cell.Visibilityy(number);
					ExplosionOnMap(number, yy, xx);
					ExplosionOnMap(number, yyy, xxx);
					ExplosionOnMap(number, yyyy, xxxx);
				}
			}
		}

		private void ExplosionShip(Cell cell, int number, int y1, int x1, int y2, int x2)
		{
			for (int j = 0; j < threePointShip.Count; j++)
			{
				if ((y1, x1) == threePointShip[j])
				{
					cell.Visibilityy(number);
					ExplosionOnMap(number, y1, x1);
					ExplosionOnMap(number, y2, x2);
					threePointShip.Remove((y1, x1));
					threePointShip.Remove((y2, x2));
					break;
				}
			}
		}

		private void ExplosionOnMap(int number, int y, int x) //romel dafaze unda dasvas cecxli
		{
			if (number == 1)
			{
				MainWindow.page2.ReadBs().OurMap[y][x].Visibilityy(number);
			}
			else if (number == 2)
			{
				MainWindow.page2.ReadBs().EnemyMap[y][x].Visibilityy(number);
			}
		}
	}
}
