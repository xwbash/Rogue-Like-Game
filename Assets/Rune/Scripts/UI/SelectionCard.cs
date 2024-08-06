using DG.Tweening;
using Rune.Scripts.Interfaces;
using UnityEngine;

namespace Rune.Scripts.UI
{
    public class SelectionCard : MonoBehaviour, IUIObject
    {
        private Transform _cardEndPosition;
        private Transform _cardStartPosition;
        
        public void Init(Transform cardStartPosition, Transform cardEndPosition)
        {
            _cardEndPosition = cardEndPosition;
            _cardStartPosition = cardStartPosition;
        }
        public void ShowUI()
        {
            transform.DOMoveY(_cardEndPosition.position.y, .35f);
        }

        public void HideUI()
        {
            transform.DOMoveY(_cardStartPosition.position.y, .35f);
        }
    }
}