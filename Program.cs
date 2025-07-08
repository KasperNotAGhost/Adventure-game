using System;
using System.Threading;

class Program
{
    private static Player playerInstance = new Player(1, 5);
    public static Player GetPlayerInstance() => playerInstance;

    static void Main()
    {
        Console.CursorVisible = false;
        int mapIndex = 0;
        bool gameRunning = true;
        bool npcInConversation = false;

        Player player = playerInstance;
        char[,] map = Maps.GetMap(mapIndex);
        // Inicjalizacja NPC na lewo od rzeki tylko raz
        NPC npc = new NPC(2, 5);

        BaconSpawner.SpawnBaconRandomly(); // losowy boczek

        while (gameRunning)
        {
            map = Maps.GetMap(mapIndex);

            // Aktualizacja NPC przy zmianie mapy
            if (mapIndex != 0)
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
                    // Wyjście z rozmowy przy dowolnym wciśnięciu klawisza
                    npcInConversation = false;
                    Movement.MovePlayerToNearestFreeTile(player, map);
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
                        npc.TryMoveRandomly(map); // Dodano ruch NPC po ruchu gracza
                        break;
                }
            }

            Thread.Sleep(100);
        }

        Console.SetCursorPosition(0, 20);
    }
}
