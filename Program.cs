using System;

class Program
{
    static Player player = new Player(1, 5); 
    static int currentMap = 0;
    static bool gameRunning = true;

    static void Main()
    {
        Console.CursorVisible = false;

        while (gameRunning)
    {
        Console.SetCursorPosition(0, 0); // zamiast Console.Clear()
        DrawMap();
        ConsoleKeyInfo key = Console.ReadKey(true);
        Movement.MovePlayer(key.Key, player, ref currentMap, ref gameRunning);
    }

        Console.Clear();
        Console.WriteLine("Utopiłeś się w rzece. Naucz się pływać!");
        Console.ReadKey();
    }

    static void DrawMap()
    {
        var map = Maps.GetMap(currentMap);

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
}
