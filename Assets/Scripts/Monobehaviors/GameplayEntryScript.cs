using Factories;
using TMPro;
using UnityEngine;
using Zenject;

namespace Monobehaviors
{
    public class GameplayEntryScript : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _enemyCountLabel;
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        
        // injections
        private EnemyShipFactory _enemyShipFactory;
        
        [Inject]
        private void InjectDependencies(
            EnemyShipFactory enemyShipFactory
            )
        {
            _enemyShipFactory = enemyShipFactory;
        }
        
        private void Start()
        {
            InitializeGameWorld();
        }

        private void InitializeGameWorld()
        {
            
        } 
    }
}
