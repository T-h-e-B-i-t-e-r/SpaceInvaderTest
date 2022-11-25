using UnityEngine;

namespace Data
{
    public class GameWorldState
    {
        public int EnemyCount;
        public int Score;
        public float VerticalGameOverLimit { get; private set; }
        public Vector2 HorizontalScreenLimits { get; private set; }
        public float TopOfScreen { get; private set; }

        public GameWorldState(Vector2 horizontalLimits, Vector2 verticalLimits, int enemyCount)
        {
            HorizontalScreenLimits = horizontalLimits;
            VerticalGameOverLimit = verticalLimits.x;
            TopOfScreen = verticalLimits.y;
            EnemyCount = enemyCount;
        }
    }
}
