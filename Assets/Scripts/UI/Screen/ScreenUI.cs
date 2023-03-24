using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ScreenUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _panel;
    public CanvasGroup Panel => _panel;
    private void Awake()
    {
        _panel = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        _panel.alpha = 1;
        _panel.blocksRaycasts = true;
    }

    public void Close()
    {
        _panel.alpha = 0;
        _panel.blocksRaycasts = false;
    }
}
