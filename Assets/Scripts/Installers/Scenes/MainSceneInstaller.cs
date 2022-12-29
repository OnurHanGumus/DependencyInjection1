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
            Container.Bind<MainSceneInputEvents>().AsSingle();
            Container.Bind<PlayerEvents>().AsSingle();
            Container.Bind<PoolSignals>().AsSingle();
            
        }


    }
}
