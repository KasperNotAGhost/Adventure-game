using System;

class Program
{
    static Player player = new Player(1, 5);
    static int currentMap = 0;
    static bool gameRunning = true;
    static char[,] map = Maps.GetMap(currentMap);

    static void Main()
    {
        Console.CursorVisible = false;

        while (gameRunning)
        {
            Console.Clear();

            if (Movement.IsInventoryOpen)
            {
                DrawInventory();
                var key = Console.ReadKey(true);
                Movement.MovePlayer(key.Key, player, ref currentMap, ref gameRunning, ref map);
            }
            else
            {
                DrawMap();
                Movement.DrawWoodOnMap();

                var key = Console.ReadKey(true);
                Movement.MovePlayer(key.Key, player, ref currentMap, ref gameRunning, ref map);
            }
        }

        Console.Clear();
        Console.WriteLine("Game Over!");
        Console.ReadKey();
    }

    static void DrawMap()
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (x == player.X && y == player.Y)
                    Console.Write('@');
                else
                    Console.Write(map[y, x]);
            }
            Console.WriteLine();
        }
    }

    static void DrawInventory()
    {
        Console.WriteLine("== EKWIPUNEK ==");
        var items = player.Inventory.GetItems();

        // 3 sloty
        for (int i = 0; i < 3; i++)
        {
            if (i == 0 && items.ContainsKey("Wood") && items["Wood"] > 0)
                Console.WriteLine("[t]");  // drewno
            else
                Console.WriteLine("[ ]");  // puste miejsce
        }

        Console.WriteLine();
        Console.WriteLine("Naciśnij E, by wyrzucić drewno (jeśli masz) lub dowolny inny klawisz, by wyjść.");
    }
}
