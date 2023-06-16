using System;
using System.Collections.Generic;
using koordinate = System.ValueTuple<int, int>;

namespace Battleship
{
	internal static class MapGenerator
	{
		static List<koordinate> freePlace = new List<koordinate>(); //gemis dagdebis shesadzlo variantebi(tu -1-1 ia gemis dagdeba rom ar shedzlo)
		static List<koordinate> possiblePlaces = new List<koordinate>(); //gemis wertilis dagdebis shesadzlo variantebis sia
		static int[] ships = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 }; //gemebi zomebit
		static string[,] map = new string[10, 10]; //ruka sadac ganvatavsebt gemebs
		static Random random = new Random();
		static int x1 = 0; //gemis erti bolo
		static int y1 = 0;
		static int x2 = 0; //gemis meore bolo
		static int y2 = 0;
		static int xo = 0;
		static int yo = 0;

		public static string[] GenerateMap() //qmnis rukas tavisi gemebit
		{
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					map[i, j] = "*"; //masivs avsebs *-ebit
					freePlace.Add((j, i)); //avsebs masivs gemis dagdebis shsadzlo adgilebis nomrebit
				}
			}
			GenerateAllShips(ref map); //agenerirebs yvela gems

			return ConvertArray(ref map);
		}

		private static void GenerateAllShips(ref string[,] map) //agenerirebs yvela gems
		{
			foreach (int ship in ships)
			{
				GenerateShip(ref map, ship);
			}
		}

		private static string[] ConvertArray(ref string[,] map) //akonvertirebs organzomilebian masivs ertganzomilebianshi
		{
			var result = new string[10];

			for (int i = 0; i < 10; i++) //gadahyavs ertganzomilebian masivshi
			{
				for (int j = 0; j < 10; j++)
				{
					result[i] += map[i, j];
				}
			}
			freePlace = new List<koordinate>(); //gemis dagdebis shesadzlo variantebi(tu -1-1 ia gemis dagdeba rom ar shedzlo)
			possiblePlaces = new List<koordinate>(); //gemis wertilis dagdebis shesadzlo variantebis sia
			map = new string[10, 10]; //ruka sadac ganvatavsebt gemebs
			random = new Random();
			x1 = 0; //gemis erti bolo
			y1 = 0;
			x2 = 0; //gemis meore bolo
			y2 = 0;
			xo = 0;
			yo = 0;

			return result;
		}

		private static void GenerateShip(ref string[,] map, int ship)
		{
			x1y1(ref map, ship); //rendomad agdebs wertils romelic gemis pirveli boloa
			x2y2(ref map, ship); //rendomad agdebs wertils romelic gemis meore boloa
			ShipCreation(ref map, ship); //or wertils shoris adgilis X-ebit shevseba
		}

		private static void x1y1(ref string[,] map, int ship) //rendomad agdebs wertils romelic gemis pirveli boloa
		{
			//tu x da y shesadzlo variantebis bazashi shenaxulia -1 -1 it eseigi akrdzaluli wertilia da axva adgils mozdebnis
			int repeat = 1;
			for (int i = 0; i < repeat; i++)
			{
				x1 = random.Next(10); y1 = random.Next(10);
				if (freePlace[int.Parse(x1.ToString() + y1.ToString())] != (-1, -1)) //gemis dagdebis shesadzlo variantebi(tu -1-1 ia gemis dagdeba rom ar shedzlo)
				{
					map[y1, x1] = $"{ship}";
				}
				else
				{
					repeat++;
				}
			}
		}

		private static void x2y2(ref string[,] map, int ship) //rendomad agdebs wertils romelic gemis meore boloa
		{
			//tu x da y shesadzlo variantebis bazashi shenaxulia -1 -1 it eseigi akrdzaluli wertilia da axva adgils mozdebnis
			PossibleOptions(ship, x1, y1); //es amowmebs romeli koordinatebi varga gemis wertilis dasagdebad
			int repeat = 1;
			for (int i = 0; i < repeat; i++)
			{
				(x2, y2) = possiblePlaces[random.Next(possiblePlaces.Count)]; //agenerirebs rendomad horizontalurad iyos gemi tu vertikalurad
				if (freePlace[int.Parse(x2.ToString() + y2.ToString())] != (-1, -1)) //gemis dagdebis shesadzlo variantebi(tu -1-1 ia gemis dagdeba rom ar shedzlo)
				{
					map[y2, x2] = $"{ship}";
					TakeThePlaceOff(ref freePlace, (x1, y1)); //sadac jdeba X, is adgili dakavebuli xdeba
					TakeThePlaceOff(ref freePlace, (x2, y2)); //gemis tavic da boloc dakavebuli gaxda
				}
				else
				{
					repeat++;
				}
			}
		}

		private static void ShipCreation(ref string[,] map, int ship) //or wertils shoris adgilis X-ebit shevseba
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

		private static void PossibleOptions(int ship, int x1, int y1) //es amowmebs romeli koordinatebi varga gemis wertilis dasagdebad
		{
			possiblePlaces.Clear(); //suftavdeba sia rom axlidan daiwyos sachiro infos chawera

			if (x1 - (ship - 1) >= 0 && x1 - (ship - 1) <= 9) // aq aman chaagdo x gerdzze shesadzlo varianti
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
