using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterMoveBase : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField, Header("�����İ뾶")] private float _detectRange;
    [SerializeField, Header("������ƫ����")] private float _detectOffset;
    [SerializeField, Header("���㼶")] private LayerMask _ground;
    [SerializeField, Header("��������ٶ�")] private float _maxVelocitySpeed = -30f;
    [SerializeField, Header("����ʱ����ֵ")] private float _fallOutTime = 0.2f;
    private float _fallOutDeltaTime;
    private const float _gravity = -9.8f;
    //�����ٶ�
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
    //����Ƿ��ڵ�����
    private bool CheckOnGround()
    {
        if (Physics.CheckSphere(this.transform.position + this.transform.up * _detectOffset, _detectRange, (int)_ground, QueryTriggerInteraction.Ignore))
            return true;
        return false;
    }

    //�����߼�
    private void Gravity()
    {
        if(CheckOnGround())
        {
            //���½�ɫ�ٶ�
            _fallOutDeltaTime = _fallOutTime;
            _velocitySpeed = -2f;
        }
        else
        {
            _fallOutDeltaTime -= Time.deltaTime;
            if(_fallOutDeltaTime < 0 )
            {
                //�������䶯��

                //���½�ɫ�ٶ�
                _velocitySpeed += Time.deltaTime * _gravity;
                _velocitySpeed = Mathf.Min(_velocitySpeed,_maxVelocitySpeed);
            }
        }
    }

    //�����ƶ�
    private void GravityMove()
    {
        controller.SimpleMove(this.transform.up * _velocitySpeed);
    }

    //б�����߼��Ͳ���д�ˣ�ֱ�����Դ���
    //����������
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position + this.transform.up * _detectOffset, _detectRange);
    }
}
