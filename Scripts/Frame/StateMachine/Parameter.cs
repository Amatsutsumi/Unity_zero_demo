using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Parameter
{
    //定义我们需要的参数
    public Animator animator;
    public float attack;
    public NavMeshAgent nav;
    public AnimatorStateInfo animatorStateInfo;//当前动画信息
    public float idleTime;
    public Transform targetObj;
}
