using UnityEngine;

public class BulletPool
{
    private BulletCollisionDetector.Pool _pool;

    public BulletPool(BulletCollisionDetector.Pool pool)
    {
        _pool = pool;
    }

    public BulletCollisionDetector SpawnEnemy(Vector2 spawnPos)
    {
        return _pool.Spawn(spawnPos);
    }

    public void RemoveEnemy(BulletCollisionDetector enemy)
    {
        _pool.Despawn(enemy);
    }

    // Pool i�erisindeki obje say�s�n� d�ner.
    public int GetNum()
    {
        return _pool.NumTotal;
    }
}