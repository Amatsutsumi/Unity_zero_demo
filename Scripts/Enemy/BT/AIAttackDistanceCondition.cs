using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class AIAttackDistanceCondition : Conditional
{
    private EnemyCombatController _enemyCombatController;
    [SerializeField] private float _attackDistance;
    public override void OnAwake()
    {
        _enemyCombatController = GetComponent<EnemyCombatController>();
    }
    public override TaskStatus OnUpdate()
    {
        return (ExpandClass.GetTransformDistance(this.transform,EnemyManager.Instance().GetPlayer())) > _attackDistance ? TaskStatus.Failure : TaskStatus.Success;
    }
}
