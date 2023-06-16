namespace Battleship
{
	public class BattleField : ViewModelBase
	{
		private ShooterEngine engine = new ShooterEngine();
		private ShipExplosion se1 = new ShipExplosion();
		private ShipExplosion se2 = new ShipExplosion();
		private string opponent = "0";
		private string steps = "0";
		private string you = "0";
		public Cell[][] ChooseYourMap { get; private set; }
		public Cell[][] EnemyMap { get; private set; }
		public Cell[][] OurMap { get; private set; }
		public string Opponent
		{
			get => opponent;
			set => Set(ref opponent, value);
		}
		public string Steps
		{
			get => steps;
			set => Set(ref steps, value);
		}
		public string You
		{
			get => you;
			set => Set(ref you, value);
		}
		public ShipExplosion Se1
		{
			get => se1;
			set => se1 = value;
		}
		public ShipExplosion Se2
		{
			get => se2;
			set => se2 = value;
		}
		public ShooterEngine Engine
		{
			get => engine;
			set => engine = value;
		}

		public BattleField()
		{
			string[] enemyMap = MapGenerator.GenerateMap();
			string[] YourMap = MapGenerator.GenerateMap();
			ChooseYourMap = MapCell(YourMap);
			EnemyMap = MapCell(enemyMap);
			OurMap = MapCell(YourMap);
		}

		Cell[][] MapCell(string[] mp)
		{
			var map = new Cell[10][];
			for (int i = 0; i < 10; i++)
			{
				map[i] = new Cell[10];
				for (int j = 0; j < 10; j++)
				{
					map[i][j] = new Cell(mp[i][j], i, j);
				}
			}
			return map;
		}

		public void ShootToOurMap(int x, int y)
		{
			OurMap[y][x].SetState();
		}
	}
}