namespace JustSnake
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Media;

    internal struct Position
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
        private static int windowHeight = 30;

        private static int windowWidth = 60;

        private static int level = 1;

        private static int direction;   // 0 right 1 left 2 down 3 up

        private static string menuIcon = @"    ___         ___     ______           ___        
      | |         | |    / ____|           | |       
      | |_   _ ___| |_  | (___  _ __   __ _| | _____ 
  _   | | | | / __| __|  \___ \| '_ \ / _` | |/ / _ \
 | |__| | |_| \__ \ |_   ____) | | | | (_| |   <  __/
  \____/ \__,_|___/\__| |_____/|_| |_|\__,_|_|\_\___|
                                                     
                                                     ";

        private static Random randomGenerator = new Random();

        private static Queue<Position> snakeElements = new Queue<Position>();

        private static List<Position> obstacle = new List<Position>();

        private static List<string> leaderboardNames = new List<string>();   // Leaderboard Name List

        private static List<int> leaderboardPoints = new List<int>();   // Leaderboard Points List

        private static int playerPoints = 0;    // Player points 

        private static int oldPlayerPoints = 0;

        private static int sleep = 150;

        private static string filePath = "../../file.md";

        private static string[] options = { "New Game", "HighScores", "Choose Difficulty", "Quit" };

        private static string[] difficultyOptions = { "Level 1", "Level 2", "Level 3", "Level 4", "Return" };

        private static int upperMenuBorder = 1;

        private static int lowerMenuBorder = 28;

        private static List<string> liveNumber = new List<string>()
        {
            "&",
            "&",
            "&"
        };


        private static Position[] directions = new Position[]
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
            
            LoadFile();
            Menu();          
        }

        private static void Menu()
        {
            Console.CursorVisible = false;
            Console.Clear();
            int currentSelection = 0;
            liveNumber = new List<string>()
            {
                "&",
                "&",
                "&"
            };

            while (true)
            {
                PrintData(0, lowerMenuBorder, new string('-', windowWidth), ConsoleColor.Magenta);
                PrintData(2, 3, menuIcon, ConsoleColor.Magenta);
                PrintData(0, upperMenuBorder, new string('-', windowWidth), ConsoleColor.DarkMagenta);

                PrintOptionsMenu(currentSelection);

                currentSelection = GetCurrentMenuSelection(currentSelection);
            }
        }

        private static int GetCurrentMenuSelection(int currentSelection)
        {
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
                StartFromChosenMenuOption(currentSelection);
            }

            return currentSelection;
        }

        private static void StartFromChosenMenuOption(int currentSelection)
        {
            if (currentSelection == 0)
            {
                StartGame();
            }
            else if (currentSelection == 1)
            {
                Leaderboard();
            }
            else if (currentSelection == 2)
            {
                Console.Clear();
                LevelsMenu();
            }
            else if (currentSelection == 3)
            {
                Console.Clear();
                SaveFile();
                Environment.Exit(0);
            }
        }

        private static void PrintOptionsMenu(int currentSelection)
        {
            int printPosition = 10;

            PrintData(15, printPosition + currentSelection, "> ", ConsoleColor.White);

            foreach (var i in options)
            {
                PrintData(17, printPosition, i, ConsoleColor.White);
                printPosition++;
            }
        }

        private static void LevelsMenu()
        {
            Console.CursorVisible = false;
            int currentSelection = 0;

            while (true)
            {
                PrintData(0, lowerMenuBorder, new string('-', windowWidth), ConsoleColor.Magenta);
                PrintData(16, 8, string.Format("{0}: {1}", "Level", level));
                PrintData(0, upperMenuBorder, new string('-', windowWidth), ConsoleColor.DarkMagenta);

                int printPosition = 10;

                PrintDifficultyMenu(printPosition, currentSelection);
                currentSelection = MenuKeyPressing(currentSelection);
            }
        }

        private static void PrintDifficultyMenu(int printPosition, int currentSelection)
        {
            PrintData(15, printPosition + currentSelection, "> ", ConsoleColor.White);

            foreach (var i in difficultyOptions)
            {
                PrintData(17, printPosition, i, ConsoleColor.White);
                printPosition++;
            }
        }

        private static int MenuKeyPressing(int currentSelection)
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                PrintData(15, 10 + currentSelection, " ", ConsoleColor.White);
                currentSelection++;

                if (currentSelection > difficultyOptions.Length - 1)
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
                RunMenuOption(currentSelection);
            }

            return currentSelection;
        }

        private static void RunMenuOption(int currentSelection)
        {
            if (currentSelection == 0)
            {
                level = 1;
            }
            else if (currentSelection == 1)
            {
                level = 2;
            }
            else if (currentSelection == 2)
            {
                level = 3;
            }
            else if (currentSelection == 3)
            {
                level = 4;
            }
            else if (currentSelection == 4)
            {
                Console.Clear();
                Menu();
            }
        }

        private static void Leaderboard()
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

            PrintData(0, i + 8, "Press any key to continue", ConsoleColor.White);
            Console.ReadKey();

            Menu();
        }

        private static void StartGame()
        {
            GameSounds.PlayNewGameSound();
            GameSounds.PlayMovingSound();
            playerPoints = 0;   // Starting player points
            direction = 0; // 0 right 1 left 2 down 3 up
            StartSnakeElements();
            
            Position food = new Position(randomGenerator.Next(0, Console.WindowWidth), randomGenerator.Next(6, Console.WindowHeight - 1));
            
            while (true)
            {
                
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo command = Console.ReadKey();

                    direction = ChangeDirection(command, direction);
                    
                }

                Position snakeHead = snakeElements.Last();
                Position snakeNewHead = new Position(snakeHead.X + directions[direction].X, snakeHead.Y + directions[direction].Y);

                if (snakeNewHead.X > Console.WindowWidth - 1 || snakeNewHead.X < 0 ||
                    snakeNewHead.Y < 5 || snakeNewHead.Y > Console.WindowHeight - 1 || snakeElements.Contains(snakeNewHead) || obstacle.Contains(snakeNewHead))
                {
                    if (liveNumber.Count >= 1)
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
                            PrintSnake(position.X, position.Y, '\U000025A1');
                        }
                        snakeNewHead = new Position(6, 5);
                    }
                    else
                    {
                    Console.Clear();
                    GameSounds.PlayDeathSound();
                    PrintData(22, 15, "Game Over!", ConsoleColor.Yellow);

                    Console.WriteLine();
                    WriteName();
                    Menu();
                    }
                }

                snakeElements.Enqueue(snakeNewHead);
                snakeElements.Dequeue();
                
                MoveSnake();
                PrintObstacles(level, obstacle);
                PrintLives(25, 3, "Lives: ", liveNumber);

                if (snakeNewHead.X == food.X && snakeNewHead.Y == food.Y)
                {
                    //GameSounds.PlayMovingSound();
                    playerPoints++; // for every food eaten score increases by 1
                    sleep -= 2; // for every food eaten speed increases by ~2%
                    food = FoodCanBePrinted(food);
                    PrintFood(food.X, food.Y, '\U00000298', ConsoleColor.Magenta);
                    snakeElements.Enqueue(snakeNewHead);
                    foreach (Position position in snakeElements)
                    {
                        PrintSnake(position.X, position.Y, '\U000025A1');
                    }
                }

                food = FoodCanBePrinted(food);

                ChangeLevels();
                GetSpeedOfSnake();
                
            }
        }
        private static void PrintLives(int x, int y, string lives, List<string> liveNumber, ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(lives);
            Console.WriteLine(string.Join(" ", liveNumber));
        }
        private static Position FoodCanBePrinted(Position food)
        {
            if (!snakeElements.Contains(food) && !obstacle.Contains(food))
            {
                PrintFood(food.X, food.Y, '\U000000AE', ConsoleColor.Magenta);
            }
            else
            {
                food = new Position(randomGenerator.Next(0, Console.WindowWidth),
                    randomGenerator.Next(6, Console.WindowHeight - 1));
            }

            return food;
        }

        private static void ChangeLevels()
        {
            if (playerPoints == oldPlayerPoints + 6 && level < 4)
            {
                oldPlayerPoints = playerPoints;
                level++;
            }
        }

        private static void GetSpeedOfSnake()
        {
            if (level == 1)
            {
                Thread.Sleep(sleep);
            }
            else if (level == 2)
            {
                Thread.Sleep(sleep - 20);
            }
            else if (level == 3)
            {
                Thread.Sleep(sleep - 35);
            }
            else if (level == 4)
            {
                Thread.Sleep(sleep - 50);
            }
        }

        static void PrintFood(int x, int y, char symbol, ConsoleColor foodColor = ConsoleColor.Yellow)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = foodColor;
            Console.Write(symbol);
        }

        private static void PrintObstacles(int level, List<Position> obstacle, ConsoleColor color = ConsoleColor.Green)
        {
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
        private static void WriteName()
        {
            //Console.Clear();

            PrintData(0, 6, "Write your name: ");
            string name = Console.ReadLine();
            name = name.Trim();

            if (name.Length < 1)
            {
                PrintError();
                WriteName();
            }
            else
            {
                leaderboardNames.Add(name);
                leaderboardPoints.Add(playerPoints);

                LeaderboardSort();

                if (leaderboardNames.Count > 10)
                {
                    leaderboardNames.RemoveAt(10);
                    leaderboardPoints.RemoveAt(10);
                }
            }
        }

        private static void PrintError()
        {
            PrintData(0, 0, "Write a name without empty space!");
        }

        /// <summary>
        /// Sorting Leaderboard Method
        /// </summary>
        private static void LeaderboardSort()
        {
            int length = leaderboardPoints.Count;
            int temp = leaderboardPoints[0];
            string tempString = string.Empty;

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (leaderboardPoints[i] < leaderboardPoints[j])
                    {
                        temp = leaderboardPoints[i];
                        leaderboardPoints[i] = leaderboardPoints[j];
                        leaderboardPoints[j] = temp;

                        tempString = leaderboardNames[i];
                        leaderboardNames[i] = leaderboardNames[j];
                        leaderboardNames[j] = tempString;
                    }
                }
            }
        }

        private static void MoveSnake()
        {
            SnakePlayingMenu();
            foreach (Position position in snakeElements)
            {
                PrintSnake(position.X, position.Y, '\U000025A1');
            }
        }

        private static void SnakePlayingMenu()
        {
            Console.Clear();
            PrintData(0, 0, new string('-', windowWidth));
            PrintData(0, 2, string.Format("{0}{1}", new string(' ', windowWidth / 2 - 5),
                string.Format("{0}", "JUST SNAKE")), ConsoleColor.Red);
            PrintData(4, 2, string.Format("{0}", level.ToString()), ConsoleColor.Yellow);
            PrintData(45, 2, "Score: " + playerPoints, ConsoleColor.Green);
            PrintData(0, 4, new string('-', windowWidth));
        }

        private static int ChangeDirection(ConsoleKeyInfo command, int direction)
        {
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
            if (command.Key == ConsoleKey.Spacebar)
            {
                PauseGame();
            }

            return direction;
        }

        private static void PauseGame()
        {
            while (true)
            {
                PrintData(2, 25, "Menu key - M", ConsoleColor.Blue);
                PrintData(2, 26, "Unpause key - Spacebar", ConsoleColor.Blue);
                ConsoleKeyInfo unpause = Console.ReadKey();
                if (unpause.Key == ConsoleKey.Spacebar)
                {
                    return;
                }
                else if (unpause.Key == ConsoleKey.M)
                {
                    Menu();
                }
            }
        }

        private static void StartSnakeElements()
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

        private static void PrintData(int x, int y, string str, ConsoleColor color = ConsoleColor.Green)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }

        private static void PrintSnake(int x, int y, char snakeBody)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(snakeBody);
        }

        /// <summary>
        /// Load file leaderboard
        /// </summary>
        private static void LoadFile()
        {
            if (!File.Exists(filePath))
            {
                FileStream fileStream = File.Create(filePath);
                fileStream.Close();
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line = reader.ReadLine();

                if (line != null)
                {
                    string[] divider = line.Split(' ');
                    int index = 0;
                    int minusIndex = 2;

                    while (line != null)
                    {
                        index++;
                        divider = line.Split(' ');

                        if (index > 10)
                        {
                            break;
                        }
                        if (index > 1)
                        {
                            if (divider[0] == leaderboardNames[index - minusIndex] && int.Parse(divider[1]) == leaderboardPoints[index - minusIndex])
                            {
                                minusIndex++;

                                continue;
                            }
                        }

                        leaderboardNames.Add(divider[0]);
                        leaderboardPoints.Add(int.Parse(divider[1]));

                        line = reader.ReadLine();
                    }
                }
            }
        }

        /// <summary>
        /// Saving file leaderboard
        /// </summary>
        private static void SaveFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < leaderboardNames.Count; i++)
                {
                    writer.Write(string.Format("{0} {1}", leaderboardNames[i], leaderboardPoints[i]));
                    writer.WriteLine();
                }
            }
        }
    }

    internal class GameSounds
    {
        private static System.Media.SoundPlayer player;
        private static System.Media.SoundPlayer movingplayer;

        public static void PlayMovingSound()
        {
            movingplayer = new SoundPlayer(@"..\..\sounds\snake_move.wav");
            while (!player.IsLoadCompleted) ;
            movingplayer.PlayLooping();
        }

        public static void PlayDeathSound()
        {
            player = new SoundPlayer(@"..\..\sounds\snake_die.wav");
            player.Play();
        }

        //public static void PlayEathingSound()
        //{
        //    movingplayer.Stop();
        //    player = new SoundPlayer(@"..\..\sounds\snake_eat.wav");
        //    player.Play();
        //}

        public static void PlayNewGameSound()
        {
            player = new SoundPlayer(@"..\..\sounds\snake_new.wav");
            player.PlaySync();
        }
    }
}
