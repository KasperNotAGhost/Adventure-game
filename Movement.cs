using System;

public static class Movement
{
    public static void MovePlayer(ConsoleKey key, Player player, ref int mapIndex, ref bool gameRunning)
    {
        int newX = player.X;
        int newY = player.Y;

        switch (key)
        {
            case ConsoleKey.W: newY--; break;
            case ConsoleKey.S: newY++; break;
            case ConsoleKey.A: newX--; break;
            case ConsoleKey.D: newX++; break;
            default: return;
        }

        var map = Maps.GetMap(mapIndex);

        if (newY < 0 || newY >= map.GetLength(0) || newX < 0 || newX >= map.GetLength(1))
            return;

        char target = map[newY, newX];

        if (target == '#') return;

        if (target == '~')
        {
            Console.Clear();
            Console.WriteLine("Nie umiesz pływać i utopiłeś się!\nKoniec gry.");
            gameRunning = false;
            Console.ReadKey();
            return;
        }

        if (target == 'T') return;

        if (target == 'P')
        {
            mapIndex = (mapIndex + 1) % Maps.AllMaps.Count;
            player.X = 1;
            player.Y = 5;
            return;
        }

        if (target == 't' || target == 'B')
        {
            player.AddItem(target == 't' ? "Wood" : "Bacon");
            map[newY, newX] = ' ';
        }

        if (player.Stamina > 0)
        {
            player.Stamina -= 1;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Zemdlałeś ze zmęczenia.\nKoniec gry.");
            gameRunning = false;
            Console.ReadKey();
            return;
        }

        player.X = newX;
        player.Y = newY;
    }

    public static void TryChopTree(Player player, char[,] map)
    {
        (int x, int y)[] directions = new (int, int)[]
        {
            (player.X, player.Y - 1),
            (player.X, player.Y + 1),
            (player.X - 1, player.Y),
            (player.X + 1, player.Y)
        };

        foreach (var (x, y) in directions)
        {
            if (x >= 0 && x < map.GetLength(1) && y >= 0 && y < map.GetLength(0))
            {
                if (map[y, x] == 'T')
                {
                    map[y, x] = 't';
                    break;
                }
            }
        }
    }

    public static void TryDropItem(Player player, char[,] map)
    {
        if (player.Inventory.Count == 0) return;

        (int x, int y)[] dropPositions = new (int, int)[]
        {
            (player.X + 1, player.Y),
            (player.X - 1, player.Y),
            (player.X, player.Y - 1),
            (player.X, player.Y + 1)
        };

        foreach (var (x, y) in dropPositions)
        {
            if (x >= 0 && x < map.GetLength(1) && y >= 0 && y < map.GetLength(0))
            {
                if (map[y, x] == ' ')
                {
                    // sprawdź, czy nie ma już obiektu w tym miejscu
                    bool alreadyOccupied = map[y, x] == 't' || map[y, x] == 'B';
                    if (!alreadyOccupied)
                    {
                        string item = player.Inventory[0];
                        player.RemoveItem(item);
                        map[y, x] = item == "Wood" ? 't' : item == "Bacon" ? 'B' : ' ';
                        break;
                    }
                }
            }
        }
    }

    public static void TryEatBacon(Player player)
    {
        if (player.Inventory.Contains("Bacon"))
        {
            player.Stamina += 30;
            if (player.Stamina > player.MaxStamina)
                player.Stamina = player.MaxStamina;

            player.RemoveItem("Bacon");
        }
    }

    public static void MovePlayerToNearestFreeTile(Player player, char[,] map)
    {
        (int x, int y)[] directions = new (int, int)[]
        {
            (player.X + 1, player.Y),
            (player.X - 1, player.Y),
            (player.X, player.Y - 1),
            (player.X, player.Y + 1)
        };

        foreach (var (x, y) in directions)
        {
            if (x >= 0 && x < map.GetLength(1) && y >= 0 && y < map.GetLength(0))
            {
                if (map[y, x] == ' ')  // wolne pole
                {
                    player.X = x;
                    player.Y = y;
                    break;
                }
            }
        }
    }
}
