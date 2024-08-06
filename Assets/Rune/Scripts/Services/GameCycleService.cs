using UnityEngine.Events;
using VContainer;

namespace Rune.Scripts.Services
{
    public class GameCycleService
    {
        public UnityEvent OnGamePaused = new UnityEvent();
        public UnityEvent OnGameContinued = new UnityEvent();
        private UIService _uiService;
        
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
            OnGameContinued.Invoke();
        }

        public void PauseGame()
        {
            OnGamePaused.Invoke();
        }
    }
}
