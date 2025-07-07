using System;

public static class MapRenderer
{
    public static void Draw(char[,] map, Player player, NPC npc)
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (x == player.X && y == player.Y)
                    Console.Write('@');
                else if (npc != null && x == npc.X && y == npc.Y)
                    Console.Write('N');
                else
                    Console.Write(map[y, x]);
            }
            Console.WriteLine();
        }
    }
}
