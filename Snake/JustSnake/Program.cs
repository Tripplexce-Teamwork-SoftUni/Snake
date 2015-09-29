using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

struct Position
{
    public int X;
    public int Y;
    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

class Program
{
    static void Main()
    {

        Console.BufferHeight = Console.WindowHeight = 30;
        Console.BufferWidth = Console.WindowWidth = 70;

        Random randomGenerator = new Random();

        PrintData(0, 0, "----------------------------------------------------------------------");
        PrintData(29, 2, "JUST SNAKE", ConsoleColor.Red);
        PrintData(0, 4, "----------------------------------------------------------------------");

        Position[] directions = new Position[] 
        {
            new Position(1,0), //right
            new Position(-1,0), // left
            new Position(0,1), //down
            new Position(0,-1) //up
        };

        int direction = 0; // 0 right 1 left 2 down 3 up
        Queue<Position> snakeElements = new Queue<Position>();

        for (int i = 0; i <= 5; i++)
        {
            snakeElements.Enqueue(new Position(i, 5));
        }
        foreach (Position position in snakeElements)
        {
            PrintSnake(position.X, position.Y, 'o');
        }

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo command = Console.ReadKey();
                if (command.Key == ConsoleKey.RightArrow)
                {
                    if (direction != 1)
                    {
                        direction = 0;
                    }
                }
                if (command.Key == ConsoleKey.LeftArrow)
                {
                    if (direction != 0)
                    {
                        direction = 1;
                    }
                }
                if (command.Key == ConsoleKey.DownArrow)
                {
                    if (direction != 3)
                    {
                        direction = 2;
                    }
                }
                if (command.Key == ConsoleKey.UpArrow)
                {
                    if (direction != 2)
                    {
                        direction = 3;
                    }
                }
            }
            Position snakeHead = snakeElements.Last();
            Position snakeNewHead = new Position(snakeHead.X + directions[direction].X, snakeHead.Y + directions[direction].Y);

            if (snakeNewHead.X > Console.WindowWidth - 1 || snakeNewHead.X < 0 ||
                snakeNewHead.Y < 5 || snakeNewHead.Y > Console.WindowHeight - 1 || snakeElements.Contains(snakeNewHead))
            {
                PrintData(29, 15, "Game Over!", ConsoleColor.Yellow);
                Console.WriteLine();
                Console.ReadKey();
                return;
            }

            snakeElements.Enqueue(snakeNewHead);
            snakeElements.Dequeue();

            Console.Clear();

            foreach (Position position in snakeElements)
            {
                PrintSnake(position.X, position.Y, 'o');
            }
            PrintData(0, 0, "----------------------------------------------------------------------");
            PrintData(29, 2, "JUST SNAKE", ConsoleColor.Red);
            PrintData(0, 4, "----------------------------------------------------------------------");
            Thread.Sleep(150);

        }
    }
    static void PrintData(int x, int y, string str, ConsoleColor color = ConsoleColor.Green)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(str);
    }
    static void PrintSnake(int x, int y, char snakeBody)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(snakeBody);
    }
}
