using VContainer.Unity;

namespace Rune.Scripts.Services
{
    public class ExperimentService : IStartable
    {
        private int _currentExperiment;
        private int[] _experimentValue = new[] { 100, 300, 500, 1000, 1500, 2000, 3000, 5000, 10000, 15000 };

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

        private float CalculateLerp()
        {
            return 0;
        }
    }
}