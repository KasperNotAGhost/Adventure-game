using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        int mapIndex = 0;
        bool gameRunning = true;
        bool npcInConversation = false;

        Player player = new Player(1, 5);
        char[,] map = Maps.GetMap(mapIndex);
        
        // Inicjalizacja NPC jako "pustego" (poza mapą)
        NPC npc = new NPC(-1, -1);

        BaconSpawner.SpawnBaconRandomly(); // losowy boczek

        while (gameRunning)
        {
            map = Maps.GetMap(mapIndex);

            // Aktualizacja NPC przy zmianie mapy
            if (mapIndex == 0)
            {
                npc.X = 2;
                npc.Y = 5;
            }
            else
            {
                // Ukryj NPC poza mapą, by nie był widoczny i nie generował błędów
                npc.X = -1;
                npc.Y = -1;
            }

            Console.Clear();
            MapRenderer.Draw(map, player, npc);
            UI.ShowInventory(player);
            UI.ShowStamina(player);

            if (player.Stamina <= 0)
            {
                Console.Clear();
                Console.WriteLine("Upadasz ze zmęczenia. Gra zakończona.");
                break;
            }

            if (player.X == npc.X && player.Y == npc.Y)
            {
                if (!npcInConversation)
                {
                    npc.Interact();
                    npcInConversation = true;
                }
            }
            else
            {
                npcInConversation = false;
            }

            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                if (npcInConversation)
                {
                    // Wyjście z rozmowy przy dowolnym wciśnięciu klawisza (np. E lub Esc)
                    if (key == ConsoleKey.E || key == ConsoleKey.Escape)
                    {
                        npcInConversation = false;
                        Movement.MovePlayerToNearestFreeTile(player, map);
                    }
                    // W trakcie rozmowy ignorujemy inne klawisze
                    continue;
                }

                switch (key)
                {
                    case ConsoleKey.R:
                        Movement.TryChopTree(player, map);
                        break;
                    case ConsoleKey.E:
                        UI.ToggleInventory();
                        break;
                    case ConsoleKey.Q:
                        Movement.TryDropItem(player, map);
                        break;
                    case ConsoleKey.U:
                        Movement.TryEatBacon(player);
                        break;
                    default:
                        Movement.MovePlayer(key, player, ref mapIndex, ref gameRunning);
                        break;
                }
            }

            Thread.Sleep(100);
        }

        Console.SetCursorPosition(0, 20);
    }
}
