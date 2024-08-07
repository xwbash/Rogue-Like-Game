using Rune.Scripts.Interfaces;
using Rune.Scripts.UI;

namespace Rune.Scripts.Services
{
    public class UIService 
    {
        private readonly MainUIController _mainUIController;
        private readonly LostUIController _lostUIController;


        public UIService(LostUIController lostUIController, MainUIController mainUIController)
        {
            _lostUIController = lostUIController;
            _mainUIController = mainUIController;
        }
        
        public void ShowCards()
        {
            _mainUIController.ShowCards();
        }

        public void OnLost()
        {
            _lostUIController.ShowUI();
        }
    }
}