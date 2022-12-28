using UnityEngine;
using UnityEngine.Events;

namespace Events.External
{
    public class MainSceneInputEvents
    {
        public UnityAction onInputBegin;
        public UnityAction<InputUpdate> onInputUpdate;
        public UnityAction onInputEnd;

        public UnityAction<Vector3> onAttackedToEnemy;

        public readonly struct InputUpdate
        {
            public readonly Vector3 TerrainPos;
        
            public InputUpdate(Vector3 terrainPos) {
                TerrainPos = terrainPos;
            }
        }
    }
}