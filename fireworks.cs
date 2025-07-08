using System;
using System.Threading;

public static class Fireworks
{
    private static Random random = new Random();

    public static void Show()
    {
        Console.Clear();
        Console.CursorVisible = false;

        for (int i = 0; i < 5; i++)
        {
            Explode();
            Thread.Sleep(500);
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("🎉 Gratulacje! Zebrałeś kamień i ukończyłeś grę! 🎉");
        Console.ResetColor();
        Console.CursorVisible = true;
    }

    private static void Explode()
    {
        int centerX = random.Next(10, Console.WindowWidth - 10);
        int centerY = random.Next(5, Console.WindowHeight - 5);
        ConsoleColor color = (ConsoleColor)random.Next(1, 16);
        char[] explosionChars = { '*', '+', '.', 'o', '@' };

        for (int frame = 0; frame < 3; frame++)
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < 20; i++)
            {
                int offsetX = random.Next(-frame - 1, frame + 2);
                int offsetY = random.Next(-frame - 1, frame + 2);
                int x = centerX + offsetX;
                int y = centerY + offsetY;

                if (x >= 0 && x < Console.WindowWidth && y >= 0 && y < Console.WindowHeight)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(explosionChars[random.Next(explosionChars.Length)]);
                }
            }

            Thread.Sleep(100);
        }

        Console.ResetColor();
    }
}
