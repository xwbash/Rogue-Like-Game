using DG.Tweening;
using Rune.Scripts.Interfaces;
using Rune.Scripts.Services;
using TMPro;
using UnityEngine;
using VContainer;

namespace Rune.Scripts.UI
{
    public class HitLabelController : MonoBehaviour, IPoolableObject
    {
        public int HitDamage = 0;
        
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private TMP_Text m_textLabel;
        
        [Inject] private HitLabelService _hitLabelService;
        private PoolingService _poolingService;
        private bool _isObjectActive = true;
        
        public void Init(ISpawner spawner)
        {
            
        }

        public bool IsObjectActive()
        {
            return _isObjectActive;
        }

        public void SetObjectActive(bool isActive)
        {
            gameObject.SetActive(!isActive);
            _isObjectActive = isActive;
        }

        public void OnObjectSpawned()
        {
           
        }

        public void RemoveObject()
        {
            _hitLabelService.RemoveObject(this);
        }

        public void SetSpawnPoint(Vector3 spawnPoint)
        {
            transform.position = spawnPoint;
        }

        public PoolingService GetPoolingService()
        {
            return _poolingService;
        }

        public void SetPoolingService(PoolingService poolingService)
        {
            _poolingService = poolingService;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void StartAnimation(int weaponDamage)
        {
            m_textLabel.text = "-" + weaponDamage.ToString();
            m_canvasGroup.alpha = 1;
            transform.DOMoveY(transform.position.y + 1, .65f);
            m_canvasGroup.DOFade(0, .65f)
                .OnComplete(() =>
                {
                    RemoveObject();
                });
        }
    }
}