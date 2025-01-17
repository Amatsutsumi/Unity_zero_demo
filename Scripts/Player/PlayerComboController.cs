
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboController : CharacterCombatBase
{

    [SerializeField, Header("���˼�ⷶΧ��뾶")] private float DetectRange = 3f;
    //�սἼ�б�
    [SerializeField,Header("�սἼ")]private List<ComboData> FinishCombo;

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


    #region ���Ҽ�����
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

    #region �����ж�

    //�õ�����
    private void SetEnemy()
    {
        Collider[] hits =  Physics.OverlapSphere(this.transform.position, DetectRange, 1 << 7, QueryTriggerInteraction.Ignore);
        if (hits.Length > 0) // ��������Ƿ���Ԫ��
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
            Debug.Log("��⵽����");
        }
        else
        {
            currentEnemy = null; // ��������ΪĬ��ֵ������ִ�������߼�
        }

    }

    //ִ�д���
    private void FinishExcute()
    {
        if (!CanFinish()) return;
        //����F�����սἼ
        if (GameInputManager.Instance().execute)
        {
            Debug.Log("���Դ��սἼ��");
            Debug.Log("���Ѿ������սἼ");
            currentCombo = FinishCombo[Random.Range(0,FinishCombo.Count)];
            Debug.Log("��ٻ�սἼ����" + currentCombo.ComboName);
            //����ƥ��
            animator.CrossFade(currentCombo.ComboName, 0.03f);
            MatchTarget();
            GameEventManager.Instance().InvokeEvent("FinishCombo", currentCombo.ComboDamage[0].hitName,currentEnemy,this.transform);
        }
    }

    #endregion
}
