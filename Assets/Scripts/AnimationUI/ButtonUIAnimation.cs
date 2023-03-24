using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonUIAnimation : MonoBehaviour
{
    [SerializeField] private LobbyCameraAnimation _lobbyCameraAnimation;
    [SerializeField] private List<RectTransform> _upButtons;
    [SerializeField] private List<RectTransform> _downButtons;
    [SerializeField] private List<RectTransform> _leftButtons;
    [SerializeField] private List<RectTransform> _rightButtons;


    private void OnEnable()
    {
        _lobbyCameraAnimation.AnimationIsFinished += StartShoot;
    }

    private void OnDisable()
    {
        _lobbyCameraAnimation.AnimationIsFinished -= StartShoot;
    }

    public void StartShoot()
    {
         StartCoroutine(PushButtons(0.4f, 0.7f));
    }

    private IEnumerator PushButtons(float delayStart, float delayButton)
    {
        yield return new WaitForSeconds(delayStart);
        foreach (var button in _downButtons)
        {
            button.DOMoveY(100, 0.6f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(delayButton);
        }
    }
}
