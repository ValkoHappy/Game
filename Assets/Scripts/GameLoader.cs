using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OpenPlayMenu);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OpenPlayMenu);
    }


    private void OpenPlayMenu()
    {
        Level_1.Load();
    }
}
