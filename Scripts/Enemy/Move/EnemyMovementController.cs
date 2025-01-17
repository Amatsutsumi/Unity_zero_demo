using UnityEngine;

//提供给外部的方法是SetAnimation，控制速度
public class EnemyMovementController : CharacterMoveBase
{
    private bool _applyMotion;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        SetapplyMotion(true);
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        LookTarget(EnemyManager.Instance().GetPlayer());
    }

    private void LookTarget(Transform target)
    {
        if(animator.AnimationAtTag("Move"))
        {
            ExpandClass.RotateTowards(this.transform, target);
        }
    }

    public void SetAnimation(float horizontal,float vertical)
    {
        if (_applyMotion)
        {
            animator.SetFloat("Hspeed", horizontal);
            animator.SetFloat("Vspeed",vertical);
            animator.SetFloat("Lock", 1f);
        }
        else
        {
            animator.SetFloat("Hspeed", 0f);
            animator.SetFloat("Vspeed", 0f);
            animator.SetFloat("Lock", 0f);
        }
    }

    public void SetapplyMotion(bool set)
    {
        _applyMotion = set;
    }
}
