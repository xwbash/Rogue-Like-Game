using System;
using Rune.Scripts.Base;
using Rune.Scripts.Gameplay.Character_Related;
using Rune.Scripts.Services;
using UnityEngine;
using VContainer;

namespace Rune.Scripts.Gameplay
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem m_particleSystem;
        [SerializeField] private float m_playerSpeed;
        [SerializeField] private float m_playerRotationSpeed;
        [SerializeField] private Transform m_rotationTransform;

        private bool _isGamePaused = false;
        private InputService _inputService;
        private Rigidbody _rigidBody;
        private CommonPlayerService _commonPlayerService;
        private PlayerData _playerData;
        private GameCycleService _gameCycleService;
        
        [Inject]
        private void Construct(InputService inputService, CommonPlayerService commonPlayerService, GameCycleService gameCycleService)
        {
            _gameCycleService = gameCycleService;
            _inputService = inputService;
            _commonPlayerService = commonPlayerService;
        }

        private void OnEnable()
        {
            _gameCycleService.OnGamePaused.AddListener(OnGamePaused);
            _gameCycleService.OnGameContinued.AddListener(OnGameContinued);
            _inputService.AddListener(OnPlayerMove);
        }
        
        private void OnDisable()
        {
            _gameCycleService.OnGamePaused.RemoveListener(OnGamePaused);
            _gameCycleService.OnGameContinued.RemoveListener(OnGameContinued);
            _inputService.RemoveListener(OnPlayerMove);
        }

        private void OnGamePaused()
        {
            _isGamePaused = true;
        }

        private void OnGameContinued()
        {
            _isGamePaused = false;
        }
        
        private void Start()
        {
            _playerData = GetComponent<PlayerBase>().PlayerData;
            _rigidBody = GetComponent<Rigidbody>();
        }
        
        private void OnPlayerMove(InputData inputData)
        {
            if (_isGamePaused)
            {
                _rigidBody.velocity = Vector3.zero;
                return;
            }
            var horizontal = inputData.Horizontal;
            var vertical  = inputData.Vertical;

            if (horizontal != 0 || vertical != 0)
            {
                if (!m_particleSystem.isPlaying)
                {
                    m_particleSystem.Play();
                }
            }
            
            _rigidBody.velocity = new Vector3(horizontal * m_playerSpeed, 0, vertical * m_playerSpeed);
            
            Vector2 joystickDirection = inputData.Direction;

            var closestEnemy = _commonPlayerService.GetClosestEnemy();

            if (closestEnemy)
            {
                var closestEnemyDistance = Vector3.Distance(closestEnemy.transform.position, transform.position);
            
                if (closestEnemyDistance < _playerData.Range)
                {
                    Vector3 direction = closestEnemy.transform.position - transform.position;
                    RotatePlayerAlongInput( new Vector2(direction.normalized.x, direction.normalized.z));
                }
                else
                {
                    RotatePlayerAlongInput(joystickDirection);
                }
            }
            else
            {
                RotatePlayerAlongInput(joystickDirection);
            }
        }

        private void RotatePlayerAlongInput(Vector2 direction)
        {
            if (direction != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

                Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

                m_rotationTransform.rotation = Quaternion.Slerp(m_rotationTransform.rotation, targetRotation, Time.deltaTime * m_playerRotationSpeed);
            }   
        }
    }
}