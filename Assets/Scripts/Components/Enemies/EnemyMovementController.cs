using Data.MetaData;
using Events.External;
using Extensions.Unity;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Components.Players
{
    public class EnemyMovementController : MonoBehaviour
    {
        [SerializeField] private Transform _myTransform;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [Inject] private InputEvents MainSceneInputEvents { get; set; }
        [Inject] private PlayerEvents PlayerEvents { get; set; }

        private RoutineHelper _onPosUpdate;
        [Inject] private EnemySettings EnemySettings { get; set; }

        private Settings _mySettings;
        private void OnEnable()
        {
            RegisterEvents();
        }
        private void Awake()
        {
            _mySettings = EnemySettings.EnemyMovementSettings;
        }
        private void OnDisable()
        {
            UnRegisterEvents();
        }

        private void RegisterEvents()
        {
            PlayerEvents.onPlayerMove += OnPlayerMove;
        }

        private void UnRegisterEvents()
        {
            PlayerEvents.onPlayerMove += OnPlayerMove;
            _navMeshAgent.speed = _mySettings.Speed;
        }

        private void OnPlayerMove(Vector3 playerPos)
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
            _navMeshAgent.destination = playerPos;
        }
        [Serializable]
        public class Settings
        {
            [SerializeField] public float Speed = 1f;
        }
    }
}