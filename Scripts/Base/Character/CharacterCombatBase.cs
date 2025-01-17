using UnityEngine;

public abstract class CharacterCombatBase : MonoBehaviour
{
    protected Animator animator;
    protected bool canAttack = true;
    protected CharacterHealthBase enemyHealth;
    //基础招式
    [SerializeField] protected ComboData firstCombo;
    protected ComboData currentCombo;
    //当前敌人
    protected Transform currentEnemy;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        currentCombo = firstCombo;
        enemyHealth = GetComponent<CharacterHealthBase>();
    }

    protected virtual void Update()
    {
        CanAttack();
        CanFinish();
    }

    protected virtual void LateUpdate()
    {
        LockEnemy();
        MatchTarget();
        ResetCombo();
    }

    //判断是否可以攻击
    protected bool CanAttack()
    {
        if (canAttack == false) return false;
        if (animator.AnimationAtTag("Hit")) return false;
        if (animator.AnimationAtTag("Parry")) return false;
        if (animator.AnimationAtTag("Finish")) return false;
        return true;
    }
    //重置招式
    protected void ResetCombo()
    {
        if (!animator.AnimationAtTag("Attack"))
            currentCombo = firstCombo;
        else return;
    }
    //执行招式
    protected void ExcuteAttack()
    {
        animator.CrossFade(currentCombo.ComboName, 0.05f);
        canAttack = false;
        TimerManager.Instance().TryGetOneTimer(currentCombo.ColdTime, UpdateCombo);
    }

    protected void UpdateCombo()
    {
        canAttack = true;
        currentCombo = currentCombo.NextCombo;
    }

    //攻击检测,在动画事件调用
    protected void ATK(int index)
    {
        if (currentEnemy == null) return;
        //如果敌人和我距离小于一定值并且夹角小于一定值
        if (ExpandClass.GetTransformDistance(currentEnemy, this.transform) < 1.5f && Vector3.Dot(ExpandClass.GetTransformVector3(currentEnemy, this.transform).normalized, this.transform.forward.normalized) > 0.6f)
        {
            Debug.Log("开始看");
            Debug.Log("在攻击状态下吗" + animator.AnimationAtTag("Attack"));
            //敌人伤害判定
            if (animator.AnimationAtTag("Attack"))
            {
                Debug.Log("被调用了");
                GameEventManager.Instance().InvokeEvent<float, string, string, Transform, Transform>("DamageTrigger", currentCombo.ComboDamage[index].damage, currentCombo.ComboDamage[index].hitName, currentCombo.ComboDamage[index].parryName, currentEnemy, this.transform);
            }
        }
    }

    //只有攻击时进行锁敌
    protected void LockEnemy()
    {
        if (currentEnemy == null) return;
        if (animator.AnimationAtTag("Attack") || animator.AnimationAtTag("Finish"))
        {
            Vector3 directionToEnemy = (currentEnemy.position - this.transform.position).normalized;
            Quaternion rotationToEnemy = Quaternion.LookRotation(new Vector3(directionToEnemy.x, 0, directionToEnemy.z));
            Quaternion targetRotate = Quaternion.Lerp(this.transform.rotation, rotationToEnemy, 0.1f);
            this.transform.rotation = targetRotate;
        }
        else return;
    }

    //能否处决，感觉可以改进
    protected bool CanFinish()
    {
        if (currentEnemy == null) return false;
        if (animator.AnimationAtTag("Finish")) return false;
        if (!enemyHealth.CanFinishExcute()) return false;
        if (ExpandClass.GetTransformDistance(currentEnemy, this.transform) > 2f) return false;

        return true;
    }

    //动画匹配
    protected void MatchTarget()
    {
        if (animator.AnimationAtTag("Finish"))
        {
            animator.MatchTarget(currentEnemy.transform.position + (-this.transform.forward) * 0.1f, Quaternion.identity, AvatarTarget.Body, new MatchTargetWeightMask(Vector3.one, 0f), 0f, 0.03f);
        }
        return;
    }
}
