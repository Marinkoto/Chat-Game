using System;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    public static TickManager instance;

    // Per-frame tickables
    private readonly List<ITickable> tickables = new();

    // Timed ticks for interval-based updates
    private readonly List<TimedTick> timedTicks = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        UpdateFrameTicks(deltaTime);

        UpdateTimedTicks(deltaTime);
    }

    private void UpdateFrameTicks(float deltaTime)
    {
        foreach (var tickable in tickables)
        {
            tickable.Tick(deltaTime);
        }
    }

    private void UpdateTimedTicks(float deltaTime)
    {
        for (int i = 0; i < timedTicks.Count; i++)
        {
            var timedTick = timedTicks[i];
            timedTick.TimeAccumulator += deltaTime;

            if (timedTick.TimeAccumulator >= timedTick.Interval)
            {
                timedTick.TickAction?.Invoke();
                timedTick.TimeAccumulator -= timedTick.Interval;
            }
        }
    }

    public void Register(ITickable tickable)
    {
        if (!tickables.Contains(tickable))
            tickables.Add(tickable);
    }

    public void Unregister(ITickable tickable)
    {
        tickables.Remove(tickable);
    }

    public void RegisterTimedTick(float interval, Action tickAction)
    {
        if (tickAction == null) 
            return;

        timedTicks.Add(new TimedTick(interval, tickAction));
    }

    public void UnregisterTimedTick(Action tickAction)
    {
        timedTicks.RemoveAll(tick => tick.TickAction == tickAction);
    }

    private class TimedTick
    {
        public float Interval;          
        public float TimeAccumulator;   
        public Action TickAction;       

        public TimedTick(float interval, Action tickAction)
        {
            Interval = interval;
            TickAction = tickAction;
            TimeAccumulator = 0f;
        }
    }
}
