using Components.Enemies;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Events.External
{
    [UsedImplicitly]
    public class PlayerEvents
    {
        public UnityAction<Vector3> onPlayerMove;
        public UnityAction<IAttackable> onEnemyShooted;
    }
}