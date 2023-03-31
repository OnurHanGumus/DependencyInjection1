using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Events.External;
using Enums;
public class Explosion : MonoBehaviour, IPoolType
{
    [Inject] private PoolSignals poolSignals { get; set; }

    private void OnDisable()
    {
        poolSignals.onRemove(PoolEnums.Particle, this);
    }
    public class Pool : MemoryPool<Vector2, Explosion>, IPool
    {
        public void Despawn(IPoolType explosion)
        {
            base.Despawn((Explosion) explosion);
        }

        public new GameObject Spawn(Vector2 pos)
        {
            return base.Spawn(pos).gameObject;
        }
    }
}
