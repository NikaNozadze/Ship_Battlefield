using System;
using System.Collections.Generic;
using System.Windows;
using Koordinate = System.ValueTuple<int, int>;

namespace Battleship
{
	public class ShooterEngine
	{
		private List<Koordinate> list1 = new List<Koordinate>();
		private List<Koordinate> list2 = new List<Koordinate>();
		private List<Koordinate> list3 = new List<Koordinate>();
		private List<Koordinate> koordinatesToRemove;
		private Random random = new Random();
		private int x = 0, y = 0;
		private int xx = 0, yy = 0;
		private bool shipTrue = false;
		private int direction = 0;
		private bool thereIsNoShip = false;
		private int xx1 = 0, yy1 = 0;

		public ShooterEngine()
		{
			int n = 0;

			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (n % 2 == 0)
					{
						list1.Add((i, j));
					}
					else
					{
						list2.Add((i, j));
					}
					n++;
				}
				n++;
			}
		}

		public Koordinate XY()
		{
			if (MainWindow.page2.ReadBs().OurMap[y][x].Burn1 == Visibility.Visible)
			{
				for (int i = 0; i < list3.Count; i++)
				{
					(xx1, yy1) = list3[i];
					RemoveKoordinates(xx1, yy1);
					list3.RemoveAt(i);
					i--;
				}
				x = 0; y = 0;
				xx = 0; yy = 0;
				shipTrue = false;
				direction = 0;
				thereIsNoShip = false;
			}
			if (!shipTrue)
			{
				if (list1.Count != 0)
				{
					(x, y) = list1[random.Next(list1.Count)];
					list1.Remove((x, y));
				}
				else
				{
					(x, y) = list2[random.Next(list2.Count)];
					list2.Remove((x, y));
				}

				if (MainWindow.page2.ReadBs().OurMap[y][x].Ship)
				{
					list3.Add((x, y));
					shipTrue = true;
				}

				return (x, y);
			}
			else
			{
				if (thereIsNoShip)
				{
					x = xx; y = yy;
					(x, y) = RestOfTheShip();
					if (MainWindow.page2.ReadBs().OurMap[y][x].Ship)
					{
						list3.Add((x, y));
					}

					return (x, y);
				}
				if (MainWindow.page2.ReadBs().OurMap[y][x].Burn1 == Visibility.Collapsed)
				{
					(x, y) = RestOfTheShip();
					if (MainWindow.page2.ReadBs().OurMap[y][x].Ship)
					{
						list3.Add((x, y));
					}

					return (x, y);
				}
				else
				{
					if (list1.Count != 0)
					{
						(x, y) = list1[random.Next(list1.Count)];
						list1.Remove((x, y));
					}
					else
					{
						(x, y) = list2[random.Next(list2.Count)];
						list2.Remove((x, y));
					}

					if (MainWindow.page2.ReadBs().OurMap[y][x].Ship)
					{
						list3.Add((x, y));
						shipTrue = true;
					}
					else
					{
						shipTrue = false;
					}

					return (x, y);
				}
			}
		}

		private Koordinate RestOfTheShip()
		{
			thereIsNoShip = false;
			xx = x; yy = y;
			List<Koordinate> directionList = new List<Koordinate>() { (x - 1, y), (x, y - 1), (x + 1, y), (x, y + 1) };
			Koordinate result = new Koordinate();

			if (direction == 1)
			{
				result = DirectionResult(x - 1, y, x + 2, y, x + 1, y, x + 3, y, 3);
			}
			else if (direction == 2)
			{
				result = DirectionResult(x, y - 1, x, y + 2, x, y + 1, x, y + 3, 4);
			}
			else if (direction == 3)
			{
				result = DirectionResult(x + 1, y, x - 2, y, x - 1, y, x - 3, y, 1);
			}
			else if (direction == 4)
			{
				result = DirectionResult(x, y + 1, x, y - 2, x, y - 1, x, y - 3, 2);
			}
			else
			{
				CheckDirectionList(ref directionList);
				result = directionList[random.Next(directionList.Count)];
				(int xo, int yo) = result;
				if (MainWindow.page2.ReadBs().OurMap[yo][xo].Ship)
				{
					if (result == (x - 1, y))
					{
						direction = 1;
					}
					else if (result == (x, y - 1))
					{
						direction = 2;
					}
					else if (result == (x + 1, y))
					{
						direction = 3;
					}
					else if (result == (x, y + 1))
					{
						direction = 4;
					}
					thereIsNoShip = false;
				}
				else
				{
					thereIsNoShip = true;
				}
			}

			return result;
		}

		private Koordinate DirectionResult(int xx11, int yy11, int dir1x, int dir1y, int dir1xx, int dir1yy, int dir2x, int dir2y, int dirnumb)
		{
			Koordinate result = new Koordinate();

			if (DirectionIsPossible(xx11, yy11))
			{
				if (!MainWindow.page2.ReadBs().OurMap[yy11][xx11].Ship)
				{
					if (DirectionIsPossible(dir1x, dir1y))
					{
						xx = dir1xx; yy = dir1yy;
					}
					else if (DirectionIsPossible(dir2x, dir2y))
					{
						xx = dir1x; yy = dir1y;
					}
					direction = dirnumb;
					thereIsNoShip = true;
				}
				result = (xx11, yy11);
			}
			else if (DirectionIsPossible(dir1x, dir1y))
			{
				result = (dir1x, dir1y);
				direction = dirnumb;
			}
			else if (DirectionIsPossible(dir2x, dir2y))
			{
				result = (dir2x, dir2y);
				direction = 0;
			}

			return result;
		}

		private bool DirectionIsPossible(int xxx, int yyy)
		{
			if (-1 < xxx && xxx < 10 && -1 < yyy && yyy < 10)
			{
				if (MainWindow.page2.ReadBs().OurMap[yyy][xxx].Shoot == Visibility.Collapsed)
				{
					return true;
				}
			}

			return false;
		}

		private void CheckDirectionList(ref List<Koordinate> directionList)
		{
			directionList = new List<Koordinate>() { (x - 1, y), (x, y - 1), (x + 1, y), (x, y + 1) };
			for (int i = 0; i < directionList.Count; i++)
			{
				(int xxx, int yyy) = directionList[i];
				if (0 > xxx || xxx > 9 || 0 > yyy || yyy > 9 || MainWindow.page2.ReadBs().OurMap[yyy][xxx].Shoot == Visibility.Visible || (!(list1.Contains((xxx, yyy)) || list2.Contains((xxx, yyy)))))
				{
					directionList.Remove(directionList[i]);
					i--;
				}
			}
		}

		private void RemoveKoordinates(int xx, int yy)
		{
			koordinatesToRemove = new List<Koordinate>() { (xx - 1, yy - 1), (xx, yy - 1), (xx + 1, yy - 1), (xx - 1, yy), (xx, yy),
														   (xx + 1, yy), (xx - 1, yy + 1), (xx, yy + 1), (xx + 1, yy + 1) };
			for (int i = 0; i < koordinatesToRemove.Count; i++)
			{
				list1.Remove(koordinatesToRemove[i]);
				list2.Remove(koordinatesToRemove[i]);
			}
		}
	}
}