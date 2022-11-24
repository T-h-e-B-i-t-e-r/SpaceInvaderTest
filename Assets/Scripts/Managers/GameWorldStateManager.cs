using Data;
using UnityEngine;

namespace Managers
{
    public class GameWorldStateManager
    {
        private GameWorldState _gameWorldState;

        public GameWorldState GameWorldState => _gameWorldState;
        
        public void Initialize(Camera gameplayCamera)
        {
            _gameWorldState = new GameWorldState();
            
            var leftLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(0,0,0)); // left of screen
            var rightLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(1,0,0)); // right of screen
            var horizontalScreenLimits = new Vector2(leftLimit.x, rightLimit.x);
            var verticalGameOverLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(0, 0.1f, 0)).y; // bottom 10% of screen
            
            _gameWorldState.Initialize(horizontalScreenLimits, verticalGameOverLimit);
        }
    }
}
