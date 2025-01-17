using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour
{
    private event UnityAction AwakeAction;
    private event UnityAction StartAction;
    private event UnityAction UpdateAction;

    private void Awake()
    {
        if(AwakeAction != null)
            AwakeAction();
    }
    private void Start()
    {
        if(StartAction != null)
            StartAction();
    }

    private void Update()
    {
        if(UpdateAction != null)
            UpdateAction();
    }

    public void AddAwake(UnityAction action)
    {
        AwakeAction += action;
    }

    public void RemoveAwake(UnityAction action)
    {
        AwakeAction -= action;
    }

    public void AddStart(UnityAction action)
    {
        AwakeAction += action;
    }

    public void RemoveStart(UnityAction action)
    {
        AwakeAction -= action;
    }
    public void AddUpdate(UnityAction action)
    {
        AwakeAction += action;
    }

    public void RemoveUpdate(UnityAction action)
    {
        AwakeAction -= action;
    }
}
