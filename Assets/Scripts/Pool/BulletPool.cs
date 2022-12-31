using UnityEngine;

public class BulletPool
{
    private BulletCollisionDetector.Pool _pool;

    public BulletPool(BulletCollisionDetector.Pool pool)
    {
        _pool = pool;
    }

    public BulletCollisionDetector SpawnBullet(Vector2 spawnPos)
    {
        return _pool.Spawn(spawnPos);
    }

    public void RemoveBullet(BulletCollisionDetector bullet)
    {
        _pool.Despawn(bullet);
    }

    // Pool i�erisindeki obje say�s�n� d�ner.
    public int GetNum()
    {
        return _pool.NumTotal;
    }
    public void Reset()
    {
        _pool.Clear();
    }
}