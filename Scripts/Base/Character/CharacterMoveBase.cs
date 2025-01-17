using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterMoveBase : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField, Header("检测球的半径")] private float _detectRange;
    [SerializeField, Header("检测球的偏移量")] private float _detectOffset;
    [SerializeField, Header("检测层级")] private LayerMask _ground;
    [SerializeField, Header("最大下落速度")] private float _maxVelocitySpeed = -30f;
    [SerializeField, Header("下落时间检测值")] private float _fallOutTime = 0.2f;
    private float _fallOutDeltaTime;
    private const float _gravity = -9.8f;
    //下落速度
    private float _velocitySpeed;
    private bool _isOnGround;

    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    protected virtual void Update()
    {
        CheckOnGround();
        Gravity();
        GravityMove();
    }
    //检测是否在地面上
    private bool CheckOnGround()
    {
        if (Physics.CheckSphere(this.transform.position + this.transform.up * _detectOffset, _detectRange, (int)_ground, QueryTriggerInteraction.Ignore))
            return true;
        return false;
    }

    //重力逻辑
    private void Gravity()
    {
        if(CheckOnGround())
        {
            //更新角色速度
            _fallOutDeltaTime = _fallOutTime;
            _velocitySpeed = -2f;
        }
        else
        {
            _fallOutDeltaTime -= Time.deltaTime;
            if(_fallOutDeltaTime < 0 )
            {
                //播放下落动画

                //更新角色速度
                _velocitySpeed += Time.deltaTime * _gravity;
                _velocitySpeed = Mathf.Min(_velocitySpeed,_maxVelocitySpeed);
            }
        }
    }

    //重力移动
    private void GravityMove()
    {
        controller.SimpleMove(this.transform.up * _velocitySpeed);
    }

    //斜面检测逻辑就不用写了，直接用自带的
    //画出重力球
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position + this.transform.up * _detectOffset, _detectRange);
    }
}
