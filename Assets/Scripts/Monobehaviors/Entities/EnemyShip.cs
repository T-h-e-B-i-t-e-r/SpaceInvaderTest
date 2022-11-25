using System;
using Managers;
using UnityEngine;
using Zenject;

namespace Monobehaviors.Entities
{
    public class EnemyShip : MonoBehaviour
    {
        [SerializeField] private float _movementDistance;
        [SerializeField] private float _movementRate;
        [SerializeField] private int _horizontalMovementTicks;
        [SerializeField] private int _verticalMovementTicks;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        private enum MovementDirection
        {
            Left,
            Right,
            Down
        }
        
        private bool _initialDirectionChanged;
        private float _gameOverVerticalPos;
        private float _movementCounter;
        private int _currentMovementTicks;
        private MovementDirection _currentDirection;
        private MovementDirection _previousHorizontalDirection;
        
        // injections
        private GameWorldStateManager _gameWorldStateManager;
        private EntityPoolManager<EnemyShip> _enemyPoolManager;
        
        [Inject]
        private void InjectDependencies(
            GameWorldStateManager gameWorldStateManager,
            EntityPoolManager<EnemyShip> enemyPoolManager
        )
        {
            _gameWorldStateManager = gameWorldStateManager;
            _enemyPoolManager = enemyPoolManager;
        }

        private void Awake()
        {
            _movementCounter = 0;
            _currentMovementTicks = 0;
            _previousHorizontalDirection = _currentDirection = MovementDirection.Left;
            _gameOverVerticalPos = _gameWorldStateManager.GameWorldState.VerticalGameOverLimit;
            _initialDirectionChanged = false;
        }

        private void FixedUpdate()
        {
            _movementCounter += Time.deltaTime;

            if (_movementCounter > _movementRate)
            {
                _movementCounter = 0;
                MoveEnemyShip();
            }
        }

        private void MoveEnemyShip()
        {
            Vector3 position = _rigidbody2D.position;

            if (_rigidbody2D.position.y <= _gameOverVerticalPos)
            {
                _gameWorldStateManager.TriggerGameOver();
                return;
            }
            
            Vector2 targetPos;
            _currentMovementTicks++;
            
            switch (_currentDirection)
            {
                case MovementDirection.Left:
                    targetPos = new Vector2(position.x - _movementDistance, position.y);
                    CheckChangeDirection(_horizontalMovementTicks, true);
                    break;
                case MovementDirection.Right:
                    targetPos = new Vector2(position.x + _movementDistance, position.y);
                    CheckChangeDirection(_horizontalMovementTicks, true);
                    break;
                case MovementDirection.Down:
                default:
                    targetPos = new Vector2(position.x, position.y - _movementDistance);
                    CheckChangeDirection(_verticalMovementTicks, false);
                    break;
            }

            _rigidbody2D.MovePosition(targetPos);
        }

        private void CheckChangeDirection(int ticksToCheck, bool isHorizontal)
        {
            if (_currentMovementTicks >= ticksToCheck)
            {
                _currentMovementTicks = 0;

                if (isHorizontal)
                {
                    _previousHorizontalDirection = _currentDirection;
                    _currentDirection = MovementDirection.Down;
                }
                else
                {
                    if (_previousHorizontalDirection == MovementDirection.Left)
                    {
                        _currentDirection = MovementDirection.Right;   
                    }
                    else
                    {
                        _currentDirection = MovementDirection.Left;    
                    }
                }
                
                if (!_initialDirectionChanged)
                {
                    _initialDirectionChanged = true;
                    _horizontalMovementTicks *= 2;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _enemyPoolManager.ReturnObjectToPool(this);
            _gameWorldStateManager.RegisterEnemyDestroyed();
        }
    }
}
