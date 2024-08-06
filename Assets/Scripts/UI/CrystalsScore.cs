using TMPro;
using UnityEngine;

public class CrystalsScore : MonoBehaviour
{
    [SerializeField] private CrystalsContainer _crystalsContainer;
    [SerializeField] private TMP_Text _score;

    private void Start()
    {
        _score.text = _crystalsContainer.Crystals.ToString();
    }

    private void OnEnable()
    {
        _score.text = _crystalsContainer.Crystals.ToString();
        _crystalsContainer.CrystalsChanged += OnSetScore;
    }

    private void OnDisable()
    {
        _crystalsContainer.CrystalsChanged -= OnSetScore;
    }

    private void OnSetScore(int score)
    {
        _score.text = score.ToString();
    }
}
