using System;
using UnityEngine;

public enum TimerWorkState
{ 
    NONE,
    WORKING,
    DONE
}

public class GameTimer
{
    private float _startTime;
    private bool _stop;
    private Action _action;
    private TimerWorkState _state;

    public GameTimer()
    {
        
    }

    public void CallOut(float time,Action task)
    {
        _startTime = time;
        _stop = false;
        _action = task;
        _state = TimerWorkState.WORKING;
    }

    public void Working()
    {
        _startTime -= Time.deltaTime;
        if (_startTime < 0)
        {
            _action.Invoke();
            _stop = true;
            _state = TimerWorkState.DONE;
        }
    }

    public TimerWorkState GetTimerState() => _state;

    public void ResetTimer()
    {
        _startTime = 0f;
        _stop = true;
        _action = null;
        _state = TimerWorkState.NONE;
    }
}