using System;
using System.Collections.Generic;
using DG.Tweening;
using Rune.Scripts.Interfaces;
using Rune.Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VContainer;

namespace Rune.Scripts.UI
{
    [Serializable]
    public class CardData
    {
        public int Health = -1;
        public float Speed = -1;
        public float GunSpeed = -1;
        public float BulletSpeed = -1;
        public int Damage = -1;
        public int Range = -1;
        public float EnemySpeedDecreasePercentage = -1;
        public float EnemyDamageDecreasePercentage = -1;
        public float EnemyBulletSpeedDecreasePercentage = -1;
        public float ExperimentAmount = -1;
    }
    
    public class SelectionCard : MonoBehaviour, IUIObject
    {
        [SerializeField] private GameObject m_textGameObject;
        [SerializeField] private CardData m_abilitysOnCard = new CardData();
        [SerializeField] private Button _cardButton;
        
        private AbilityService _abilityService;
        private Transform _cardEndPosition;
        private List<GameObject> _createdTextObjects = new List<GameObject>();
        private Transform _cardStartPosition;
        private UnityAction _onCardClicked;

        [Inject]
        private void Construct(AbilityService abilityService)
        {
            _abilityService = abilityService;
        }
        
        private void OnEnable()
        {
            _cardButton.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _cardButton.onClick.RemoveListener(OnClicked);
        }

        public void SetCardData(CardData cardDatas)
        {
            foreach (var createdTextObject in _createdTextObjects)
            {
                Destroy(createdTextObject);
            }
            
            m_abilitysOnCard = cardDatas;

            List<string> abilityDescription = _abilityService.GetDescription(m_abilitysOnCard);

            foreach (var description in abilityDescription)
            {
                var instantiatedAbilityDescription = Instantiate(m_textGameObject, transform, false);
                var labelText = instantiatedAbilityDescription.GetComponent<TMP_Text>();
                labelText.text = description;
                _createdTextObjects.Add(instantiatedAbilityDescription);
            }
        }
        
        private void OnClicked()
        {
            _abilityService.SetNewAbilityCard(m_abilitysOnCard);
            _onCardClicked.Invoke();
        }

        public void Init(Transform cardStartPosition, Transform cardEndPosition, UnityAction onCardSelected)
        {
            _cardEndPosition = cardEndPosition;
            _cardStartPosition = cardStartPosition;
            _onCardClicked = onCardSelected;
        }
        public void ShowUI()
        {
            _cardButton.interactable = true;
            transform.DOMoveY(_cardEndPosition.position.y, .7f);
            transform.DOScale(Vector3.one, 0.8f);
            transform.DORotate(Vector3.up * 360, .7f);
        }

        public void HideUI()
        {
            _cardButton.interactable = false;
            transform.DOMoveY(_cardStartPosition.position.y, .7f);
            transform.DOScale(Vector3.zero, 0.8f);
            transform.DORotate(Vector3.up * 360, .7f);
        }
    }
}