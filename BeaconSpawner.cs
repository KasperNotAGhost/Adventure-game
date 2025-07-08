using System;

public static class BaconSpawner
{
    public static void SpawnBaconRandomly()
    {
        Random rand = new Random();

        foreach (var map in Maps.AllMaps)
        {
            int placed = 0;
            while (placed < 3)
            {
                int x = rand.Next(1, map.GetLength(1) - 1);
                int y = rand.Next(1, map.GetLength(0) - 1);

                if (map[y, x] == ' ')
                {
                    map[y, x] = 'B';
                    placed++;
                }
            }
        }
    }
}
