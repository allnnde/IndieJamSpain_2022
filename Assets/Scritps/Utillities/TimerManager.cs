using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private static ArrayList timers = new ArrayList();
    private static Stack<Timer> removedTimers = new Stack<Timer>();
    public static bool isPlaying = true;
    private static GameObject instance;

    private void Start()
    {
        if (instance == null)
            TimerManager.instance = this.gameObject;
    }
    private void Update()
    {
        foreach (Timer t in timers)
            t.Update(Time.deltaTime);       
    }

    private static void CreateInstance()
    {
        instance = new GameObject("Timer Manager");
        instance.AddComponent<TimerManager>();
        DontDestroyOnLoad(instance);
    }

    public static void SetupTimer(Timer t)
    {
        if (instance == null) 
            CreateInstance();
        timers.Add(t);
    }

    public static void RemoveTimer(Timer t)
    {
        removedTimers.Push(t);
    }
}

public class Timer{
	private float time = 0f;
	public float endTime;
	public event Action OnTimerExecute;
	public Timer(float endTime)
	{
		TimerManager.SetupTimer(this);
		this.endTime = endTime;
	}
	public Timer(float endTime, Action function) : this(endTime)
	{
        OnTimerExecute += function;
	}

	
	public void Update(float delta)
	{
        time += delta;
        if (time >= endTime)
        {
            OnTimerExecute?.Invoke();
            time = 0;
        }

    }



}
