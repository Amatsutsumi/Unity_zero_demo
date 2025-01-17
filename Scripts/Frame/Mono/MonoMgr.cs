using UnityEngine;
using UnityEngine.Events;

public class MonoMgr : SingleTon<MonoMgr>
{
    //˽�л�һ����������д���캯��
    private MonoController controller;
    public MonoMgr()
    {
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }
    //��д���������Ǽ����ί�л��ڿ�����ִ��

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