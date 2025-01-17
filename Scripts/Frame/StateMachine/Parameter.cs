using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Parameter
{
    //����������Ҫ�Ĳ���
    public Animator animator;
    public float attack;
    public NavMeshAgent nav;
    public AnimatorStateInfo animatorStateInfo;//��ǰ������Ϣ
    public float idleTime;
    public Transform targetObj;
}
