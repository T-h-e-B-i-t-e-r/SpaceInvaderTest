using Data;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Managers
{
    public class GameWorldStateManager
    {
        // local
        private GameWorldState _gameWorldState;
        private Camera _gameplayCamera;
        private UnityAction<int> _updateEnemyUiAction;
        private UnityAction<int> _updateScoreUiAction;
        
        public GameWorldState GameWorldState => _gameWorldState;
        public Camera GamePlayCamera => _gameplayCamera;
        
        // injections
        private SceneLoader _sceneLoader;
        private GameResultsData _gameResultsData;
        
        [Inject]
        private void InjectDependencies(
            SceneLoader sceneLoader,
            GameResultsData gameResultsData
        )
        {
            _sceneLoader = sceneLoader;
            _gameResultsData = gameResultsData;
        }
        
        public void Initialize(Camera gameplayCamera, int enemyCount, UnityAction<int> updateEnemyUi, UnityAction<int> updateScoreUi)
        {
            _gameplayCamera = gameplayCamera;
            _updateEnemyUiAction = updateEnemyUi;
            _updateScoreUiAction = updateScoreUi;
            
            var leftLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(0,0,0)); // left of screen
            var rightLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(1,0,0)); // right of screen
            float topLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y; // top of screen;
            
            var horizontalScreenLimits = new Vector2(leftLimit.x, rightLimit.x);
            var verticalGameOverLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(0, 0.05f, 0)).y; // bottom 5% of screen
             
            Vector2 verticalLimits = new Vector2(verticalGameOverLimit, topLimit);
            
            _gameWorldState = new GameWorldState(horizontalScreenLimits, verticalLimits, enemyCount);
            _gameResultsData.Reset();
        }

        public void RegisterEnemyDestroyed()
        {
            _gameWorldState.EnemyCount--;
            _updateEnemyUiAction?.Invoke(_gameWorldState.EnemyCount);
            _gameResultsData.EnemiesDefeated++;
            _gameResultsData.Score += 100;
            _updateScoreUiAction?.Invoke(_gameResultsData.Score);
            CheckGameOutcome();
        }

        public void TriggerGameOver()
        {
            _gameResultsData.IsVictory = false;
            _sceneLoader.LoadScene(GameConstants.GameOverScene);
        }

        private void CheckGameOutcome()
        {
            if (_gameWorldState.EnemyCount <= 0)
            {
                _gameResultsData.IsVictory = true;
                _sceneLoader.LoadScene(GameConstants.GameOverScene);
            }
        }
    }
}
