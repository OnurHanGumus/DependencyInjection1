using Events.External;
using Zenject;
using UnityEngine;
using Data.MetaData;

namespace Installers.Scenes
{
    public class MainSceneInstaller : MonoInstaller<MainSceneInstaller>
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject enemy;
        private BulletSettings _bulletSettings;

        public override void InstallBindings()
        {
            BindComponents();
            BindSettings();
        }

        void BindComponents()
        {

            Container.Bind<MainSceneInputEvents>().AsSingle();
            Container.Bind<PoolSignals>().AsSingle();
            Container.Bind<PlayerEvents>().AsSingle();
            Container.BindMemoryPool<BulletCollisionDetector, BulletCollisionDetector.Pool>().FromComponentInNewPrefab(bullet).UnderTransformGroup("Bullets"); ;
            Container.BindMemoryPool<Enemy, Enemy.Pool>().FromComponentInNewPrefab(enemy);

            Container.Bind<BulletPool>().AsSingle();
            Container.Bind<EnemyPool>().AsSingle();
        }

        private void BindSettings()
        {
            _bulletSettings = Resources.Load<BulletSettings>("BulletSettings");

            Container.BindInstance(_bulletSettings).AsSingle();
        }



    }
}
