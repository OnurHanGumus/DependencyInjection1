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
        [SerializeField] private GameObject explosionParticle;
        private BulletSettings _bulletSettings;

        public override void InstallBindings()
        {
            BindComponents();
            BindSettings();
        }

        void BindComponents()
        {

            Container.Bind<InputSignals>().AsSingle();
            Container.Bind<PoolSignals>().AsSingle();
            Container.Bind<PlayerSignals>().AsSingle();

            Container.BindMemoryPool<BulletCollisionDetector, BulletCollisionDetector.Pool>().WithInitialSize(5).FromComponentInNewPrefab(bullet).UnderTransformGroup("Bullets");
            Container.BindMemoryPool<Enemy, Enemy.Pool>().FromComponentInNewPrefab(enemy);
            Container.BindMemoryPool<Explosion, Explosion.Pool>().FromComponentInNewPrefab(explosionParticle).UnderTransformGroup("Particles");

            Container.Bind<PoolHolder>().AsSingle();
        }

        private void BindSettings()
        {
            _bulletSettings = Resources.Load<BulletSettings>("BulletSettings");
            Container.BindInstance(_bulletSettings).AsSingle();
        }



    }
}
