using System;
using Rune.Scripts.Gameplay.Character_Related;
using Rune.Scripts.Services;
using Rune.Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace Rune.Scripts.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem m_particleSystem;
        [SerializeField] private float m_playerRotationSpeed;
        [SerializeField] private Transform m_rotationTransform;

        private float _entitySpeed;
        private float _entityRange;
        private bool _isGamePaused = false;
        private InputService _inputService;
        private Rigidbody _rigidBody;
        private CommonPlayerService _commonPlayerService;
        private GameCycleService _gameCycleService;
        private AbilityService _abilityService;

        [Inject]
        private void Construct(InputService inputService, CommonPlayerService commonPlayerService, GameCycleService gameCycleService, AbilityService abilityService)
        {
            _abilityService = abilityService;
            _gameCycleService = gameCycleService;
            _inputService = inputService;
            _commonPlayerService = commonPlayerService;
        }

        private void OnEnable()
        {
            _abilityService.OnAbilitySelected.AddListener(OnAbilityUpdate);   
            _gameCycleService.OnGamePaused.AddListener(OnGamePaused);
            _gameCycleService.OnGameContinued.AddListener(OnGameContinued);
            _inputService.AddListener(OnPlayerMove);
        }

        
        private void OnDisable()
        {
            _abilityService.OnAbilitySelected.RemoveListener(OnAbilityUpdate);
            _gameCycleService.OnGamePaused.RemoveListener(OnGamePaused);
            _gameCycleService.OnGameContinued.RemoveListener(OnGameContinued);
            _inputService.RemoveListener(OnPlayerMove);
        }
        

        private void OnAbilityUpdate(CardData cardData)
        {
            if (cardData.Speed > 0)
            {
                _entitySpeed += (_entitySpeed * cardData.Speed);
            }

            if (cardData.Range > 0)
            {
                _entityRange += cardData.Range;
            }
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
            var entityBase = GetComponent<Player>();
            
            if (!entityBase)
            {
                throw new Exception("Couldn't not found Player");
            }

            _entityRange = entityBase.GetRange();
            _entitySpeed = entityBase.GetSpeed();
            
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
            
            _rigidBody.velocity = new Vector3(horizontal * _entitySpeed, 0, vertical * _entitySpeed);
            
            Vector2 joystickDirection = inputData.Direction;

            var closestEnemy = _commonPlayerService.GetClosestEnemy();

            if (closestEnemy)
            {
                var closestEnemyDistance = Vector3.Distance(closestEnemy.transform.position, transform.position);
            
                if (closestEnemyDistance < _entityRange)
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