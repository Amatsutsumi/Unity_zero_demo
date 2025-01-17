using UnityEngine;

public class EnemyCombatController : CharacterCombatBase
{
    public bool attackCommand = false;

    private  void Start()
    {
        canAttack = true;
        currentEnemy = GameObject.FindWithTag("Player").transform;
    }

    protected override void Update()
    {
        base.Update();
    }
    private bool CheckAttackCommand()
    {
        if(animator.AnimationAtTag("Hit"))return false;
        if(animator.AnimationAtTag("Parry"))return false;
        if(animator.AnimationAtTag("Finish"))return false;
        return true;
    }
    //设置攻击
    public void SetAttack( bool attack)
    {
        if (!CheckAttackCommand())
        {
            attackCommand = false;
            return;
        }
        attackCommand = attack;
    }

    public bool GetAttackCommand()
    {
        return attackCommand;
    }

    //执行攻击
    public void AIExcuteAttack()
    {
        if(!CanAttack() || attackCommand == false) return;
        ExcuteAttack();
    }

    //当玩家接受攻击指令但被玩家打了
}
