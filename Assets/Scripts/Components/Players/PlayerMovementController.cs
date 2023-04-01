using Data.MetaData;
using Events.External;
using Extensions.Unity;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Components.Players
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Transform _myTransform;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [Inject] private InputEvents MainSceneInputEvents { get; set; }
        [Inject] private PlayerEvents PlayerEvents { get; set; }

        private RoutineHelper _onPosUpdate;
        [Inject] private PlayerSettings PlayerSettings { get; set; }

        private Settings _mySettings;
        private void OnEnable()
        {
            RegisterEvents();

            _onPosUpdate = new RoutineHelper
            (this, null, OnPosUpdate, () => true);
        }
        private void Awake()
        {
            _mySettings = PlayerSettings.PlayerMovementSettings;
        }
        private void OnPosUpdate()
        {
            PlayerEvents.onPlayerMove?.Invoke(_myTransform.position);

            if (_navMeshAgent.isStopped)
            {
                _onPosUpdate.StopCoroutine();
            }
        }

        private void OnDisable()
        {
            UnRegisterEvents();
        }

        private void RegisterEvents()
        {
            MainSceneInputEvents.onInputUpdate += OnInputUpdate;
            PlayerEvents.onAttackedToEnemy += OnAttackToEnemy;
        }

        private void OnInputUpdate(InputEvents.InputUpdate inputUpdate)
        {
            if (_onPosUpdate.IsInvoking == false)
            {
                _navMeshAgent.isStopped = false;

                _onPosUpdate.StartCoroutine();
            }

            //Debug.LogWarning(MethodBase.GetCurrentMethod().Name);

            _navMeshAgent.destination = inputUpdate.TerrainPos;
            _navMeshAgent.speed = _mySettings.Speed;
        }

        private void OnAttackToEnemy(Vector3 targetPos)
        {
            _navMeshAgent.isStopped = true;
            transform.LookAt(targetPos);
        }

        private void UnRegisterEvents()
        {
            MainSceneInputEvents.onInputUpdate -= OnInputUpdate;
            PlayerEvents.onAttackedToEnemy -= OnAttackToEnemy;
        }
        [Serializable]
        public class Settings
        {
            [SerializeField] public float Speed = 1f;
        }
    }
}