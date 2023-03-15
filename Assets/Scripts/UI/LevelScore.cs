using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelScore : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        _score.text = _spawner.Level.ToString();
        _spawner.LevelChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _spawner.LevelChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _score.text = score.ToString();
    }
}
