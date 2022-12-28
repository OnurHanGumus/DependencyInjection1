using UnityEngine;
using UnityEngine.Events;

namespace Components.Enemies {
    public class EnemyCollisionDetector : MonoBehaviour, IAttackable
    {
        private int _enemyHits = 2;
        
        void IAttackable.OnWeaponTriggerEnter()
        {
            _enemyHits--;

            if (_enemyHits == 0)
            {
                OnDeath?.Invoke(this);
            }
        }

        public UnityAction<IAttackable> OnDeath { get; set; }
    }
}