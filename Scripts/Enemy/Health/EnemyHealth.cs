using UnityEngine;

public class EnemyHealth : CharacterHealthBase
{
    protected override void GetDamage(float damage, string hitNmae, string parryName, Transform Enemy, Transform _attacker)
    {
        //敌人不是自己就不要攻击
        if (Enemy != this.transform) return;
        //设置敌人
        SetAttacker(_attacker);

        if (damage > 30f || nowPower <= 1f)
        {
            nowHP -= damage;
            //播放受击动画
            animator.Play(hitNmae);
        }
        else
        {
            nowPower -= damage;
            //播放格挡动画
            animator.Play(parryName);
        }
    }
}
