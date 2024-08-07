using Rune.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Services
{
    public class ProgressbarService
    {
        private GameObject _progressBarObject;
        private IObjectResolver _objectResolver;
        
        public ProgressbarService(IObjectResolver objectResolver, GameObject progressBarObject)
        {
            _progressBarObject = progressBarObject;
            _objectResolver = objectResolver;
        }

        public ProgressBarController GetProgressBar(Transform parentTransform)
        {
            var progressBarObject = _objectResolver.Instantiate(_progressBarObject);
            progressBarObject.transform.SetParent(parentTransform, false);
            return progressBarObject.GetComponent<ProgressBarController>();
        }
    }
}