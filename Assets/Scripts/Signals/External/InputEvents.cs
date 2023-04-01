using UnityEngine;
using UnityEngine.Events;

namespace Events.External
{
    public class InputEvents
    {
        public UnityAction onInputBegin;
        public UnityAction<InputUpdate> onInputUpdate;
        public UnityAction onInputEnd;
        
        public readonly struct InputUpdate
        {
            public readonly Vector3 TerrainPos;
        
            public InputUpdate(Vector3 terrainPos) {
                TerrainPos = terrainPos;
            }
        }
    }
}