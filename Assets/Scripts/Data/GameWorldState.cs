using UnityEngine;

namespace Data
{
    public class GameWorldState
    {
        public int WaveCount;
        public int Score;
        public float VerticalGameOverLimit { get; private set; }
        public Vector2 HorizontalScreenLimits { get; private set; }
        public float TopOfScreen { get; private set; }

        public void Initialize(Vector2 horizontalLimits, Vector2 verticalLimits)
        {
            HorizontalScreenLimits = horizontalLimits;
            VerticalGameOverLimit = verticalLimits.x;
            TopOfScreen = verticalLimits.y;
        }
    }
}
