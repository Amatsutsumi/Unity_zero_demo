using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using System.Collections;

public class AIFreeMoveACtion : Action
{
    private EnemyMovementController moveController;
    private EnemyCombatController combatController;
    private int FreeMoveIndex;
    private int lastIndex;
    private float actionTimer = 2f;
    public override void OnAwake()
    {
        moveController = this.GetComponent<EnemyMovementController>();
        combatController = this.GetComponent<EnemyCombatController>();
        FreeMoveIndex = Random.Range(0, 3);
    }

    public override TaskStatus OnUpdate()
    {
        //接收到攻击指令就结束了
        if (combatController.GetAttackCommand())
            return TaskStatus.Success;
        else
        {
            //时间管理器，让10秒后自动设置为攻击
            if(ExpandClass.GetTransformDistance(this.transform,EnemyManager.Instance().GetPlayer())  > 8f)
            {
                moveController.SetAnimation(0f, 1f);
            }
            if(ExpandClass.GetTransformDistance(this.transform, EnemyManager.Instance().GetPlayer()) < 8f && ExpandClass.GetTransformDistance(this.transform, EnemyManager.Instance().GetPlayer()) > 3f)
            {
                FreeMove();
                UpdateIndex();
            }
            if (ExpandClass.GetTransformDistance(this.transform, EnemyManager.Instance().GetPlayer()) < 3f && ExpandClass.GetTransformDistance(this.transform, EnemyManager.Instance().GetPlayer()) > 0f)
            {
                moveController.SetAnimation(-1f, -1f);
            }

            return TaskStatus.Running;
        }
    }

    private void FreeMove()
    {
        switch (FreeMoveIndex)
        {
            case 0:
                moveController.SetAnimation(-1f, 0f);
                break;
            case 1:
                moveController.SetAnimation(0f, 0f);
                break;
            case 2:
                moveController.SetAnimation(1f,0f);
                break;

        }

    }

    private void UpdateIndex()
    {
        actionTimer -= Time.deltaTime;
        if (actionTimer < 0f)
        {
            UpdateTwo();
        }
    }

    private void UpdateTwo()
    {
        lastIndex = FreeMoveIndex;
        FreeMoveIndex = Random.Range(0, 3);
        if (FreeMoveIndex == lastIndex)
        {
            FreeMoveIndex = Random.Range(0, 3);
        }
        actionTimer = 2f;
    }
}
