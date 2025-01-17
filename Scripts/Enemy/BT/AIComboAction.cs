using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class AIComboAction : Action
{
    private EnemyCombatController _enemyCombatController;
    public override void OnAwake()
    {
        base.OnAwake();
        _enemyCombatController = GetComponent<EnemyCombatController>();
    }

    public override TaskStatus OnUpdate()
    {
        if( _enemyCombatController.GetAttackCommand())
        {
            _enemyCombatController.AIExcuteAttack();
        }
        return TaskStatus.Success;
    }
}
