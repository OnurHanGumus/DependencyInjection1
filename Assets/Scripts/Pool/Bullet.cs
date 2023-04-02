using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Events.External;
using Enums;
public class Bullet : MonoBehaviour, IPoolType
{
    [Inject] private PoolSignals PoolSignals { get; set; }
    private void OnDisable()
    {
        PoolSignals.onRemove(PoolEnums.Bullet, this);
    }
    public class Pool : MemoryPool<Vector2, Bullet>, IPool
    {
        public void Despawn(IPoolType bullet)
        {
            base.Despawn((Bullet) bullet);
        }

        public new GameObject Spawn(Vector2 pos)
        {
            return base.Spawn(pos).gameObject;
        }
        public void DisableAll()
        {
            for (int i = 0; i < base.NumActive; i++)
            {
                base.GetInternal().gameObject.SetActive(false);
            }
        }
    }
}
