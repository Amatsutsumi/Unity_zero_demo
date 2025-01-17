using TMPro.EditorUtilities;
using UnityEngine;

public abstract class CharacterHealthBase : MonoBehaviour
{
    //声明我们角色的生命脚本
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
        //事件注册
        GameEventManager.Instance().AddEventListening<float,string,string,Transform,Transform>("DamageTrigger",GetDamage);
        GameEventManager.Instance().AddEventListening<string,Transform,Transform>("FinishCombo", FinishCombo);
    }
    protected virtual void Update()
    {
        isDead();
        UpdatePower();
        Dead();
    }
    
    //角色受击逻辑，玩家敌人逻辑不同需要重写
    protected virtual void GetDamage(float damage,string hitNmae,string parryName,Transform Enemy,Transform _attacker)
    {

    }
    //终结技，玩家敌人逻辑相同
    protected virtual void FinishCombo(string hitName,Transform self,Transform _attacker)
    {
        if(self != this.transform) return;
        SetAttacker(_attacker);
        ExpandClass.RotateTowards(this.transform, attacker);
        animator.Play(hitName);
        //直接打死
        nowHP -= 999f;
    }

    //判断死亡，玩家角色逻辑相同
    protected void isDead()
    {
        if (nowHP <= 0f)
            IsDead = true;
        else IsDead = false;
    }
    //设置敌人，逻辑相同
    protected void SetAttacker(Transform attack)
    {
        attacker = attack;
    }

    //体力值恢复，逻辑相同
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
                //硬直时间3秒
                TimerManager.Instance().TryGetOneTimer(healthData.HitTime,()=> nowPower += Time.deltaTime * 1f);
            }
        }
    }

    //死亡逻辑和硬值逻辑，想通
    protected void Dead()
    {
        if(!IsDead && nowPower <= 0f)
        {
            //播放硬值动画
        }
        if(IsDead)
        {
            nowHP = 0f;
            //播放死亡动画
            Debug.Log("我死了");
            //并销毁
        }
        return;
    }

    //提供外部，是否可以被终结
    public bool CanFinishExcute()
    {
        if (nowPower < 0f && !IsDead) return true;
        return false;
    }

}
