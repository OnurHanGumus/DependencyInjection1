using Events.External;
using Zenject;
using UnityEngine;
using Data.MetaData;
using Signals;

namespace Installers.Scenes
{
    public class MainSceneInstallerPoyrazHoca : MonoInstaller<MainSceneInstallerPoyrazHoca>
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject explosionPrefab;
        private BulletSettings _bulletSettings;
        private EnemySpawnSettings _enemySpawnSettings;
        public override void InstallBindings()
        {
            BindComponents();
            BindSettings();
        }

        void BindComponents()
        {
            Container.Bind<CoreGameSignals>().AsSingle();
            Container.Bind<InputSignalsPoyrazHoca>().AsSingle();
            Container.Bind<LevelSignals>().AsSingle();
            Container.Bind<UISignals>().AsSingle();
            Container.Bind<ScoreSignals>().AsSingle();
            Container.Bind<SaveSignals>().AsSingle();
            Container.Bind<PoolSignals>().AsSingle();
            Container.Bind<AudioSignals>().AsSingle();
            Container.Bind<PlayerSignals>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemySpawnManager>().AsSingle();

            Container.BindFactory<BulletManager, BulletManager.Factory>().FromComponentInNewPrefab(bulletPrefab);
            Container.BindFactory<EnemyManager, EnemyManager.Factory>().FromComponentInNewPrefab(enemyPrefab);
            Container.BindFactory<ExplosionManager, ExplosionManager.Factory>().FromComponentInNewPrefab(explosionPrefab);            
        }

        private void BindSettings()
        {
            _bulletSettings = Resources.Load<BulletSettings>("BulletSettings");
            Container.BindInstance(_bulletSettings).AsSingle();

            _enemySpawnSettings = Resources.Load<EnemySpawnSettings>("EnemySpawnSettings");
            Container.BindInstance(_enemySpawnSettings).AsSingle();
        }



    }
}
