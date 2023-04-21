using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertScreen : UIScreenAnimator
{
    [SerializeField] private int _shutdownTime = 3;

    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;

        if(_time >= _shutdownTime)
        {
            CloseScreen();
            _time = 0;
        }
    }
}
