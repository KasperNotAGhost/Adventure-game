using System;

public class NPC
{
    public int X, Y;
    private bool bridgeBuilt = false;
    private int moveCounter = 0;
    private static Random random = new Random();

    public NPC(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Interact()
    {
        Console.Clear();
        Console.WriteLine("NPC: Witaj podróżniku!");
        Console.WriteLine("1 - Porozmawiaj");
        Console.WriteLine("2 - Zagraj w Snake");
        Console.WriteLine("Inny - Wyjdź");
        Console.WriteLine("(Wciśnij E lub ESC, aby wyjść z dialogu)");

        var input = Console.ReadKey(true).Key;

        if (input == ConsoleKey.D1)
        {
            Console.WriteLine("NPC: Daj mi Drewno to pozwolę ci przejsć przez most. I nie zapominaj jeść boczku, by mieć energię!");
            // Sprawdź, czy gracz ma drewno
            var player = Program.GetPlayerInstance(); // zakładamy, że taka metoda istnieje lub przekazujemy player do Interact
            if (player != null && player.Inventory.Contains("Wood"))
            {
                Console.WriteLine("Oddałeś drewno NPC. Buduję most...");
                player.RemoveItem("Wood");
                BuildBridge();
                Console.WriteLine("Most został zbudowany! Możesz przejść przez rzekę.");
            }
            else
            {
                Console.WriteLine("Nie masz drewna!");
            }
            Console.ReadKey();
        }
        else if (input == ConsoleKey.D2)
        {
            SnakeGame.Play();
        }
    }

    public void TryMoveRandomly(char[,] map)
    {
        moveCounter++;
        if (moveCounter % 2 != 0) return; // NPC rusza się co dwa ruchy gracza

        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };
        int dir = random.Next(4);
        int newX = X + dx[dir];
        int newY = Y + dy[dir];
        if (newY < 0 || newY >= map.GetLength(0) || newX < 0 || newX >= map.GetLength(1)) return;
        char target = map[newY, newX];
        if (target == ' ')
        {
            X = newX;
            Y = newY;
        }
    }

    private void BuildBridge()
    {
        if (bridgeBuilt) return;
        var map = Maps.GetMap(0);
        // Most budujemy na środku rzeki (linia 5, kolumna 8)
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == '~')
                {
                    map[y, x] = '=';
                    bridgeBuilt = true;
                    return;
                }
            }
        }
    }
}
