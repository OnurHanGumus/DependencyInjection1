using Events.InternalEvents;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public interface IPool
{
    GameObject Spawn(Vector2 pos);
    void Despawn(IPoolType enemy);
    void GetObject();
}