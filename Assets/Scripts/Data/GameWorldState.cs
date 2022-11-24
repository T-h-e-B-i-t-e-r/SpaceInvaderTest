using UnityEngine;

namespace Data
{
    public class GameWorldState
    {
        public int WaveCount;
        public int Score;
        public float VerticalGameOverLimit { get; private set; }
        public Vector2 HorizontalScreenLimits { get; private set; }

        public void Initialize(Vector2 horizontalLimit, float verticalLimit)
        {
            HorizontalScreenLimits = horizontalLimit;
            VerticalGameOverLimit = verticalLimit;
        }
    }
}
