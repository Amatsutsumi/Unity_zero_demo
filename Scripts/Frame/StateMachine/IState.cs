using UnityEngine;

public abstract class IState
{
    protected FSM fsm;// 当前状态机
    protected Parameter parameter;// 参数
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}