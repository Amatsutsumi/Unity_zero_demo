using UnityEngine;

public class PlayerMovementController : CharacterMoveBase
{
    private Animator animator;
    private float movespeed;
    //平滑旋转
    private float currentVelocity;
    [SerializeField,Header("平滑旋转时间")]private float smoothTime = 50f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Move();
        PlayerRotate();
    }
    private bool HasInput()
    {
        if(GameInputManager.Instance().move != Vector2.zero) return true;
        return false;
    }

    private void Move()
    {
        animator.SetFloat("Lock", GameInputManager.Instance().Lock == true ? 1f : 0f);
        if (HasInput())
        {
            movespeed = GameInputManager.Instance().speed ? 3f : 1.4f;
            if(animator.GetFloat("Lock") == 1f)
            {
                animator.SetFloat("Hspeed", GameInputManager.Instance().move.x);
                animator.SetFloat("Vspeed", GameInputManager.Instance().move.y);
            }
        }        
        animator.SetBool("HasInput", HasInput());
        animator.SetFloat("Speed",movespeed,0.5f,0.5f);
    }

    //旋转逻辑
    private void PlayerRotate()
    {
        //有输入时才进行旋转
        if(HasInput() && animator.AnimationAtTag("Move"))
        {
            float targetY = Camera.main.transform.eulerAngles.y + Mathf.Atan2(GameInputManager.Instance().move.x,GameInputManager.Instance().move.y) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0f, targetY, 0f),Time.deltaTime * 40f);
        }
    }
}
