using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Rune.Scripts.UI
{
    public class ProgressBarController : MonoBehaviour
    {
        [SerializeField] private Image m_progressBar;

        public void SetProgressBar(float minValue, float maxValue, float currentValue)
        {
            var lerpValue = Mathf.InverseLerp(minValue, maxValue, currentValue);
            m_progressBar.DOColor(Color.Lerp(Color.red, Color.green, lerpValue), .25f);
            m_progressBar.DOFillAmount(lerpValue, .25f);
        }
    }
}
