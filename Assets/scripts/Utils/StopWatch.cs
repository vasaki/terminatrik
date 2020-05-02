using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopWatch : MonoBehaviour
{
    /// timer duration
	float totalSeconds = 0;
    // timer execution
    float elapsedSeconds = 0;
    bool running = false;
    // support for Finished property
    UnityEvent timeEvent = new UnityEvent();
    public float EventThreshold = 0;
    float nextEventThreshold = 0;
    public bool Running { get => running; }
    public float ElapsedSeconds => elapsedSeconds;
    void Update()
    {
        // update timer and check for finished
        if (running)
        {
            elapsedSeconds += Time.deltaTime;
            if (nextEventThreshold > 0 && elapsedSeconds > nextEventThreshold)
            {
                nextEventThreshold += EventThreshold;
                timeEvent.Invoke();
            }
        }
    }
    public void StartWatch()
    {
        running = true;
        elapsedSeconds = 0;
        nextEventThreshold = EventThreshold;
    }
    public void Stop()
    {
        running = false;
    }
    public void Continue()
    {
        running = true;
    }
    public void AddListener(UnityAction listener)
    {
        timeEvent.AddListener(listener);
    }
}
