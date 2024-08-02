using Rune.Scripts.Services;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Scopes
{
    public class MenuLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<UIService>(Lifetime.Singleton);
        }
    }
}
