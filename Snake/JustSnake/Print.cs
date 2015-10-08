namespace JustSnake
{
    using System;
    using System.Collections.Generic;

    internal class Print
    {
        private static string[] controlsList = { "Move Up", "Move Down", "Move left", "Move Right", "Pause" };

        private static string[] controlsKeys = { "Up Arrow", "Down Arrow", "Left Arrow", "Right Arrow", "Spacebar" };

        internal static void PrintData(int x, int y, string str, ConsoleColor color = ConsoleColor.Green)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }

        internal static void Leaderboard(List<string> leaderboardNames, List<int> leaderboardPoints, int windowWidth, int lowerMenuBorder, int upperMenuBorder)
        {
            Console.Clear();

            PrintData(0, lowerMenuBorder, new string('-', windowWidth), ConsoleColor.Magenta);
            PrintData(0, 3, string.Format("{0}{1}", new string(' ', windowWidth / 2 - 6), "Leaderboard"), ConsoleColor.White);
            PrintData(0, upperMenuBorder, new string('-', windowWidth), ConsoleColor.DarkMagenta);

            int i = 0;

            for (; i < leaderboardNames.Count; i++)
            {
                PrintData(0, i + 6, string.Format("[{0}] {1} {2}", i + 1, leaderboardNames[i],
                    leaderboardPoints[i]), ConsoleColor.Yellow);
            }

            PrintData(0, i + 8, "Press any key to return to Menu", ConsoleColor.White);
            Console.ReadKey();

            MainGameCode.Menu();
        }

        internal static void PrintSnake(int x, int y, char snakeBody)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(snakeBody);
        }

        internal static void StartSnakeElements(Queue<Position> snakeElements)
        {
            snakeElements.Clear();

            for (int i = 0; i <= 5; i++)
            {
                snakeElements.Enqueue(new Position(i, 5));
            }

            foreach (Position position in snakeElements)
            {
                PrintSnake(position.X, position.Y, 'o');
            }
        }

        internal static void SnakePlayingMenu(int windowWidth, string[] difficultyOptions, int playerPoints, int level)
        {
            Console.Clear();

            PrintData(0, 0, new string('-', windowWidth));
            PrintData(0, 2, string.Format("{0}{1}", new string(' ', windowWidth / 2 - 5),
                string.Format("{0}", "JUST SNAKE")), ConsoleColor.Red);
            PrintData(4, 2, string.Format("Level: {0}", difficultyOptions[level - 1]), ConsoleColor.Yellow);
            PrintData(45, 2, "Score: " + playerPoints, ConsoleColor.Green);
            PrintData(0, 4, new string('-', windowWidth));
        }

        internal static void PrintError()
        {
            PrintData(0, 0, "Write a name without empty space!");
        }

        internal static void Controls(int windowWidth, int lowerMenuBorder, int upperMenuBorder)
        {
            Console.Clear();

            PrintData(0, lowerMenuBorder, new string('-', windowWidth), ConsoleColor.Magenta);
            PrintData(0, 3, string.Format("{0}{1}", new string(' ', windowWidth / 2 - 6), "Controls"), ConsoleColor.White);
            PrintData(0, upperMenuBorder, new string('-', windowWidth), ConsoleColor.DarkMagenta);

            int i = 0;

            for (; i < controlsList.Length; i++)
            {
                PrintData(0, i + 6, string.Format("\U00002022 {0} [{1}]", controlsList[i].PadRight(18),
                    controlsKeys[i]), ConsoleColor.Yellow);
            }

            PrintData(0, i + 8, "Press any key to return to Menu", ConsoleColor.White);
            Console.ReadKey();

            MainGameCode.Menu();
        }

        internal static void PrintOptionsMenu(string[] options, int currentSelection)
        {
            int printPosition = 10;

            PrintData(15, printPosition + currentSelection, "> ", ConsoleColor.White);

            foreach (var i in options)
            {
                PrintData(17, printPosition, i, ConsoleColor.White);
                printPosition++;
            }
        }
        
        internal static void PrintLives(int x, int y, string lives, List<string> liveNumber, ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(lives);
            Console.WriteLine(string.Join(" ", liveNumber));
        }

        internal static void PrintObstacles(int level, List<Position> obstacle, ConsoleColor color = ConsoleColor.Green)
        {
            obstacle.Clear();
            Console.ForegroundColor = color;

            if (level >= 2)
            {
                for (int i = 12; i < 19; i++)
                {
                    obstacle.Add(new Position(9, i));
                }

                for (int i = 12; i < 19; i++)
                {
                    Console.SetCursorPosition(9, i);
                    Console.Write("x");
                }

                for (int i = 12; i < 19; i++)
                {
                    obstacle.Add(new Position(51, i));
                }

                for (int i = 12; i < 19; i++)
                {
                    Console.SetCursorPosition(51, i);
                    Console.Write("x");
                }
            }
            if (level >= 3)
            {
                for (int i = 21; i < 39; i++)
                {
                    obstacle.Add(new Position(i, 8));
                }

                Console.SetCursorPosition(21, 8);
                Console.Write("xxxxxxxxxxxxxxxxxx");

                for (int i = 21; i < 39; i++)
                {
                    obstacle.Add(new Position(i, 22));
                }

                Console.SetCursorPosition(21, 22);
                Console.Write("xxxxxxxxxxxxxxxxxx");
            }
            if (level == 4)
            {
                for (int i = 21; i < 39; i++)
                {
                    obstacle.Add(new Position(i, 15));
                }

                Console.SetCursorPosition(22, 15);
                Console.Write("xxxxxxxxxxxxxxxxx");

                for (int i = 12; i < 19; i++)
                {
                    obstacle.Add(new Position(30, i));
                }

                for (int i = 12; i < 19; i++)
                {
                    Console.SetCursorPosition(30, i);
                    Console.Write("x");
                }
            }
        }

        internal static void PrintFood(int x, int y, char symbol, ConsoleColor foodColor = ConsoleColor.Yellow)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = foodColor;
            Console.Write(symbol);
        }
    }
}
