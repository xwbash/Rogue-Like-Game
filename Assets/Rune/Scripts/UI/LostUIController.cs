using System;
using DG.Tweening;
using Rune.Scripts.Interfaces;
using Rune.Scripts.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace Rune.Scripts.UI
{
    public class LostUIController : MonoBehaviour, IUIObject
    {
        [SerializeField] private Button m_retryButton;
        private CanvasGroup _canvasGroup;
        private SceneService _sceneService;
        
        [Inject]
        private void Consturct(SceneService sceneService)
        {
            _sceneService = sceneService;
        }

        private void OnEnable()
        {
            m_retryButton.onClick.AddListener(OnClickedRetry);
        }

        private void OnDisable()
        {
            m_retryButton.onClick.RemoveListener(OnClickedRetry);
        }

        private void OnClickedRetry()
        {
            OnButtonClick();
        }

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowUI()
        {
            _canvasGroup.DOFade(1, .5f).OnComplete(() =>
            {
                _sceneService.UnloadGameScene();
            });
            
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void HideUI()
        {
            _canvasGroup.DOFade(0, .5f);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnButtonClick()
        {
            _sceneService.ShowGameScene();
            HideUI();
        }
    }
}