using System;
using System.Collections.Generic;
using System.Windows;
using Koordinate = System.ValueTuple<int, int>;

namespace Battleship
{
	public class ShooterEngine
	{
		private List<Koordinate> list1 = new List<Koordinate>(); //es inaxavs ujrebs shaxmaturad
		private List<Koordinate> list2 = new List<Koordinate>(); //es inaxavs im ujrebs rasac ar sheinaxavs list1
		private List<Koordinate> list3 = new List<Koordinate>(); //es inaxavs im ujrebs sadac afetqebuli gemebia, rom shemdegshi waishalos list1dan da 2dan
		private List<Koordinate> koordinatesToRemove;
		private Random random = new Random();
		private int x = 0, y = 0; //pirvelad ro shemodixar am koordinatebs gadzlevs da meored ukve es xdeba wina dartymis wertili
		private int xx = 0, yy = 0; //axali koordinati
		private bool shipTrue = false; //wina dartyma gemze iyo tu ara
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
			if (!shipTrue) //tu wina dartyma gemze ar iyo
			{
				if (list1.Count != 0)
				{
					(x, y) = list1[random.Next(list1.Count)]; //generirdeba axali rendom x y
					list1.Remove((x, y));
				}
				else
				{
					(x, y) = list2[random.Next(list2.Count)];
					list2.Remove((x, y));
				}

				if (MainWindow.page2.ReadBs().OurMap[y][x].Ship) //tu es axali x y gemia mashin shemdeg shemosvlaze sheva else-shi
				{
					list3.Add((x, y));
					shipTrue = true;
				}

				return (x, y);
			}
			else //tu wina shemosvlaze gemze moartyi mushaobs es
			{
				if (thereIsNoShip) //tu iqsebis mere wertili dasva da unda sawinaagmdego mimartulebit wavides
				{
					x = xx; y = yy;
					(x, y) = RestOfTheShip(); //edzebs dartymis garshemo wertils
					if (MainWindow.page2.ReadBs().OurMap[y][x].Ship)
					{
						list3.Add((x, y));
					}

					return (x, y);
				}
				if (MainWindow.page2.ReadBs().OurMap[y][x].Burn1 == Visibility.Collapsed) //tu wina dartymis shedegad gemi ar daiwva
				{
					(x, y) = RestOfTheShip(); //edzebs dartymis garshemo wertils
					if (MainWindow.page2.ReadBs().OurMap[y][x].Ship)
					{
						list3.Add((x, y));
					}

					return (x, y);
				}
				else //tu gemi daiwva idzebneba axali rendom wertili
				{
					if (list1.Count != 0)
					{
						(x, y) = list1[random.Next(list1.Count)]; //generirdeba axali rendom x y
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

		private Koordinate RestOfTheShip() //edzebs dartymis garshemo wertils
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
				CheckDirectionList(ref directionList); //es amowmebs romel mxares sheidzleba srola
				result = directionList[random.Next(directionList.Count)];
				(int xo, int yo) = result;
				if (MainWindow.page2.ReadBs().OurMap[yo][xo].Ship) //tu archeul wertilshi gemia imaxsovrebs romel mxares unda isrolos shemdegshi
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

			if (DirectionIsPossible(xx11, yy11)) //romel mxares gvinda gavxsnat ujra
			{
				if (!MainWindow.page2.ReadBs().OurMap[yy11][xx11].Ship) //tu gemi ar aris
				{
					if (DirectionIsPossible(dir1x, dir1y)) //vanxulobt sapirispiro mimartulebas da aris tu ara es ujra gia
					{
						xx = dir1xx; yy = dir1yy;
					}
					else if (DirectionIsPossible(dir2x, dir2y)) //tu ar aris amis shemdeg ujras vxsnit
					{
						xx = dir1x; yy = dir1y;
					}
					direction = dirnumb; //vcvlit mimartulebas sapirispiroze
					thereIsNoShip = true; //avamushavebt shemdeg shemosvlaze am seqtors rom x da y sheicvalos dzvel wertilshi dasabruneblad
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
			if (-1 < xxx && xxx < 10 && -1 < yyy && yyy < 10) //tu aris dafis farglebshi da tu aris eg wertili gia
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
				if (0 > xxx || xxx > 9 || 0 > yyy || yyy > 9 || MainWindow.page2.ReadBs().OurMap[yyy][xxx].Shoot == Visibility.Visible || (!(list1.Contains((xxx, yyy)) || list2.Contains((xxx, yyy))))) //tu aris dafis farglebshi da tu aris eg wertili gia
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