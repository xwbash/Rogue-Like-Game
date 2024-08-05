using VContainer.Unity;

namespace Rune.Scripts.Services
{
    public class ExperimentService : IStartable
    {
        private int _currentExperiment;

        public void Start()
        {
            
        }

        public void AddExperiment(int playerDataExperimentAmount)
        {
            _currentExperiment += playerDataExperimentAmount;
            OnExperimentUpdated();
        }

        private void OnExperimentUpdated()
        {
            
        }
    }
}