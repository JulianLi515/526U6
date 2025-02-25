using UnityEngine;

public class Timer
{
    public float timer;
    public void Set(float duration)
    {
        timer = duration;
    }
    public void CountDown()
    {
        timer = Mathf.Max(timer - Time.deltaTime, 0f);
    }

    public bool TimeUp()
    {
        return timer == 0f;
    }
}
