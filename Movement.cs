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

        // granice
        if (newY < 0 || newY >= map.GetLength(0) || newX < 0 || newX >= map.GetLength(1))
            return;

        char target = map[newY, newX];

        if (target == '#') return;
        if (target == '~') {
            gameRunning = false;
            return;
        }
        if (target == 'P') {
            mapIndex = (mapIndex + 1) % Maps.AllMaps.Count;
            player.X = 1;
            player.Y = 1;
            return;
        }

        player.X = newX;
        player.Y = newY;
    }
}
