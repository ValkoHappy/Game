using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrystalsScore : MonoBehaviour
{
    [SerializeField] private CrystalsContainer _crystalsContainer;
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        _score.text = _crystalsContainer.Crystals.ToString();
        _crystalsContainer.CrystalsChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _crystalsContainer.CrystalsChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _score.text = score.ToString();
    }
}
