using UnityEngine;

public abstract class IState
{
    protected FSM fsm;// ��ǰ״̬��
    protected Parameter parameter;// ����
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}