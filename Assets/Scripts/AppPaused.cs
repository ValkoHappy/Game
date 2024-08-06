using UnityEngine;
using Agava.WebUtility;

public class AppPaused : MonoBehaviour
{
    private float _maxValue = 1f;
    private float _minValue = 0f;

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnSetPauseState;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnSetPauseState;
    }

    private void OnSetPauseState(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = _minValue;
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = _maxValue;
            AudioListener.pause = false;
        }
    }

    private void OnApplicationPause(bool isPaused)
    {
        OnSetPauseState(isPaused);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        OnSetPauseState(hasFocus);
    }
}

