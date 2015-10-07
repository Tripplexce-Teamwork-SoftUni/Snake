namespace JustSnake
{
    using System.Media;

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

        public static void StopMovingSound()
        {
            movingplayer.Stop();
        }

        public static void PlayNewGameSound()
        {
            player = new SoundPlayer(@"..\..\sounds\snake_new.wav");
            player.PlaySync();
        }
    }
}
