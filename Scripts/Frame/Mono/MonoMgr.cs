using UnityEngine;
using UnityEngine.Events;

public class MonoMgr : SingleTon<MonoMgr>
{
    //私有化一个控制器，写构造函数
    private MonoController controller;
    public MonoMgr()
    {
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }
    //书写函数，我们加入的委托会在控制器执行

    public void AddAwake(UnityAction action)
    {
        controller.AddAwake(action);
    }
    public void AddStart(UnityAction action)
    {
        controller.AddStart(action);
    }

    public void AddUpdate(UnityAction action)
    {
        controller.AddUpdate(action);
    }
    public void RemoveAwake(UnityAction action)
    {
        controller.RemoveAwake(action);
    }
    public void RemoveStart(UnityAction action)
    {
        controller.RemoveStart(action);
    }

    public void RemoveUpdate(UnityAction action)
    {
        controller.RemoveUpdate(action);
    }
}