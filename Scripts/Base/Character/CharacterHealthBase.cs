using TMPro.EditorUtilities;
using UnityEngine;

public abstract class CharacterHealthBase : MonoBehaviour
{
    //�������ǽ�ɫ�������ű�
    public CharacterHealthData healthData;
    protected float nowHP;
    protected float nowPower;
    protected bool IsDead = false;

    protected Animator animator;
    protected Transform attacker;
    protected virtual void Awake()
    {
        nowHP = healthData.MaxHP;
        nowPower = healthData.MaxPower;
        animator = GetComponent<Animator>();
        //�¼�ע��
        GameEventManager.Instance().AddEventListening<float,string,string,Transform,Transform>("DamageTrigger",GetDamage);
        GameEventManager.Instance().AddEventListening<string,Transform,Transform>("FinishCombo", FinishCombo);
    }
    protected virtual void Update()
    {
        isDead();
        UpdatePower();
        Dead();
    }
    
    //��ɫ�ܻ��߼�����ҵ����߼���ͬ��Ҫ��д
    protected virtual void GetDamage(float damage,string hitNmae,string parryName,Transform Enemy,Transform _attacker)
    {

    }
    //�սἼ����ҵ����߼���ͬ
    protected virtual void FinishCombo(string hitName,Transform self,Transform _attacker)
    {
        if(self != this.transform) return;
        SetAttacker(_attacker);
        ExpandClass.RotateTowards(this.transform, attacker);
        animator.Play(hitName);
        //ֱ�Ӵ���
        nowHP -= 999f;
    }

    //�ж���������ҽ�ɫ�߼���ͬ
    protected void isDead()
    {
        if (nowHP <= 0f)
            IsDead = true;
        else IsDead = false;
    }
    //���õ��ˣ��߼���ͬ
    protected void SetAttacker(Transform attack)
    {
        attacker = attack;
    }

    //����ֵ�ָ����߼���ͬ
    protected virtual void UpdatePower()
    {
        if(IsDead) return;
        if(nowPower < 100f)
        {
            if(nowPower > 0f)
                nowPower += Time.deltaTime * 0.1f;
            if(nowPower <= 0f)
            {
                nowPower = 0f;
                //Ӳֱʱ��3��
                TimerManager.Instance().TryGetOneTimer(healthData.HitTime,()=> nowPower += Time.deltaTime * 1f);
            }
        }
    }

    //�����߼���Ӳֵ�߼�����ͨ
    protected void Dead()
    {
        if(!IsDead && nowPower <= 0f)
        {
            //����Ӳֵ����
        }
        if(IsDead)
        {
            nowHP = 0f;
            //������������
            Debug.Log("������");
            //������
        }
        return;
    }

    //�ṩ�ⲿ���Ƿ���Ա��ս�
    public bool CanFinishExcute()
    {
        if (nowPower < 0f && !IsDead) return true;
        return false;
    }

}
