using System;
using System.Collections;
using System.Collections.Generic;
using Components.Enemies;
using Data.MetaData;
using Events.External;
using UnityEngine;
using Zenject;

public class BulletCollisionDetector : MonoBehaviour
{
    [Inject] private PlayerEvents PlayerEvents { get; set; }
    [Inject] private PoolSignals PoolSignals { get; set; }
    //[Inject] private BulletSettings BulletSettings { get; set; }

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

    private void Start()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable attackable))
        {
            PlayerEvents.onEnemyShooted?.Invoke(attackable);
            DespawnSignal();
            attackable.OnWeaponTriggerEnter();
            //gameObject.SetActive(false);
        }
    }
    private IEnumerator BulletLifeTime()
    {
        yield return new WaitForSeconds(_mySettings.BulletLifeTime);
        DespawnSignal();
    }
    private void DespawnSignal()
    {
        PoolSignals.onRemove(this);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        
    }
    [Serializable]
    public class Settings
    {
        [SerializeField] public float BulletLifeTime = 1f;
    }

    public class Pool : MemoryPool<Vector2, BulletCollisionDetector>
    {
        
    }
}
