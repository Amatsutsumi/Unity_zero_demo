using UnityEngine;

public class EnemyHealth : CharacterHealthBase
{
    protected override void GetDamage(float damage, string hitNmae, string parryName, Transform Enemy, Transform _attacker)
    {
        //���˲����Լ��Ͳ�Ҫ����
        if (Enemy != this.transform) return;
        //���õ���
        SetAttacker(_attacker);

        if (damage > 30f || nowPower <= 1f)
        {
            nowHP -= damage;
            //�����ܻ�����
            animator.Play(hitNmae);
        }
        else
        {
            nowPower -= damage;
            //���Ÿ񵲶���
            animator.Play(parryName);
        }
    }
}
