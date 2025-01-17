using UnityEngine;

public abstract class CharacterCombatBase : MonoBehaviour
{
    protected Animator animator;
    protected bool canAttack = true;
    protected CharacterHealthBase enemyHealth;
    //������ʽ
    [SerializeField] protected ComboData firstCombo;
    protected ComboData currentCombo;
    //��ǰ����
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

    //�ж��Ƿ���Թ���
    protected bool CanAttack()
    {
        if (canAttack == false) return false;
        if (animator.AnimationAtTag("Hit")) return false;
        if (animator.AnimationAtTag("Parry")) return false;
        if (animator.AnimationAtTag("Finish")) return false;
        return true;
    }
    //������ʽ
    protected void ResetCombo()
    {
        if (!animator.AnimationAtTag("Attack"))
            currentCombo = firstCombo;
        else return;
    }
    //ִ����ʽ
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

    //�������,�ڶ����¼�����
    protected void ATK(int index)
    {
        if (currentEnemy == null) return;
        //������˺��Ҿ���С��һ��ֵ���Ҽн�С��һ��ֵ
        if (ExpandClass.GetTransformDistance(currentEnemy, this.transform) < 1.5f && Vector3.Dot(ExpandClass.GetTransformVector3(currentEnemy, this.transform).normalized, this.transform.forward.normalized) > 0.6f)
        {
            Debug.Log("��ʼ��");
            Debug.Log("�ڹ���״̬����" + animator.AnimationAtTag("Attack"));
            //�����˺��ж�
            if (animator.AnimationAtTag("Attack"))
            {
                Debug.Log("��������");
                GameEventManager.Instance().InvokeEvent<float, string, string, Transform, Transform>("DamageTrigger", currentCombo.ComboDamage[index].damage, currentCombo.ComboDamage[index].hitName, currentCombo.ComboDamage[index].parryName, currentEnemy, this.transform);
            }
        }
    }

    //ֻ�й���ʱ��������
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

    //�ܷ񴦾����о����ԸĽ�
    protected bool CanFinish()
    {
        if (currentEnemy == null) return false;
        if (animator.AnimationAtTag("Finish")) return false;
        if (!enemyHealth.CanFinishExcute()) return false;
        if (ExpandClass.GetTransformDistance(currentEnemy, this.transform) > 2f) return false;

        return true;
    }

    //����ƥ��
    protected void MatchTarget()
    {
        if (animator.AnimationAtTag("Finish"))
        {
            animator.MatchTarget(currentEnemy.transform.position + (-this.transform.forward) * 0.1f, Quaternion.identity, AvatarTarget.Body, new MatchTargetWeightMask(Vector3.one, 0f), 0f, 0.03f);
        }
        return;
    }
}
