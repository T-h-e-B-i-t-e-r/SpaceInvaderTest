namespace Data
{
    public class GameResultsData
    {
        public bool IsVictory = false;
        public int EnemiesDefeated = 0;
        public int Score = 0;

        public void Reset()
        {
            IsVictory = false;
            EnemiesDefeated = 0;
            Score = 0;
        }
    }
}
