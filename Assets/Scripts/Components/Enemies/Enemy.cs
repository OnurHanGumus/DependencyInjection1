using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    public void OnDespawned()
    {
        throw new System.NotImplementedException();
    }

    public void OnSpawned()
    {
        throw new System.NotImplementedException();
    }

    public class Pool : MemoryPool<Vector2, Enemy>
    {

    }
}
