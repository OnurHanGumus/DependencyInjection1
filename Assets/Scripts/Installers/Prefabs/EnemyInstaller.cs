using Data.MetaData;
using UnityEngine;
using Zenject;

namespace Installers.Prefabs
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        private EnemySettings _enemySettings;

        public override void InstallBindings()
        {
            _enemySettings = Resources.Load<EnemySettings>("EnemySettings");

            Container.BindInstance(_enemySettings).AsSingle();
        }
    }
}