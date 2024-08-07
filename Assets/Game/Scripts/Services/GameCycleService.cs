using UnityEngine.Events;
using VContainer;

namespace Rune.Scripts.Services
{
    public class GameCycleService
    {
        public UnityEvent OnGamePaused = new UnityEvent();
        public UnityEvent OnGameContinued = new UnityEvent();
        private UIService _uiService;
        private bool _isGamePaused = false;
        
        [Inject]
        public GameCycleService(UIService uiService)
        {
            _uiService = uiService;
        }
        
        public void OnPlayerDead()
        {
            _uiService.OnLost();
        }

        public void ContinueGame()
        {
            _isGamePaused = false;
            OnGameContinued.Invoke();
        }

        public void PauseGame()
        {
            _isGamePaused = true;
            OnGamePaused.Invoke();
        }

        public bool IsGamePaused()
        {
            return _isGamePaused;
        }
    }
}
