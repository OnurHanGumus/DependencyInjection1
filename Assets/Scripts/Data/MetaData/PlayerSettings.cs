using Components.Players;
using UnityEngine;

namespace Data.MetaData
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ZenjectExample/PlayerSettings", order = 0)]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] public PlayerCameraController.Settings PlayerCameraControllerSettings;
        [SerializeField] public PlayerShootManager.Settings PlayerShootManagerSettings;
    }
}