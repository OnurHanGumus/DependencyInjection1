using System.Collections.Generic;
using Components.Enemies;
using Events.External;
using UnityEngine;
using Zenject;

public class BulletCollisionDetector : MonoBehaviour
{
    [Inject] private PlayerEvents PlayerEvents { get; set; }
    private void Start()
    {
        Debug.Log(PlayerEvents);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable attackable))
        {
            PlayerEvents.onEnemyShooted?.Invoke(attackable);

            
            attackable.OnWeaponTriggerEnter();
        }
    }

    public class Pool : MemoryPool<Vector2, BulletCollisionDetector>
    {

    }
}
