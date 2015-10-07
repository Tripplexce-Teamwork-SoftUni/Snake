namespace JustSnake
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    internal class MenuChange
    {
        internal static void RunMenuOption(int currentSelection, int level)
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
                MainGameCode.Menu();
            }
        }

        internal static void WriteName(List<string> leaderboardNames, List<int> leaderboardPoints, int playerPoints)
        {
            //Console.Clear();

            Print.PrintData(0, 6, "Write your name: ");
            string name = Console.ReadLine();
            name = name.Trim();

            if (name.Length < 1)
            {
                Print.PrintError();
                WriteName(leaderboardNames, leaderboardPoints, playerPoints);
            }
            else
            {
                leaderboardNames.Add(name);
                leaderboardPoints.Add(playerPoints);

                LeaderboardSort(leaderboardNames, leaderboardPoints);

                if (leaderboardNames.Count > 10)
                {
                    leaderboardNames.RemoveAt(10);
                    leaderboardPoints.RemoveAt(10);
                }
            }
        }

        /// <summary>
        /// Sorting Leaderboard Method
        /// </summary>
        internal static void LeaderboardSort(List<string> leaderboardNames, List<int> leaderboardPoints)
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

        internal static void GetSpeedOfSnake(int level, int sleep)
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
    }
}
