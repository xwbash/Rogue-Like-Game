using VContainer;

namespace Rune.Scripts.Services
{
    public class GameCycleService
    {
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
    }
}
