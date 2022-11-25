using Data;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Monobehaviors
{
    public class GameOverEntryScript : MonoBehaviour
    {
        // local
        [SerializeField] private TextMeshProUGUI _resultLabel;
        [SerializeField] private TextMeshProUGUI _enemiesDefeatedLabel;
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        [SerializeField] private Button _playAgainButton;

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

        private void Awake()
        {
            _playAgainButton.onClick.AddListener(OnPlayAgainPressed);
            _resultLabel.text = _gameResultsData.IsVictory ? GameConstants.YouWin : GameConstants.YouLose;
            _enemiesDefeatedLabel.text = string.Format(GameConstants.EnemyDefeatedLabel, _gameResultsData.EnemiesDefeated);
            _scoreLabel.text = string.Format(GameConstants.ScoreLabel, _gameResultsData.Score);
        }

        private void OnPlayAgainPressed()
        {
            _sceneLoader.LoadScene(GameConstants.GameplayScene);
        }
    }
}
