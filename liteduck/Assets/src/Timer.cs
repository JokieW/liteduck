using UnityEngine;
using System.Collections;

public class Timer
{
    private float _startTime;
    private float _delay;

    public Timer(float time)
    {
        Start(time);
    }

    public void Start(float time)
    {
        _startTime = Time.time;
        _delay = time;
    }

    public bool Check()
    {
        return Time.time >= _startTime + _delay;
    }

    public float ElapsedSeconds()
    {
        return Time.time - _startTime;
    }

    public void Reset()
    {
        _startTime = Time.time;
    }
}