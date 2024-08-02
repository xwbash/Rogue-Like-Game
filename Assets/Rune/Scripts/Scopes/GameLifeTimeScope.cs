using System.Collections.Generic;
using Cinemachine;
using Rune.Scripts.Gameplay.Guns_Related;
using Rune.Scripts.Services;
using Rune.Scripts.Spawner;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Scopes
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private CinemachineVirtualCamera m_playerCinemachine;
        [SerializeField] private Joystick m_joyStick;
        
        [SerializeField] private PlayerSpawner m_playerSpawner;
        [SerializeField] private EnemySpawner m_enemySpawner;
        
        [SerializeField] private List<ProjectileCreationData> m_bulletGameObject = new List<ProjectileCreationData>();
        protected override void Configure(IContainerBuilder builder)
        {
            // COMPONENTS
            builder.RegisterComponent(m_enemySpawner);
            builder.RegisterComponent(m_playerSpawner);

            // POOL - POOL OBJECTS
            builder.RegisterEntryPoint<BulletService>(Lifetime.Singleton).WithParameter(m_bulletGameObject).AsSelf();
            builder.Register<EnemyService>(Lifetime.Singleton);
            builder.Register<PoolingFactory>(Lifetime.Singleton);

            // OTHERS
            builder.Register<GameCycleService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<CommonPlayerService>(Lifetime.Singleton).AsSelf();
            builder.Register<PlayerService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<CameraService>(Lifetime.Singleton).WithParameter(m_playerCinemachine);
            builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf().WithParameter(m_joyStick);
        }
    }
}