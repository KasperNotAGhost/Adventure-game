using System;

public static class Movement
{
    private static System.Collections.Generic.List<(int x, int y)> woodOnMap = new System.Collections.Generic.List<(int x, int y)>();

    // Stan wyświetlania ekwipunku
    public static bool IsInventoryOpen = false;

    public static void MovePlayer(ConsoleKey key, Player player, ref int mapIndex, ref bool gameRunning, ref char[,] map)
    {
        if (IsInventoryOpen)
        {
            // W ekwipunku naciskanie dowolnego klawisza zamyka ekwipunek
            if (key == ConsoleKey.E)
            {
                // Jeśli masz drewno, wyrzuć je
                if (player.Inventory.Remove("Wood"))
                {
                    TryDropWood(player, map);
                }
                IsInventoryOpen = false;
            }
            else
            {
                IsInventoryOpen = false;
            }
            return;
        }

        int newX = player.X;
        int newY = player.Y;

        switch (key)
        {
            case ConsoleKey.W: newY--; break;
            case ConsoleKey.S: newY++; break;
            case ConsoleKey.A: newX--; break;
            case ConsoleKey.D: newX++; break;
            case ConsoleKey.R:
                TryChopTree(player, map);
                return;
            case ConsoleKey.E:
                // Otwórz ekwipunek
                IsInventoryOpen = true;
                return;
            default:
                return;
        }

        // granice
        if (newY < 0 || newY >= map.GetLength(0) || newX < 0 || newX >= map.GetLength(1))
            return;

        char target = map[newY, newX];

        // Kolizje
        if (target == '#' || target == 'T' || target == '~') return;

        // Przejście mapy
        if (target == 'P')
        {
            mapIndex = (mapIndex + 1) % Maps.AllMaps.Count;
            player.X = 1;
            player.Y = 1;
            map = Maps.GetMap(mapIndex);
            woodOnMap.Clear();
            return;
        }

        // Podnoszenie drewna (małe t)
        if (target == 't')
        {
            map[newY, newX] = ' ';          // usuń drewno z mapy
            player.Inventory.Add("Wood");   // dodaj drewno do ekwipunku
            // nie ruszamy pozycji gracza, bo stoi na tym miejscu
            return;
        }

        player.X = newX;
        player.Y = newY;
    }

    private static void TryChopTree(Player player, char[,] map)
    {
        if (!player.HasAxe) return;

        var adjacentPositions = new (int x, int y)[]
        {
            (player.X + 1, player.Y),
            (player.X - 1, player.Y),
            (player.X, player.Y + 1),
            (player.X, player.Y - 1),
        };

        foreach (var pos in adjacentPositions)
        {
            if (pos.x < 0 || pos.x >= map.GetLength(1) || pos.y < 0 || pos.y >= map.GetLength(0))
                continue;

            if (map[pos.y, pos.x] == 'T')
            {
                map[pos.y, pos.x] = 't'; // ścięcie drzewa na drewno
                return;
            }
        }
    }

    private static void TryDropWood(Player player, char[,] map)
    {
        var positionsToTry = new (int x, int y)[]
        {
            (player.X + 1, player.Y),
            (player.X - 1, player.Y),
            (player.X, player.Y + 1),
            (player.X, player.Y - 1),
        };

        foreach (var pos in positionsToTry)
        {
            if (pos.x < 0 || pos.x >= map.GetLength(1) || pos.y < 0 || pos.y >= map.GetLength(0))
                continue;

            char target = map[pos.y, pos.x];
            if (target == ' ' && !IsWoodAtPosition(pos.x, pos.y))
            {
                if (target == '#' || target == '~' || target == '=')
                    continue;

                woodOnMap.Add((pos.x, pos.y));
                return;
            }
        }

        // Brak miejsca - drewno po prostu zniknie 
    }

    public static bool IsWoodAtPosition(int x, int y)
    {
        foreach (var w in woodOnMap)
            if (w.x == x && w.y == y) return true;
        return false;
    }

    public static void DrawWoodOnMap()
    {
        foreach (var w in woodOnMap)
        {
            Console.SetCursorPosition(w.x, w.y);
            Console.Write('t');
        }
    }
}
