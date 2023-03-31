using Components.Enemies;
using Enums;
using Events.External;
using Installers.Prefabs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolManager
{
    public Enemy.Pool EnemyPool;
    public BulletCollisionDetector.Pool BulletPool;
    [SerializeField] private Dictionary<PoolEnums, IPool> poolDictionary;
    [Inject] private PoolSignals poolSignals { get; set; }

public PoolManager(Enemy.Pool enemyPool, BulletCollisionDetector.Pool bulletPool)
    {
        poolDictionary = new Dictionary<PoolEnums, IPool>();
        EnemyPool = enemyPool;
        BulletPool = bulletPool;

        InitializePool(PoolEnums.Enemy, enemyPool);
        InitializePool(PoolEnums.Bullet, bulletPool);
    }
    private void InitializePool(PoolEnums type, IPool pool, int size = 0)
    {
        poolDictionary.Add(type, pool);
    }


    public GameObject Spawn(PoolEnums poolEnum, Vector2 spawnPos)
    {
        return poolDictionary[poolEnum].Spawn(spawnPos);
    }

    public void Remove(PoolEnums poolEnum, IPoolType enemy)
    {
        poolDictionary[poolEnum].Despawn(enemy);
    }

    // Pool içerisindeki obje sayýsýný döner.
    public int GetNum()
    {
        return EnemyPool.NumTotal;
    }
    public void Reset()
    {
        EnemyPool.Clear();
    }

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        poolSignals.onGetObject += Spawn;
        poolSignals.onRemove += Remove;
    }

    private void UnsubscribeEvents()
    {
        poolSignals.onGetObject += Spawn;
        poolSignals.onRemove += Remove;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

}