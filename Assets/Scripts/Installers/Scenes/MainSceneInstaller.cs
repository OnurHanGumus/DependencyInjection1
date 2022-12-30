using Events.External;
using Zenject;
using UnityEngine;

namespace Installers.Scenes
{
    public class MainSceneInstaller : MonoInstaller<MainSceneInstaller>
    {
        [SerializeField] private GameObject bullet;
        public override void InstallBindings()
        {
            InitExecutionOrder();
        }

        void InitExecutionOrder()
        {

            Container.Bind<MainSceneInputEvents>().AsSingle();
            Container.Bind<PoolSignals>().AsSingle();
            Container.Bind<PlayerEvents>().AsSingle();
            Container.BindMemoryPool<BulletCollisionDetector, BulletCollisionDetector.Pool>().FromComponentInNewPrefab(bullet);

            Container.Bind<BulletPool>().AsSingle();
        }



    }
}
