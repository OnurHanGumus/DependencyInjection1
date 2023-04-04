using Events.External;
using Zenject;
using UnityEngine;
using Data.MetaData;

namespace Installers.Scenes
{
    public class MainSceneInstaller : MonoInstaller<MainSceneInstaller>
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject explosionPrefab;
        private BulletSettings _bulletSettings;

        public override void InstallBindings()
        {
            BindComponents();
            BindSettings();
        }

        void BindComponents()
        {

            Container.Bind<CoreGameSignals>().AsSingle();
            Container.Bind<InputSignals>().AsSingle();
            Container.Bind<PoolSignals>().AsSingle();
            Container.Bind<PlayerSignals>().AsSingle();

            //Container.BindMemoryPool<Bullet, Bullet.Pool>().WithInitialSize(5).FromComponentInNewPrefab(bullet).UnderTransformGroup("Bullets");
            //Container.BindMemoryPool<Enemy, Enemy.Pool>().FromComponentInNewPrefab(enemy);
            //Container.BindMemoryPool<Explosion, Explosion.Pool>().FromComponentInNewPrefab(explosionParticle).UnderTransformGroup("Particles");
            Container.BindFactory<BulletManager, BulletManager.Factory>().FromComponentInNewPrefab(bulletPrefab);
            Container.BindFactory<EnemyManager, EnemyManager.Factory>().FromComponentInNewPrefab(enemyPrefab);
            Container.BindFactory<ExplosionManager, ExplosionManager.Factory>().FromComponentInNewPrefab(explosionPrefab);
            //Container.Bind<PoolHolder>().AsSingle();
        }

        private void BindSettings()
        {
            _bulletSettings = Resources.Load<BulletSettings>("BulletSettings");
            Container.BindInstance(_bulletSettings).AsSingle();
        }



    }
}
