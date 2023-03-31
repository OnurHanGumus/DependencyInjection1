using Events.External;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Enums;
namespace Components.Enemies {
    public class EnemyCollisionDetector : MonoBehaviour, IAttackable
    {
        private int _enemyHits = 2;
        [Inject] private PoolSignals PoolSignals;
        [SerializeField] private Enemy enemy;
        private void OnDisable()
        {
            _enemyHits = 2;
        }
        void IAttackable.OnWeaponTriggerEnter()
        {
            _enemyHits--;

            if (_enemyHits == 0)
            {
                OnDeath?.Invoke(this);
                PoolSignals.onRemove?.Invoke(PoolEnums.Enemy, enemy);
                enemy.gameObject.SetActive(false);
            }
        }

        public UnityAction<IAttackable> OnDeath { get; set; }
    }
}