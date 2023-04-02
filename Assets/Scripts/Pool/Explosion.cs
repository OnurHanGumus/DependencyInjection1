using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Events.External;
using Enums;
public class Explosion : MonoBehaviour, IPoolType
{
    [Inject] private PoolSignals PoolSignals { get; set; }

    private void OnDisable()
    {
        PoolSignals.onRemove(PoolEnums.Particle, this);
    }
    public class Pool : MemoryPool<Vector2, Explosion>, IPool
    {
        public void Despawn(IPoolType explosion)
        {
            base.Despawn((Explosion) explosion);
        }

        public void DisableAll()
        {
            base.GetInternal().gameObject.SetActive(false);
        }

        public new GameObject Spawn(Vector2 pos)
        {
            return base.Spawn(pos).gameObject;
        }
    }
}
