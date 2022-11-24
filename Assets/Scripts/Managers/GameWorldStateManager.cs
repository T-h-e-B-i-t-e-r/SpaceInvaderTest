using Data;
using UnityEngine;

namespace Managers
{
    public class GameWorldStateManager
    {
        private GameWorldState _gameWorldState;
        private Camera _gameplayCamera;
        
        public GameWorldState GameWorldState => _gameWorldState;
        public Camera GamePlayCamera => _gameplayCamera;
        
        public void Initialize(Camera gameplayCamera)
        {
            _gameplayCamera = gameplayCamera;
            
            var leftLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(0,0,0)); // left of screen
            var rightLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(1,0,0)); // right of screen
            float topLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y; // top of screen;
            
            var horizontalScreenLimits = new Vector2(leftLimit.x, rightLimit.x);
            var verticalGameOverLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(0, 0.1f, 0)).y; // bottom 10% of screen
             
            Vector2 verticalLimits = new Vector2(verticalGameOverLimit, topLimit);
            
            _gameWorldState = new GameWorldState();
            _gameWorldState.Initialize(horizontalScreenLimits, verticalLimits);
        }
    }
}
