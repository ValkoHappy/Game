using UnityEngine;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _newPlayButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private SettingMenuScreen _settingMenuScreen;
    [SerializeField] private SceneNext _sceneNext;

    private const string _map = "Map";

    private void Awake()
    {
        _saveSystem.LoadScene();
        if (PlayerPrefs.HasKey(_map))
        {
            _continueButton.gameObject.SetActive(true);
        }
        else
        {
            _continueButton.gameObject.SetActive(false);
        }
    }

    

    private void OnEnable()
    {
        _saveSystem.SaveNotFound += OffContinueButton;
        _settingMenuScreen.ExitButtonClick += CloseSettingMenuScreen;
        _settingButton.onClick.AddListener(OpenSettingMenuScreen);
        _continueButton.onClick.AddListener(OpenPlayMenu);
        _newPlayButton.onClick.AddListener(ResetPlay);
    }

    private void OnDisable()
    {
        _saveSystem.SaveNotFound -= OffContinueButton;
        _settingMenuScreen.ExitButtonClick -= CloseSettingMenuScreen;
        _settingButton.onClick.RemoveListener(OpenSettingMenuScreen);
        _continueButton.onClick.RemoveListener(OpenPlayMenu);
        _newPlayButton.onClick.RemoveListener(ResetPlay);
    }

    private void OffContinueButton()
    {
        _continueButton.gameObject.SetActive(true);
    }


    private void OpenPlayMenu()
    {
        _sceneNext.OpenScene();
    }

    private void OpenSettingMenuScreen()
    {
        _settingMenuScreen.Open();
    }

    private void CloseSettingMenuScreen()
    {
        _settingMenuScreen.Close();
    }

    private void ResetPlay()
    {
        _saveSystem.ResetSave();
        _saveSystem.LoadScene();
        _sceneNext.OpenScene();
    }
}
