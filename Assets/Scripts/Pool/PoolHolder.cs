using Components.Enemies;
using Enums;
using Events.External;
using Installers.Prefabs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolHolder
{
    public Dictionary<PoolEnums, IPool> PoolDictionary;

    private Enemy.Pool _enemyPool;
    private Explosion.Pool _explosionPool;
    private Bullet.Pool _bulletPool;

    public PoolHolder(Enemy.Pool enemyPool, Bullet.Pool bulletPool, Explosion.Pool explosionPool)
    {
        PoolDictionary = new Dictionary<PoolEnums, IPool>();
        _enemyPool = enemyPool;
        _bulletPool = bulletPool;
        _explosionPool = explosionPool;

        InitializePool(PoolEnums.Enemy, _enemyPool);
        InitializePool(PoolEnums.Bullet, _bulletPool);
        InitializePool(PoolEnums.Particle, _explosionPool);
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
        return _enemyPool.NumTotal;
    }
    public void Reset()
    {
        for (int i = 0; i < PoolDictionary.Keys.Count; i++)
        {
            //PoolDictionary[(PoolEnums) i].DisableAll();
            PoolDictionary[PoolEnums.Enemy].GetObject();
        }
    }


}