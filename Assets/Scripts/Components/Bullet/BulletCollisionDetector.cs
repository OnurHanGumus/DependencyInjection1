using System.Collections.Generic;
using Components.Enemies;
using Events.External;
using UnityEngine;
using Zenject;

public class BulletCollisionDetector : MonoBehaviour
{
    [InjectAttribute] private PlayerEvents PlayerEvents { get; set; }
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


}
