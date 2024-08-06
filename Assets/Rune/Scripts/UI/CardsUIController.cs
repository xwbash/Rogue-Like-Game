using System;
using System.Collections.Generic;
using Rune.Scripts.Interfaces;
using Rune.Scripts.Services;
using UnityEngine;
using VContainer;

namespace Rune.Scripts.UI
{
    public class CardsUIController : MonoBehaviour, IUIObject
    {
        [SerializeField] private Transform m_startTransform;
        [SerializeField] private Transform m_endTransform;
        
        [SerializeField] private GameObject m_cardGameObject;

        private ExperimentService _experimentService;
        private List<SelectionCard> _cardsList = new List<SelectionCard>();

        [Inject]
        private void Construct(ExperimentService experimentService)
        {
            _experimentService = experimentService;
        }
        
        private void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                var cardGameObject = Instantiate(m_cardGameObject, transform, false);

                var selectionCard = cardGameObject.GetComponent<SelectionCard>();

                if (!selectionCard)
                {
                    throw new ArgumentException("Kartlarda selection card yok");
                }
                
                selectionCard.Init(m_startTransform, m_endTransform);
                selectionCard.HideUI();
                _cardsList.Add(selectionCard);    
            }
        }

        public void ShowUI()
        {
            _experimentService.PauseGame();
            foreach (var card in _cardsList)
            {
                card.ShowUI();
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