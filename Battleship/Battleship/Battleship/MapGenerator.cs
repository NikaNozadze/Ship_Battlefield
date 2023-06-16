using System;
using System.Collections.Generic;
using koordinate = System.ValueTuple<int, int>;

namespace Battleship
{
	internal static class MapGenerator
	{
		static List<koordinate> freePlace = new List<koordinate>();
		static List<koordinate> possiblePlaces = new List<koordinate>();
		static int[] ships = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
		static string[,] map = new string[10, 10];
		static Random random = new Random();
		static int x1 = 0;
		static int y1 = 0;
		static int x2 = 0;
		static int y2 = 0;
		static int xo = 0;
		static int yo = 0;

		public static string[] GenerateMap()
		{
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					map[i, j] = "*";
					freePlace.Add((j, i));
				}
			}
			GenerateAllShips(ref map);

			return ConvertArray(ref map);
		}

		private static void GenerateAllShips(ref string[,] map)
		{
			foreach (int ship in ships)
			{
				GenerateShip(ref map, ship);
			}
		}

		private static string[] ConvertArray(ref string[,] map)
		{
			var result = new string[10];

			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					result[i] += map[i, j];
				}
			}
			freePlace = new List<koordinate>();
			possiblePlaces = new List<koordinate>();
			map = new string[10, 10];
			random = new Random();
			x1 = 0;
			y1 = 0;
			x2 = 0;
			y2 = 0;
			xo = 0;
			yo = 0;

			return result;
		}

		private static void GenerateShip(ref string[,] map, int ship)
		{
			x1y1(ref map, ship);
			x2y2(ref map, ship);
			ShipCreation(ref map, ship);
		}

		private static void x1y1(ref string[,] map, int ship)
		{
			int repeat = 1;
			for (int i = 0; i < repeat; i++)
			{
				x1 = random.Next(10); y1 = random.Next(10);
				if (freePlace[int.Parse(x1.ToString() + y1.ToString())] != (-1, -1))
				{
					map[y1, x1] = $"{ship}";
				}
				else
				{
					repeat++;
				}
			}
		}

		private static void x2y2(ref string[,] map, int ship)
		{
			PossibleOptions(ship, x1, y1);
			int repeat = 1;
			for (int i = 0; i < repeat; i++)
			{
				(x2, y2) = possiblePlaces[random.Next(possiblePlaces.Count)];
				if (freePlace[int.Parse(x2.ToString() + y2.ToString())] != (-1, -1))
				{
					map[y2, x2] = $"{ship}";
					TakeThePlaceOff(ref freePlace, (x1, y1));
					TakeThePlaceOff(ref freePlace, (x2, y2));
				}
				else
				{
					repeat++;
				}
			}
		}

		private static void ShipCreation(ref string[,] map, int ship)
		{
			if (x1 == x2)
			{
				if (y1 < y2)
				{
					for (int i = 0; i < ship - 2; i++)
					{
						map[y1 + (i + 1), x1] = $"{ship}";
						TakeThePlaceOff(ref freePlace, (x1, y1 + (i + 1)));
					}
				}
				else
				{
					for (int i = 0; i < ship - 2; i++)
					{
						map[y1 - (i + 1), x1] = $"{ship}";
						TakeThePlaceOff(ref freePlace, (x1, y1 - (i + 1)));
					}
				}
			}
			else
			{
				if (x1 < x2)
				{
					for (int i = 0; i < ship - 2; i++)
					{
						map[y1, x1 + (i + 1)] = $"{ship}";
						TakeThePlaceOff(ref freePlace, (x1 + (i + 1), y1));
					}
				}
				else
				{
					for (int i = 0; i < ship - 2; i++)
					{
						map[y1, x1 - (i + 1)] = $"{ship}";
						TakeThePlaceOff(ref freePlace, (x1 - (i + 1), y1));
					}
				}
			}
		}

		private static void PossibleOptions(int ship, int x1, int y1)
		{
			possiblePlaces.Clear();

			if (x1 - (ship - 1) >= 0 && x1 - (ship - 1) <= 9)
			{
				possiblePlaces.Add((x1 - (ship - 1), y1));
			}

			if (x1 + (ship - 1) >= 0 && x1 + (ship - 1) <= 9)
			{
				possiblePlaces.Add((x1 + (ship - 1), y1));
			}

			if (y1 - (ship - 1) >= 0 && y1 - (ship - 1) <= 9)
			{
				possiblePlaces.Add((x1, y1 - (ship - 1)));
			}

			if (y1 + (ship - 1) >= 0 && y1 + (ship - 1) <= 9)
			{
				possiblePlaces.Add((x1, y1 + (ship - 1)));
			}
		}

		private static void TakeThePlaceOff(ref List<koordinate> variantebisList, koordinate koordinate) //sadac jdeba X, is adgili dakavebuli xdeba
		{
			(int x, int y) = koordinate;
			for (int i = -1; i < 2; i++)
			{
				yo = y + i;
				for (int j = -1; j < 2; j++)
				{
					xo = x + j;
					if ((yo = y + i) >= 0 && (xo = x + j) >= 0 && (yo = y + i) < 10 && (xo = x + j) < 10)
					{
						variantebisList[int.Parse(xo.ToString() + yo.ToString())] = (-1, -1);
					}
				}
			}
		}
	}
}