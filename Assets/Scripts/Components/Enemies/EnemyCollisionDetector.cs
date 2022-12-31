using Events.External;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Components.Enemies {
    public class EnemyCollisionDetector : MonoBehaviour, IAttackable
    {
        private int _enemyHits = 2;
        [Inject] private PoolSignals PoolSignals;
        [SerializeField] private Enemy enemy;
        void IAttackable.OnWeaponTriggerEnter()
        {
            _enemyHits--;

            if (_enemyHits == 0)
            {
                OnDeath?.Invoke(this);
                PoolSignals.onRemoveEnemy?.Invoke(enemy);
            }
        }

        public UnityAction<IAttackable> OnDeath { get; set; }
    }
}