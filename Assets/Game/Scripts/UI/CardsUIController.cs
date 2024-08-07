using System;
using System.Collections.Generic;
using Rune.Scripts.Interfaces;
using Rune.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.UI
{
    public class CardsUIController : MonoBehaviour
    {
        [SerializeField] private Transform m_startTransform;
        [SerializeField] private Transform m_endTransform;
        
        [SerializeField] private GameObject m_cardGameObject;

        private ExperimentService _experimentService;
        private List<SelectionCard> _cardsList = new List<SelectionCard>();
        private AbilityService _abilityService;
        private IObjectResolver _objectResolver;

        [Inject]
        private void Construct(ExperimentService experimentService, AbilityService abilityService, IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
            _experimentService = experimentService;
            _abilityService = abilityService;
        }
        
        private void Start()
        {
            
            var cardAbilites = _abilityService.GetNewCardData();
            
            for (int i = 0; i < cardAbilites.Count; i++)
            {
                var cardGameObject = _objectResolver.Instantiate(m_cardGameObject, transform, false);

                var selectionCard = cardGameObject.GetComponent<SelectionCard>();

                if (!selectionCard)
                {
                    throw new ArgumentException("There is no selection card in cards.");
                }
                
                selectionCard.Init(m_startTransform, m_endTransform, OnCardSelected);
                selectionCard.SetCardData(cardAbilites[i]);
                selectionCard.HideUI();
                _cardsList.Add(selectionCard);    
            }
        }

        private void OnCardSelected()
        {
            HideUI();
        }

        public void ShowUI()
        {
            _experimentService.PauseGame();
            var cardAbilities = _abilityService.GetNewCardData();
            
            for (int i = 0; i < _cardsList.Count; i++)
            {
                _cardsList[i].SetCardData(cardAbilities[i]);
                _cardsList[i].ShowUI();
            }
        }

        public void HideUI()
        {
            _experimentService.ContinueGame();
            
            foreach (var card in _cardsList)
            {
                card.HideUI();
            }
        }
    }
}