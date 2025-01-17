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
    //我们需要的参数
    public Parameter parameter;

    protected virtual void Awake()
    {
        //注册方法
    }

    protected virtual void OnEnable()
    {
        parameter.animator = transform.GetComponent<Animator>();  // 获取角色上的动画控制器组件
        TransformState(StateType.idle);    // 初始状态为Idle
        currentIState.Enter();
    }

    //添加状态
    public virtual void AddState(StateType state, IState istate)
    {
        if (states.ContainsKey(state))
        {
            Debug.Log("请勿重复添加状态");
            return;
        }
        states.Add(state, istate);
    }
    //转换状态
    public virtual void TransformState(StateType state)
    {
        if (!states.ContainsKey(state))
        {
            Debug.Log("没有找到此状态");
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
        parameter.animatorStateInfo = parameter.animator.GetCurrentAnimatorStateInfo(0);// 获取当前动画状态信息
        currentIState.Update();
    }
}
