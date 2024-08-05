using DG.Tweening;
using Rune.Scripts.Interfaces;
using Rune.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Rune.Scripts.UI
{
    public class MainUIController : MonoBehaviour, IUIObject
    {
        [SerializeField] private Button m_startGameButton;
        private CanvasGroup _canvasGroup;
        private SceneService _sceneService;
        
        [Inject]
        private void Consturct(SceneService sceneService)
        {
            _sceneService = sceneService;
        }
        
        private void OnEnable()
        {
            m_startGameButton.onClick.AddListener(OnClickedStart);
        }

        private void OnDisable()
        {
            m_startGameButton.onClick.RemoveListener(OnClickedStart);
        }

        private void OnClickedStart()
        {
            OnButtonClick();
        }
        
        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            ShowUI();
        }

        public void ShowUI()
        {
            _canvasGroup.DOFade(1, .5f);
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