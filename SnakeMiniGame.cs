using System;
using System.Collections.Generic;
using System.Threading;

public static class SnakeGame
{
    public static void Play()
    {
        const int width = 22;
        const int height = 12;
        Console.Clear();
        Console.CursorVisible = false;

        List<(int x, int y)> snake = new List<(int, int)> { (width / 2, height / 2) };
        (int x, int y) direction = (1, 0);
        Random random = new Random();
        (int x, int y) food = (random.Next(1, width - 1), random.Next(1, height - 1));

        bool alive = true;

        while (alive)
        {
            Console.SetCursorPosition(0, 0);

            // Render
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                    {
                        Console.Write('#'); // Wall
                    }
                    else if (snake[0].x == x && snake[0].y == y)
                    {
                        Console.Write('@'); // Head
                    }
                    else if (snake.Exists(p => p.x == x && p.y == y))
                    {
                        Console.Write('o'); // Body
                    }
                    else if (x == food.x && y == food.y)
                    {
                        Console.Write('F'); // Food
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }

            // Input
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                direction = key switch
                {
                    ConsoleKey.W when direction != (0, 1) => (0, -1),
                    ConsoleKey.S when direction != (0, -1) => (0, 1),
                    ConsoleKey.A when direction != (1, 0) => (-1, 0),
                    ConsoleKey.D when direction != (-1, 0) => (1, 0),
                    _ => direction
                };
            }

            // Move head
            var newHead = (x: snake[0].x + direction.x, y: snake[0].y + direction.y);

            // Collision with wall or self
            if (newHead.x <= 0 || newHead.x >= width - 1 || newHead.y <= 0 || newHead.y >= height - 1 ||
                snake.Contains(newHead))
            {
                Console.SetCursorPosition(0, height + 1);
                Console.WriteLine("Koniec gry! Naciśnij dowolny klawisz...");
                Console.ReadKey(true);
                break;
            }

            snake.Insert(0, newHead);

            if (newHead == food)
            {
                food = (random.Next(1, width - 1), random.Next(1, height - 1));
                while (snake.Contains(food))
                    food = (random.Next(1, width - 1), random.Next(1, height - 1));
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }

            Thread.Sleep(120);
        }

        Console.Clear();
    }
}
