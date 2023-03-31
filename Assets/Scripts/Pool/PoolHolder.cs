using Components.Enemies;
using Enums;
using Events.External;
using Installers.Prefabs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolHolder
{
    public Enemy.Pool EnemyPool;
    public Explosion.Pool ExplosionPool;
    public BulletCollisionDetector.Pool BulletPool;
    [SerializeField] public Dictionary<PoolEnums, IPool> PoolDictionary;

    public PoolHolder(Enemy.Pool enemyPool, BulletCollisionDetector.Pool bulletPool, Explosion.Pool explosionPool)
    {
        PoolDictionary = new Dictionary<PoolEnums, IPool>();
        EnemyPool = enemyPool;
        BulletPool = bulletPool;
        ExplosionPool = explosionPool;

        InitializePool(PoolEnums.Enemy, EnemyPool);
        InitializePool(PoolEnums.Bullet, BulletPool);
        InitializePool(PoolEnums.Particle, ExplosionPool);
    }
    private void InitializePool(PoolEnums type, IPool pool, int size = 0)
    {
        PoolDictionary.Add(type, pool);
    }


    public GameObject Spawn(PoolEnums poolEnum, Vector2 spawnPos)
    {
        return PoolDictionary[poolEnum].Spawn(spawnPos);
    }

    public void Remove(PoolEnums poolEnum, IPoolType type)
    {
        PoolDictionary[poolEnum].Despawn(type);
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


}