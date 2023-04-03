using Enums;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Events.External
{
    public class PoolSignals
    {
        public Func<PoolEnums,Vector3, GameObject> onGetObject = delegate { return null; };
        public Func<Transform> onGetPoolManagerObj = delegate { return null; };
    }
}