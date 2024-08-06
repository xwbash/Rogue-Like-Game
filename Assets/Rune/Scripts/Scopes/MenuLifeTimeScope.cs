using Rune.Scripts.Services;
using Rune.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Scopes
{
    public class MenuLifeTimeScope : LifetimeScope
    {
        [SerializeField] private LostUIController m_lostUIController;
        [SerializeField] private MainUIController m_mainUIController;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(m_lostUIController);
            builder.RegisterComponent(m_mainUIController);
            
            builder.Register<UIService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<ExperimentService>(Lifetime.Singleton).AsSelf();
            builder.Register<SceneService>(Lifetime.Singleton);
            builder.Register<GameCycleService>(Lifetime.Singleton);
            builder.Register<AbilityService>(Lifetime.Singleton);
        }
    }
}
