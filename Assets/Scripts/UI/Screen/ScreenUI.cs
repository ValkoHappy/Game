using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ScreenUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _panel;

    private Animator _animator;
    public CanvasGroup Panel => _panel;
    private void Awake()
    {
        _panel = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();
    }

    public  void Open()
    {
        _panel.blocksRaycasts = true;
        if (_animator != null)
            _animator.SetTrigger("Open");
 
            _panel.alpha = 1;
    }

    public  void Close()
    {
        _panel.blocksRaycasts = false;
        if (_animator != null)
            _animator.SetTrigger("Close");

            _panel.alpha = 0;
    }
}
