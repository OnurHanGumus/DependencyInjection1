using Enums;
using Events.External;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IPoolType
{
    [Inject] private PoolSignals PoolSignals { get; set; }

    private void OnDisable()
    {
        PoolSignals.onRemove?.Invoke(PoolEnums.Enemy, this);
    }
    public class Pool : MemoryPool<Vector2, Enemy>, IPool
    {
        public void Despawn(IPoolType enemy)
        {
            base.Despawn((Enemy) enemy);
        }

        public void DisableAll()
        {
            //base.GetInternal().gameObject.SetActive(false);
        }

        public new GameObject Spawn(Vector2 pos)
        {
            return base.Spawn(pos).gameObject;
        }
    }
}
