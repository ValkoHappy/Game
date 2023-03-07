using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    private string _openAnimation = "Open";
    private string _closeAnimation = "Close";

    private Animator _animator;

    public GameObject Panel => _panel;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        _panel.SetActive(true);
        //_animator.SetTrigger(_openAnimation);
    }

    public void Close()
    {
        _panel.SetActive(false);
        //_animator.SetTrigger(_closeAnimation);
    }
}
