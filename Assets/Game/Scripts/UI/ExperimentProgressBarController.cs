using System;
using DG.Tweening;
using Rune.Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Rune.Scripts.UI
{
    public class ExperimentProgressBarController : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_levelLabel;
        [SerializeField] private Image m_progressImage;
        
        private ExperimentService _experimentService;
        
        [Inject]
        private void Construct(ExperimentService experimentService)
        {
            _experimentService = experimentService;
        }

        private void OnEnable()
        {
            _experimentService.OnExperimentUpdated.AddListener(OnExperimentUpdated);
        }

        private void OnDisable()
        {
            _experimentService.OnExperimentUpdated.RemoveListener(OnExperimentUpdated);
        }

        private void OnExperimentUpdated(int value)
        {
            int nextExperiment = _experimentService.GetNextExperiment();
            int previousExperiment = _experimentService.GetPreviousExperiment();
            m_levelLabel.text = "LEVEL " + _experimentService.GetLevel();

            var lerpValue = Mathf.InverseLerp(previousExperiment, nextExperiment, value);
            m_progressImage.DOFillAmount(lerpValue, 0.5f);
        }
    }
}