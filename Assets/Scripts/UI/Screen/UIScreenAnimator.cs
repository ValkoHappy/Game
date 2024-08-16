using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIScreenAnimator : MonoBehaviour
{
    private const string OpenStr = "Open";
    private const string CloseStr = "Close";

    [SerializeField] private CanvasGroup _panel;

    private Animator _animator;

    private float _minAlpha = 0;
    private float _maxAlpha = 1;

    private void Awake()
    {
        _panel = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();
    }

    public virtual void OnOpen()
    {
        _panel.blocksRaycasts = true;

        if (_animator != null)
            _animator.SetTrigger(OpenStr);

        _panel.alpha = _maxAlpha;
    }

    public virtual void OnClose()
    {
        _panel.blocksRaycasts = false;

        if (_animator != null)
            _animator.SetTrigger(CloseStr);

        _panel.alpha = _minAlpha;
    }
}
