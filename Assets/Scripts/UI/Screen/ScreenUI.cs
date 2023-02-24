using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScreenUI : MonoBehaviour
{
    [SerializeField] protected GameObject Panel;
    private string _openAnimation = "Open";
    private string _closeAnimation = "Close";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        Panel.SetActive(true);
        //_animator.SetTrigger(_openAnimation);
    }

    public void Close()
    {
        Panel.SetActive(false);
        //_animator.SetTrigger(_closeAnimation);
    }
}
