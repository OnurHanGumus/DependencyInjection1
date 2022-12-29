using Events.External;
using Extensions.Unity;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Components.Players
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Transform _myTransform;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [Inject] private MainSceneInputEvents MainSceneInputEvents { get; set; }
        [Inject] private PlayerEvents PlayerEvents { get; set; }

        private RoutineHelper _onPosUpdate;

        private void OnEnable()
        {
            RegisterEvents();

            _onPosUpdate = new RoutineHelper
            (this, null, OnPosUpdate, () => true);
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
            MainSceneInputEvents.onAttackedToEnemy += OnAttackToEnemy;
        }

        private void OnInputUpdate(MainSceneInputEvents.InputUpdate inputUpdate)
        {
            if (_onPosUpdate.IsInvoking == false)
            {
                _navMeshAgent.isStopped = false;

                _onPosUpdate.StartCoroutine();
            }

            //Debug.LogWarning(MethodBase.GetCurrentMethod().Name);

            _navMeshAgent.destination = inputUpdate.TerrainPos;
        }

        private void OnAttackToEnemy(Vector3 targetPos)
        {
            _navMeshAgent.isStopped = true;
            transform.LookAt(targetPos);
        }

        private void UnRegisterEvents()
        {
            MainSceneInputEvents.onInputUpdate -= OnInputUpdate;
            MainSceneInputEvents.onAttackedToEnemy -= OnAttackToEnemy;
        }
    }
}