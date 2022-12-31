using Installers.Prefabs;
using UnityEngine;

public class EnemyPool
{
    private Enemy.Pool _pool;

    public EnemyPool(Enemy.Pool pool)
    {
        _pool = pool;
    }

    public Enemy SpawnEnemy(Vector2 spawnPos)
    {
        return _pool.Spawn(spawnPos);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _pool.Despawn(enemy);
    }

    // Pool içerisindeki obje sayýsýný döner.
    public int GetNum()
    {
        return _pool.NumTotal;
    }
    public void Reset()
    {
        _pool.Clear();
    }
}