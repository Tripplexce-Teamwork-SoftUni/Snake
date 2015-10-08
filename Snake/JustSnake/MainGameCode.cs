namespace JustSnake
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public struct Position
    {
        public int X;

        public int Y;

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public class MainGameCode
    {
        public static int windowHeight = 30;

        public static int windowWidth = 60;

        public static int level = 1;

        public static int direction;   // 0 right 1 left 2 down 3 up

        public static string menuIcon = @"    ___         ___     ______           ___        
      | |         | |    / ____|           | |       
      | |_   _ ___| |_  | (___  _ __   __ _| | _____ 
  _   | | | | / __| __|  \___ \| '_ \ / _` | |/ / _ \
 | |__| | |_| \__ \ |_   ____) | | | | (_| |   <  __/
  \____/ \__,_|___/\__| |_____/|_| |_|\__,_|_|\_\___|
                                                     
                                                     ";

        public static Random randomGenerator = new Random();

        public static Queue<Position> snakeElements = new Queue<Position>();

        public static List<Position> obstacle = new List<Position>();

        public static List<string> leaderboardNames = new List<string>();   // Leaderboard Name List

        public static List<int> leaderboardPoints = new List<int>();   // Leaderboard Points List

        public static int playerPoints = 0;    // Player points 

        public static int oldPlayerPoints = 0;

        public static int sleep = 150;

        public static string filePath = "../../file.md";

        public static string[] options = { "New Game", "Controls", "HighScores", "Choose Difficulty", "Quit" };

        public static string[] difficultyOptions = { "Recruit", "Regular", "Hardened", "Veteran", "Return to Menu" };

        public static int upperMenuBorder = 1;

        public static int lowerMenuBorder = 28;

        public static List<string> liveNumber = new List<string>()
        {
            "\U00000238",
            "\U00000238",
            "\U00000238"
        };


        public static Position[] directions = new Position[]
        {
            new Position(1, 0),     //right
            new Position(-1, 0),    // left
            new Position(0, 1),     //down
            new Position(0, -1)     //up
        };

        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.BufferHeight = Console.WindowHeight = windowHeight;
            Console.BufferWidth = Console.WindowWidth = windowWidth;

            OpenCloseProgram.LoadFile(filePath, leaderboardNames, leaderboardPoints);
            Menu();
        }

        internal static void Menu()
        {
            Console.CursorVisible = false;
            Console.Clear();
            int currentSelection = 0;
            sleep = 150;
            playerPoints = 0;
            oldPlayerPoints = 0;

            liveNumber = new List<string>()
            {
                "\U00002022",
                "\U00002022",
                "\U00002022"
            };

            while (true)
            {
                Print.PrintData(0, lowerMenuBorder, new string('-', windowWidth), ConsoleColor.Magenta);
                Print.PrintData(2, 3, menuIcon, ConsoleColor.Magenta);
                Print.PrintData(0, upperMenuBorder, new string('-', windowWidth), ConsoleColor.DarkMagenta);

                Print.PrintOptionsMenu(options, currentSelection);

                currentSelection = GetCurrentMenuSelection(currentSelection);
            }
        }

        internal static int GetCurrentMenuSelection(int currentSelection)
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                Print.PrintData(15, 10 + currentSelection, " ", ConsoleColor.White);
                currentSelection++;

                if (currentSelection > options.Length - 1)
                {
                    currentSelection = 0;
                }
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow)
            {
                Print.PrintData(15, 10 + currentSelection, " ", ConsoleColor.White);
                currentSelection--;

                if (currentSelection < 0)
                {
                    currentSelection = 4;
                }
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                StartFromChosenMenuOption(currentSelection);
            }

            return currentSelection;
        }

        internal static void StartFromChosenMenuOption(int currentSelection)
        {
            if (currentSelection == 0)
            {
                StartGame();
            }
            else if (currentSelection == 1)
            {
                Print.Controls(windowWidth, lowerMenuBorder, upperMenuBorder);
            }
            else if (currentSelection == 2)
            {
                Print.Leaderboard(leaderboardNames, leaderboardPoints, windowWidth, lowerMenuBorder, upperMenuBorder);
            }
            else if (currentSelection == 3)
            {
                Console.Clear();
                LevelsMenu();
            }
            else if (currentSelection == 4)
            {
                Console.Clear();
                OpenCloseProgram.SaveFile(filePath, leaderboardNames, leaderboardPoints);
                Environment.Exit(0);
            }
        }

        internal static void LevelsMenu()
        {
            Console.CursorVisible = false;
            int currentSelection = 0;

            while (true)
            {
                Print.PrintData(0, lowerMenuBorder, new string('-', windowWidth), ConsoleColor.Magenta);
                Print.PrintData(16, 8, string.Format("{0}: {1}", "Level", level));
                Print.PrintData(0, upperMenuBorder, new string('-', windowWidth), ConsoleColor.DarkMagenta);

                int printPosition = 10;

                PrintDifficultyMenu(difficultyOptions, printPosition, currentSelection);
                currentSelection = MenuKeyPressing(currentSelection);
            }
        }

        internal static void PrintDifficultyMenu(string[] difficultyOptions, int printPosition, int currentSelection)
        {
            Print.PrintData(15, printPosition + currentSelection, "> ", ConsoleColor.White);

            foreach (var i in difficultyOptions)
            {
                if (i != "Return to Menu")
                {
                    Print.PrintData(17, printPosition, i, ConsoleColor.White);
                    printPosition++;
                }
                else
                {
                    Print.PrintData(17, printPosition, i, ConsoleColor.Green);
                    printPosition++;
                }
            }
        }

        internal static int MenuKeyPressing(int currentSelection)
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                Print.PrintData(15, 10 + currentSelection, " ", ConsoleColor.White);
                currentSelection++;

                if (currentSelection > difficultyOptions.Length - 1)
                {
                    currentSelection = 0;
                }
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow)
            {
                Print.PrintData(15, 10 + currentSelection, " ", ConsoleColor.White);
                currentSelection--;

                if (currentSelection < 0)
                {
                    currentSelection = 4;
                }
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                level = MenuChange.RunMenuOption(currentSelection, level);
            }

            return currentSelection;
        }

        internal static void StartGame()
        {
            GameSounds.PlayNewGameSound();
            GameSounds.PlayMovingSound();
            playerPoints = 0;
            oldPlayerPoints = 0;// Starting player points
            direction = 0; // 0 right 1 left 2 down 3 up
            Print.StartSnakeElements(snakeElements);

            Position food = new Position(randomGenerator.Next(0, Console.WindowWidth), randomGenerator.Next(6, Console.WindowHeight - 1));

            while (true)
            {

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo command = Console.ReadKey();

                    direction = Moving.ChangeDirection(command, direction);

                }

                Position snakeHead = snakeElements.Last();
                Position snakeNewHead = new Position(snakeHead.X + directions[direction].X, snakeHead.Y + directions[direction].Y);

                if (snakeNewHead.X > Console.WindowWidth - 1 || snakeNewHead.X < 0 ||
                    snakeNewHead.Y < 5 || snakeNewHead.Y > Console.WindowHeight - 1 || snakeElements.Contains(snakeNewHead) || obstacle.Contains(snakeNewHead))
                {
                    if (liveNumber.Count > 1)
                    {
                        GameSounds.PlayDeathSound();
                        liveNumber.Remove(liveNumber[0]);
                        Thread.Sleep(2000);
                        GameSounds.PlayMovingSound();

                        direction = 0;
                        snakeElements.Clear();

                        for (int i = 0; i <= 5; i++)
                        {
                            snakeElements.Enqueue(new Position(i, 5));
                        }

                        foreach (Position position in snakeElements)
                        {
                            Print.PrintSnake(position.X, position.Y, '\U000025A1');
                        }

                        snakeNewHead = new Position(6, 5);
                    }
                    else
                    {
                        level = 1;
                        Console.Clear();
                        GameSounds.PlayDeathSound();
                        Print.PrintData(22, 15, "Game Over!", ConsoleColor.Yellow);

                        Console.WriteLine();
                        MenuChange.WriteName(leaderboardNames, leaderboardPoints, playerPoints);
                        Menu();
                    }
                }

                snakeElements.Enqueue(snakeNewHead);
                snakeElements.Dequeue();

                Moving.MoveSnake(windowWidth, difficultyOptions, snakeElements, playerPoints, level);
                Print.PrintObstacles(level, obstacle);
                Print.PrintLives(25, 3, "Lives: ", liveNumber);

                if (snakeNewHead.X == food.X && snakeNewHead.Y == food.Y)
                {
                    //GameSounds.PlayEatingSound();
                    playerPoints++; // for every food eaten score increases by 1
                    if (sleep > 70) // for every food eaten speed increases by ~2%
                    {
                        sleep -= 2;
                    } 
                    food = FoodCanBePrinted(food);
                    Print.PrintFood(food.X, food.Y, '\U00000238', ConsoleColor.Magenta);
                    snakeElements.Enqueue(snakeNewHead);

                    foreach (Position position in snakeElements)
                    {
                        Print.PrintSnake(position.X, position.Y, '\U000025A1');
                    }
                }

                food = FoodCanBePrinted(food);

                ChangeLevels();
                MenuChange.GetSpeedOfSnake(level, sleep);
            }
        }

        internal static Position FoodCanBePrinted(Position food)
        {
            if (!snakeElements.Contains(food) && !obstacle.Contains(food))
            {
                Print.PrintFood(food.X, food.Y, '\U00000238', ConsoleColor.Magenta);
            }
            else
            {
                food = new Position(randomGenerator.Next(0, Console.WindowWidth),
                    randomGenerator.Next(6, Console.WindowHeight - 1));
            }

            return food;
        }

        internal static void ChangeLevels()
        {
            if (playerPoints == oldPlayerPoints + 6 && level < 4)
            {
                oldPlayerPoints = playerPoints;

                level++;
            }
        }
    }
}
