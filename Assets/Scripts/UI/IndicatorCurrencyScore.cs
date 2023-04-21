using TMPro;
using UnityEngine;

public class IndicatorCurrencyScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    private IndicatorReceivedCurrency _indicatorReceived;

    private void Awake()
    {
        _indicatorReceived = GetComponent<IndicatorReceivedCurrency>();
    }

    private void Start()
    {
        _score.text = "+" + _indicatorReceived.AmountCurrencyReceived.ToString();
    }

    private void OnEnable()
    {
        _score.text = "+" + _indicatorReceived.AmountCurrencyReceived.ToString();
        _indicatorReceived.OnCurrencyReceived += OnScoreChanged;
    }

    private void OnDisable()
    {
        _indicatorReceived.OnCurrencyReceived -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _score.text = "+" + score.ToString();
    }
}
