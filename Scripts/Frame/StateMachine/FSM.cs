using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    idle,
    stand,
    run,
    attack
}
public class FSM : MonoBehaviour
{
    private IState currentIState;
    protected Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    //������Ҫ�Ĳ���
    public Parameter parameter;

    protected virtual void Awake()
    {
        //ע�᷽��
    }

    protected virtual void OnEnable()
    {
        parameter.animator = transform.GetComponent<Animator>();  // ��ȡ��ɫ�ϵĶ������������
        TransformState(StateType.idle);    // ��ʼ״̬ΪIdle
        currentIState.Enter();
    }

    //���״̬
    public virtual void AddState(StateType state, IState istate)
    {
        if (states.ContainsKey(state))
        {
            Debug.Log("�����ظ����״̬");
            return;
        }
        states.Add(state, istate);
    }
    //ת��״̬
    public virtual void TransformState(StateType state)
    {
        if (!states.ContainsKey(state))
        {
            Debug.Log("û���ҵ���״̬");
            return;
        }
        if (currentIState != null)
        {
            currentIState.Exit();
        }
        currentIState = states[state];
        currentIState.Enter();
    }
    public void Update()
    {
        parameter.animatorStateInfo = parameter.animator.GetCurrentAnimatorStateInfo(0);// ��ȡ��ǰ����״̬��Ϣ
        currentIState.Update();
    }
}
