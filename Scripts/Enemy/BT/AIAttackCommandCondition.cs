using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class AIAttackCommandCondition : Conditional
{
    private EnemyCombatController _enemyCombatController;
    public override void OnAwake()
    {
        _enemyCombatController = GetComponent<EnemyCombatController>();
    }

    public override TaskStatus OnUpdate()
    {
        return(_enemyCombatController.GetAttackCommand()) ? TaskStatus.Success : TaskStatus.Failure;
    }
}
