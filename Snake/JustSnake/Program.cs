using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

class MainGameCode
{
    static int level = 1;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.BufferHeight = Console.WindowHeight = 25;
        Console.BufferWidth = Console.WindowWidth = 60;
        
        Random randomGenerator = new Random();

        Menu();

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
                PrintSnake(position.X, position.Y, '\U000025A1');
            }
            PrintData(0, 0, new string('-', 59));
            PrintData(4, 2, level.ToString(), ConsoleColor.Yellow);
            PrintData(25, 2, "JUST SNAKE", ConsoleColor.Red);
            PrintData(0, 4, new string('-', 59));
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
    static void Menu()
    {
        int currentSelection = 0;
        while (true)
        {
            PrintData(0, 0, new string('-', 59), ConsoleColor.Magenta);
            PrintData(0, 24, new string('-', 59), ConsoleColor.DarkMagenta);            
            int printPosition = 10;
            string[] options = { "New Game", "HighScores", "Choose Difficulty", "Quit" };
            PrintData(15, printPosition + currentSelection, "> ", ConsoleColor.White);

            foreach (var i in options)
            {
                PrintData(17, printPosition, i, ConsoleColor.White);
                printPosition++;
            }
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                PrintData(15, 10 + currentSelection, " ", ConsoleColor.White);
                currentSelection++;
                if (currentSelection > options.Length - 1)
                {
                    currentSelection = 0;
                }
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow)
            {
                PrintData(15, 10 + currentSelection, " ", ConsoleColor.White);
                currentSelection--;
                if (currentSelection < 0)
                {
                    currentSelection = 3;
                }
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                if (currentSelection == 0)
                {
                    return;
                }
                else if (currentSelection == 1)
                {
                    //DisplayHighscores();
                }
                else if (currentSelection == 2)
                {
                    Console.Clear();
                    LevelsMenu();
                }
                else if (currentSelection == 3)
                {
                    Environment.Exit(0);
                }
            }
        }
    }

    static void LevelsMenu()
    {
        int currentSelection = 0;
        level = 1;
        while (true)
        {
            PrintData(0, 0, new string('-', 59), ConsoleColor.Magenta);
            PrintData(0, 24, new string('-', 59), ConsoleColor.DarkMagenta);
            int printPosition = 10;
            string[] options = { "Level 1", "Level 2", "Level 3", "Level 4", "Return" };
            PrintData(15, printPosition + currentSelection, "> ", ConsoleColor.White);

            foreach (var i in options)
            {
                PrintData(17, printPosition, i, ConsoleColor.White);
                printPosition++;
            }
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                PrintData(15, 10 + currentSelection, " ", ConsoleColor.White);
                currentSelection++;
                if (currentSelection > options.Length - 1)
                {
                    currentSelection = 0;
                }
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow)
            {
                PrintData(15, 10 + currentSelection, " ", ConsoleColor.White);
                currentSelection--;
                if (currentSelection < 0)
                {
                    currentSelection = 4;
                }
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                if (currentSelection == 0)
                {
                    Console.Clear();
                    return;
                }
                else if (currentSelection == 1)
                {
                    level++;
                    Console.Clear();
                    return;
                }
                else if (currentSelection == 2)
                {
                    level+=2;
                    Console.Clear();
                    return;
                }
                else if (currentSelection == 3)
                {
                    level+=3;
                    Console.Clear();
                    return;
                }
                else if (currentSelection == 4)
                {
                    Console.Clear();
                    return;
                }
            }
        }
    }
}

