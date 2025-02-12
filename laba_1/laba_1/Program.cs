using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class SnakeGame
{
    static int width = 20;
    static int height = 10;
    static List<(int, int)> snake = new List<(int, int)> { (5, 5) };
    static (int, int) direction = (1, 0);
    static (int, int) food = (10, 5);
    static Random rand = new Random();

    static void Main()
    {
        Console.CursorVisible = false;
        while (true)
        {
            Draw();
            Input();
            Logic();
            Thread.Sleep(200);
        }
    }

    static void Draw()
    {
        Console.Clear();
        // Отрисовка рамки
        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                if (x == 0 || x == width || y == 0 || y == height)
                {
                    Console.Write("#");
                }
                else if (snake.Contains((x, y)))
                {
                    Console.Write("O");
                }
                else if (food == (x, y))
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }

    static void Input()
    {
        if (!Console.KeyAvailable) return;
        var key = Console.ReadKey(true).Key;
        direction = key switch
        {
            ConsoleKey.W => (0, -1),
            ConsoleKey.S => (0, 1),
            ConsoleKey.A => (-1, 0),
            ConsoleKey.D => (1, 0),
            _ => direction
        };
    }

    static void Logic()
    {
        var newHead = (snake[0].Item1 + direction.Item1, snake[0].Item2 + direction.Item2);

        if (newHead.Item1 < 0 || newHead.Item1 > width || newHead.Item2 < 0 || newHead.Item2 > height || snake.Contains(newHead))
        {
            Console.Clear();
            Console.WriteLine("Game Over!");
            Environment.Exit(0);
        }

        snake.Insert(0, newHead);
        if (newHead == food)
        {
            food = (rand.Next(1, width - 1), rand.Next(1, height - 1));
        }
        else
        {
            snake.RemoveAt(snake.Count - 1);
        }
    }
}