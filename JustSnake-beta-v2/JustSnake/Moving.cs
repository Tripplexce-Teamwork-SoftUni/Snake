namespace JustSnake
{
    using System;
    using System.Collections.Generic;

    internal class Moving
    {
        internal static void PauseGame()
        {
            GameSounds.StopMovingSound();

            while (true)
            {
                Print.PrintData(2, 25, "Menu key - M", ConsoleColor.Blue);
                Print.PrintData(2, 26, "Unpause key - Spacebar", ConsoleColor.Blue);
                ConsoleKeyInfo unpause = Console.ReadKey();

                if (unpause.Key == ConsoleKey.Spacebar)
                {
                    GameSounds.PlayMovingSound();

                    return;
                }
                else if (unpause.Key == ConsoleKey.M)
                {
                    MainGameCode.Menu();
                }
            }
        }

        internal static void MoveSnake(int windowWidth, string[] difficultyOptions, Queue<Position> snakeElements, int playerPoints, int level)
        {
            Print.SnakePlayingMenu(windowWidth, difficultyOptions, playerPoints, level);

            foreach (Position position in snakeElements)
            {
                Print.PrintSnake(position.X, position.Y, '\U000025A1');
            }
        }

        internal static int ChangeDirection(ConsoleKeyInfo command, int direction)
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
                Moving.PauseGame();
            }

            return direction;
        }
    }
}
