namespace JustSnake
{
    using System.Collections.Generic;
    using System.IO;

    internal class OpenCloseProgram
    {
        /// <summary>
        /// Load file leaderboard
        /// </summary>
        public static void LoadFile(string filePath, List<string> leaderboardNames, List<int> leaderboardPoints)
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
        public static void SaveFile(string filePath, List<string> leaderboardNames, List<int> leaderboardPoints)
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
}
