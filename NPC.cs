using System;

public class NPC
{
    public int X, Y;

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

        var input = Console.ReadKey(true).Key;

        if (input == ConsoleKey.D1)
        {
            Console.WriteLine("NPC: Daj mi Drewno to pozwolę ci przejsć przez most. I nie zapominaj jeść boczku, by mieć energię!");
            Console.ReadKey();
        }
        else if (input == ConsoleKey.D2)
        {
            SnakeGame.Play();
        }
    }
}
