
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboController : CharacterCombatBase
{

    [SerializeField, Header("敌人检测范围球半径")] private float DetectRange = 3f;
    //终结技列表
    [SerializeField,Header("终结技")]private List<ComboData> FinishCombo;

    private void Update()
    {
        base.Update();
        SetEnemy();
    }
    private void LateUpdate()
    {
        base.LateUpdate();
        Attack();
        FinishExcute();
    }


    #region 左右键攻击
    private void Attack()
    {
        if(!CanAttack()) return;
        if(GameInputManager.Instance().leftFire)
        {
            ExcuteAttack();
        }
        if(GameInputManager.Instance().RightFire)
        {
            if(currentCombo.BranchCombo == null) return;
            currentCombo = currentCombo.BranchCombo;
            ExcuteAttack();
        }
    }


    #endregion

    #region 敌人判定

    //得到敌人
    private void SetEnemy()
    {
        Collider[] hits =  Physics.OverlapSphere(this.transform.position, DetectRange, 1 << 7, QueryTriggerInteraction.Ignore);
        if (hits.Length > 0) // 检查数组是否有元素
        {
            currentEnemy = hits[0].transform;
            for (int i = 0; i < hits.Length; i++)
            {
                if (ExpandClass.GetTransformDistance(currentEnemy, this.transform) > ExpandClass.GetTransformDistance(hits[i].transform, this.transform))
                {
                    currentEnemy = hits[i].transform;
                }
            }
            enemyHealth = currentEnemy.GetComponent<EnemyHealth>();
            Debug.Log("检测到敌人");
        }
        else
        {
            currentEnemy = null; // 或者设置为默认值，或者执行其他逻辑
        }

    }

    //执行处决
    private void FinishExcute()
    {
        if (!CanFinish()) return;
        //按下F键打终结技
        if (GameInputManager.Instance().execute)
        {
            Debug.Log("可以打终结技了");
            Debug.Log("我已经按下终结技");
            currentCombo = FinishCombo[Random.Range(0,FinishCombo.Count)];
            Debug.Log("党倩终结技名称" + currentCombo.ComboName);
            //动作匹配
            animator.CrossFade(currentCombo.ComboName, 0.03f);
            MatchTarget();
            GameEventManager.Instance().InvokeEvent("FinishCombo", currentCombo.ComboDamage[0].hitName,currentEnemy,this.transform);
        }
    }

    #endregion
}
