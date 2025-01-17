using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class AIJustMoveForward : Action
{
    private EnemyMovementController _enemyMovementController;
    public override void OnAwake()
    {
        _enemyMovementController = GetComponent<EnemyMovementController>();
    }

    public override TaskStatus OnUpdate()
    {
        if(ExpandClass.GetTransformDistance(this.transform,EnemyManager.Instance().GetPlayer()) > 3f)
        {
            _enemyMovementController.SetapplyMotion(true);
            _enemyMovementController.SetAnimation(0f, 1f);
            return TaskStatus.Running;
        }
        return TaskStatus.Success;
    }
}
