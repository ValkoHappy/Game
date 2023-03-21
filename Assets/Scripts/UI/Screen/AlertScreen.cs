using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertScreen : ScreenUI
{
    [SerializeField] private int _shutdownTime = 3;

    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;

        if(_time >= _shutdownTime)
        {
            Close();
            _time = 0;
        }
    }
}
