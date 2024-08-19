using UnityEngine.Events;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Services
{
    public class ExperimentService : IStartable
    {
        public UnityEvent<int> OnExperimentUpdated = new UnityEvent<int>();
        private int _currentExperiment;
        private int _currentLevel = 1;
        private int[] _experimentValue = new[] { 100, 300, 500, 1000, 1500, 2000, 3000, 5000, 10000, 15000 };
        private UIService _uiService;
        private readonly GameCycleService _gameCycleService;

        [Inject]
        public ExperimentService(UIService uiService, GameCycleService gameCycleService)
        {
            _gameCycleService = gameCycleService;
            _uiService = uiService;
        }
        
        public void Start()
        {
             AddExperiment(0);   
        }

        public void AddExperiment(int playerDataExperimentAmount)
        {
            _currentExperiment += playerDataExperimentAmount;
            ExperimentUpdated();
        }

        private void ExperimentUpdated()
        {
            OnExperimentUpdated.Invoke(_currentExperiment);
        }

        public int GetNextExperiment()
        {
            for (int i = 0; i < _experimentValue.Length; i++)
            {
                var experimentValue = _experimentValue[i];
                
                if (i + 1 > _currentLevel)
                {
                    LevelUpdated();
                }
                
                if (experimentValue > _currentExperiment)
                {
                    return experimentValue;
                }
            }

            return 0;
        }

        private void LevelUpdated()
        {
            _currentLevel++;
            _uiService.ShowCards();
        }

        public int GetPreviousExperiment()
        {
            int previousExperiment = 0;
            
            foreach (var experimentValue in _experimentValue)
            {
                if (experimentValue < _currentExperiment)
                {
                    previousExperiment = experimentValue;
                }
                else
                {
                    break;
                }
            }

            return previousExperiment;
        }

        public int GetLevel()
        {
            return _currentLevel;
        }

        public void ContinueGame()
        {
            _gameCycleService.ContinueGame();
        }

        public void PauseGame()
        {
            _gameCycleService.PauseGame();
        }
    }
}