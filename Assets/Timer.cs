using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float startTime = 300f;
    private float timer;
    private bool isRunning = false;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (isRunning && timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay();

            if (timer <= 0)
            {
                timer = 0;
                isRunning = false;
                TimerEnded();
            }
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        timer = startTime;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimerEnded()
    {
        Debug.Log("Timer has ended!");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isRunning)
        {
            StartTimer();
        }
    }
}
