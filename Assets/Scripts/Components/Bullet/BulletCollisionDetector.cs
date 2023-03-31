using System;
using System.Collections;
using System.Collections.Generic;
using Components.Enemies;
using Data.MetaData;
using Events.External;
using UnityEngine;
using Zenject;
using Enums;

public class BulletCollisionDetector : MonoBehaviour, IPoolType
{
    [Inject] private PlayerEvents PlayerEvents { get; set; }
    [Inject] private PoolSignals PoolSignals { get; set; }

    private Settings _mySettings;

    [Inject]
    public void Constractor(BulletSettings bulletSettings)
    {
        _mySettings = bulletSettings.BulletCollisionDetectorSettings;
    }

    private void OnEnable()
    {
        StartCoroutine(BulletLifeTime());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable attackable))
        {
            PlayerEvents.onEnemyShooted?.Invoke(attackable);
            DespawnSignal();
            attackable.OnWeaponTriggerEnter();
            GameObject particle = PoolSignals.onGetObject(PoolEnums.Particle, transform.position);
            particle.SetActive(false);
            particle.transform.position = transform.position;
            //particle.transform.LookAt(targetPos);
            //particle.transform.eulerAngles = new Vector3(0, bullet.transform.eulerAngles.y, bullet.transform.eulerAngles.z);
            particle.SetActive(true);
        }
    }

    private IEnumerator BulletLifeTime()
    {
        yield return new WaitForSeconds(_mySettings.BulletLifeTime);
        DespawnSignal();
    }

    private void DespawnSignal()
    {
        PoolSignals.onRemove(PoolEnums.Bullet, this);
        gameObject.SetActive(false);
    }

    [Serializable]
    public class Settings
    {
        [SerializeField] public float BulletLifeTime = 1f;
    }

    public class Pool : MemoryPool<Vector2, BulletCollisionDetector>, IPool
    {
        public void Despawn(IPoolType enemy)
        {
            base.Despawn((BulletCollisionDetector) enemy);
        }
        public new GameObject Spawn(Vector2 pos)
        {
            return base.Spawn(pos).gameObject;
        }
    }
}
