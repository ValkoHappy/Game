using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScreen : UIScreenAnimator
{
    [SerializeField] private Button _switchingButton;
    [SerializeField] private Spawner _spawner;

    public event UnityAction SwitchingButtonClick;

    private void OnEnable()
    {
        _spawner.CurrentLevelExceedsCount += OpenScreen;
        _switchingButton.onClick.AddListener(OnSwitchingButton);
    }

    private void OnDisable()
    {
        _spawner.CurrentLevelExceedsCount -= OpenScreen;
        _switchingButton.onClick.RemoveListener(OnSwitchingButton);
    }


    public void OnSwitchingButton()
    {
        SwitchingButtonClick?.Invoke();
        SceneManager.LoadScene(0);
        CloseScreen();
    }
}
