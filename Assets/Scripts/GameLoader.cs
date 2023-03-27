using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _newPlayButton;
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] SceneNext _sceneNext;

    private void Awake()
    {
        _saveSystem.LoadScene();
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OpenPlayMenu);
        _newPlayButton.onClick.AddListener(ResetPlay);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OpenPlayMenu);
        _newPlayButton.onClick.RemoveListener(ResetPlay);
    }


    private void OpenPlayMenu()
    {
        _sceneNext.OpenScene();
    }

    private void ResetPlay()
    {
        _saveSystem.ResetSave();
        _saveSystem.LoadScene();
        _sceneNext.OpenScene();
    }
}
