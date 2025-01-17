using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : SingleTon<GameEventManager>
{
    public delegate void MyAction<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    private interface IEventHelp
    {

    }

    private class EventHelp : IEventHelp
    {
        public event UnityAction Action;

        public void AddCall(UnityAction action)
        {
            Action += action;
        }

        public void Call()
        {
            Action?.Invoke();
        }

        public void RemoveCall(UnityAction action)
        {
            Action -= action;
        }
    }

    private class EventHelp<T> : IEventHelp
    {
        public event UnityAction<T> Action;

        public void AddCall(UnityAction<T> action)
        {
            Action += action;
        }

        public void Call(T param)
        {
            Action?.Invoke(param);
        }

        public void RemoveCall(UnityAction<T> action)
        {
            Action -= action;
        }
    }

    private class EventHelp<T1, T2, T3> : IEventHelp
    {
        public event UnityAction<T1, T2, T3> Action;

        public void AddCall(UnityAction<T1, T2, T3> action)
        {
            Action += action;
        }

        public void Call(T1 param1, T2 param2, T3 param3)
        {
            Action?.Invoke(param1, param2, param3);
        }

        public void RemoveCall(UnityAction<T1, T2, T3> action)
        {
            Action -= action;
        }
    }

    private class EventHelp<T1, T2, T3,T4> : IEventHelp
    {
        public event UnityAction<T1, T2, T3,T4> Action;

        public void AddCall(UnityAction<T1, T2, T3,T4> action)
        {
            Action += action;
        }

        public void Call(T1 param1, T2 param2, T3 param3,T4 param4)
        {
            Action?.Invoke(param1, param2, param3,param4);
        }

        public void RemoveCall(UnityAction<T1, T2, T3,T4> action)
        {
            Action -= action;
        }
    }

    private class EventHelp<T1, T2, T3, T4, T5> : IEventHelp
    {
        public event MyAction<T1, T2, T3, T4, T5> Action;

        public void AddCall(MyAction<T1, T2, T3, T4, T5> action)
        {
            Action += action;
        }

        public void Call(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            Action?.Invoke(param1, param2, param3, param4, param5);
        }

        public void RemoveCall(MyAction<T1, T2, T3, T4, T5> action)
        {
            Action -= action;
        }
    }

    private Dictionary<string, IEventHelp> eventManager = new Dictionary<string, IEventHelp>();

    // 添加事件监听
    public void AddEventListening(string eventName, UnityAction action)
    {
        if (!eventManager.ContainsKey(eventName))
        {
            eventManager[eventName] = new EventHelp();
        }
        ((EventHelp)eventManager[eventName]).AddCall(action);
    }

    // 添加带参数的事件监听
    public void AddEventListening<T>(string eventName, UnityAction<T> action)
    {
        if (!eventManager.ContainsKey(eventName))
        {
            eventManager[eventName] = new EventHelp<T>();
        }
        ((EventHelp<T>)eventManager[eventName]).AddCall(action);
    }

    public void AddEventListening<T1, T2, T3>(string eventName, UnityAction<T1, T2, T3> action)
    {
        if (!eventManager.ContainsKey(eventName))
        {
            eventManager[eventName] = new EventHelp<T1, T2, T3>();
        }
        ((EventHelp<T1, T2, T3>)eventManager[eventName]).AddCall(action);
    }

    public void AddEventListening<T1, T2, T3,T4>(string eventName, UnityAction<T1, T2, T3,T4> action)
    {
        if (!eventManager.ContainsKey(eventName))
        {
            eventManager[eventName] = new EventHelp<T1, T2, T3,T4>();
        }
    ((EventHelp<T1, T2, T3,T4>)eventManager[eventName]).AddCall(action);
    }

    public void AddEventListening<T1, T2, T3, T4, T5>(string eventName, MyAction<T1, T2, T3, T4, T5> action)
    {
        if (!eventManager.ContainsKey(eventName))
        {
            eventManager[eventName] = new EventHelp<T1, T2, T3, T4, T5>();
        }
        ((EventHelp<T1, T2, T3, T4, T5>)eventManager[eventName]).AddCall(action);
    }

    // 删除事件监听
    public void RemoveEvent(string eventName, UnityAction action)
    {
        if (eventManager.ContainsKey(eventName))
        {
            var eventHelp = eventManager[eventName] as EventHelp;
            if (eventHelp != null)
            {
                eventHelp.RemoveCall(action);
            }
        }
    }

    // 删除带参数的事件监听
    public void RemoveEvent<T>(string eventName, UnityAction<T> action)
    {
        if (eventManager.ContainsKey(eventName))
        {
            var eventHelp = eventManager[eventName] as EventHelp<T>;
            if (eventHelp != null)
            {
                eventHelp.RemoveCall(action);
            }
        }
    }

    public void RemoveEvent<T1, T2, T3>(string eventName, UnityAction<T1, T2, T3> action)
    {
        if (eventManager.ContainsKey(eventName))
        {
            var eventHelp = eventManager[eventName] as EventHelp<T1, T2, T3>;
            if (eventHelp != null)
            {
                eventHelp.RemoveCall(action);
            }
        }
    }

    public void RemoveEvent<T1, T2, T3,T4>(string eventName, UnityAction<T1, T2, T3,T4> action)
    {
        if (eventManager.ContainsKey(eventName))
        {
            var eventHelp = eventManager[eventName] as EventHelp<T1, T2, T3,T4>;
            if (eventHelp != null)
            {
                eventHelp.RemoveCall(action);
            }
        }
    }

    public void RemoveEvent<T1, T2, T3, T4, T5>(string eventName, MyAction<T1, T2, T3, T4, T5> action)
    {
        if (eventManager.ContainsKey(eventName))
        {
            var eventHelp = eventManager[eventName] as EventHelp<T1, T2, T3, T4, T5>;
            if (eventHelp != null)
            {
                eventHelp.RemoveCall(action);
            }
        }
    }

    // 触发事件
    public void InvokeEvent(string eventName)
    {
        if (eventManager.ContainsKey(eventName))
        {
            var eventHelp = eventManager[eventName] as EventHelp;
            if (eventHelp != null)
            {
                eventHelp.Call();
            }
        }
    }

    // 触发带参数的事件
    public void InvokeEvent<T>(string eventName, T param)
    {
        if (eventManager.ContainsKey(eventName))
        {
            var eventHelp = eventManager[eventName] as EventHelp<T>;
            if (eventHelp != null)
            {
                eventHelp.Call(param);
            }
        }
    }

    public void InvokeEvent<T1, T2, T3>(string eventName, T1 param1, T2 param2, T3 param3)
    {
        if (eventManager.ContainsKey(eventName))
        {
            var eventHelp = eventManager[eventName] as EventHelp<T1, T2, T3>;
            if (eventHelp != null)
            {
                eventHelp.Call(param1, param2, param3);
            }
        }
    }

    public void InvokeEvent<T1, T2, T3,T4>(string eventName, T1 param1, T2 param2, T3 param3,T4 param4)
    {
        if (eventManager.ContainsKey(eventName))
        {
            var eventHelp = eventManager[eventName] as EventHelp<T1, T2, T3,T4>;
            if (eventHelp != null)
            {
                eventHelp.Call(param1, param2, param3,param4);
            }
        }
    }

    public void InvokeEvent<T1, T2, T3, T4, T5>(string eventName, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
    {
        if (eventManager.ContainsKey(eventName))
        {
            var eventHelp = eventManager[eventName] as EventHelp<T1, T2, T3, T4, T5>;
            if (eventHelp != null)
            {
                eventHelp.Call(param1, param2, param3, param4, param5);
            }
        }
    }
}