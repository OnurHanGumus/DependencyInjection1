using Events.External;
using Events.InternalEvents;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Enums;
namespace Components.Enemies {
    public class EnemyPhysicsController : MonoBehaviour, IAttackable
    {
        private int _enemyHits = 2;
        [Inject] private PoolSignals PoolSignals { get; set;}
        [Inject] private EnemyInternalEvents EnemyInternalEvents { get; set; }
        [SerializeField] private EnemyManager enemy;

        public EnemyInternalEvents GetInternalEvents()
        {
            return EnemyInternalEvents;
        }

        private void OnDisable()
        {
            _enemyHits = 2;
        }
        void IAttackable.OnWeaponTriggerEnter()
        {
            _enemyHits--;

            if (_enemyHits == 0)
            {
                EnemyInternalEvents.OnDeath?.Invoke(this);
                //PoolSignals.onRemove?.Invoke(PoolEnums.Enemy, enemy);
                enemy.gameObject.SetActive(false);
            }
        }
    }
}