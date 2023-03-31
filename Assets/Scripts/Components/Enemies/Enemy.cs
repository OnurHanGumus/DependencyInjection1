using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IPoolType
{
    public void OnDespawned()
    {
        throw new System.NotImplementedException();
    }

    public void OnSpawned()
    {
        throw new System.NotImplementedException();
    }

    public class Pool : MemoryPool<Vector2, Enemy>, IPool
    {
        public void Despawn(IPoolType enemy)
        {
            base.Despawn((Enemy) enemy);
        }

        public GameObject Spawn(Vector2 pos)
        {
            return base.Spawn(pos).gameObject;
        }
    }
}
