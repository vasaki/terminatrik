using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    /// timer duration
	float totalSeconds = 0;
    // timer execution
    float elapsedSeconds = 0;
    bool running = false;
    // support for Finished property
    bool started = false;
    UnityEvent timeEvent = new UnityEvent();
    public float Duration
    {
        get => totalSeconds;
        set
        {
            if (!running)
            {
                totalSeconds = value;
            }
        }
    }
    public float Remaining { get => Duration - elapsedSeconds; }
    public bool Finished {  get => started && !running; }
    public bool Running { get => running; }
    public bool HasntStarted { get => !started; }
    void Update()
    {
        // update timer and check for finished
        if (running)
        {
            elapsedSeconds += Time.deltaTime;
            if (elapsedSeconds >= totalSeconds)
            {
                running = false;
                timeEvent.Invoke();
            }
        }
    }
    public void Run()
    {
        // only run with valid duration
        if (totalSeconds > 0)
        {
            started = true;
            running = true;
            elapsedSeconds = 0;
        }
    }
    public void Stop()
    {
        started = false;
        running = false;
        elapsedSeconds = 0;
    }
    public void Restart()
    {
        Stop();
        Run();
    }
    public void AddListener(UnityAction listener)
    {
        timeEvent.AddListener(listener);
    }
}
