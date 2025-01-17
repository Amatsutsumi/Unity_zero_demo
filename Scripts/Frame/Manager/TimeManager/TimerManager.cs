using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoSingle<TimerManager>
{
    private List<GameTimer> _activeTimers = new List<GameTimer>();
    private Queue<GameTimer> _dormancyTimers = new Queue<GameTimer>();
    [SerializeField] private int _howManyTimer = 10;
    private void Awake()
    {
        InitTimer(_howManyTimer);
    }
    private void Update()
    {
        TimerWork();
    }
    private void InitTimer(int timer)
    {
        for (int i = 0; i < _howManyTimer; i++)
        {
            GameTimer timer1 = new GameTimer();
            _dormancyTimers.Enqueue(timer1);
        }
    }

    public void TryGetOneTimer(float time,Action action)
    {
        if(_dormancyTimers.Count ==0)
        {
            GameTimer timer = new GameTimer();
            timer.CallOut(time,action);
            _activeTimers.Add(timer);
        }
        else
        {
            GameTimer timer = _dormancyTimers.Dequeue();
            timer.CallOut(time,action);
            _activeTimers.Add(timer);
        }
    }

    private void TimerWork()
    {
        if (_activeTimers.Count == 0) return;
        for (int i = 0; i < _activeTimers.Count; i++)
        {
            if (_activeTimers[i].GetTimerState() == TimerWorkState.WORKING)
            {
                _activeTimers[i].Working();
            }
            else
            {
                _dormancyTimers.Enqueue(_activeTimers[i]);
                _activeTimers[i].ResetTimer();
                _activeTimers.Remove(_activeTimers[i]);
            }
        }
    }
}
