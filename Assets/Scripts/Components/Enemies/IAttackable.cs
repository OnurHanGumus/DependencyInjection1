using Events.InternalEvents;
using UnityEngine.Events;

namespace Components.Enemies
{
    public interface IAttackable
    {
        void OnWeaponTriggerEnter();
        UnityAction<IAttackable> OnDeath { get; set; }
    }
}