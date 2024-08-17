using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class IndicatorCurrencyScore : MonoBehaviour
    {
        private const string Plus = "+";

        [SerializeField] private TMP_Text _score;

        private IndicatorReceivedCurrency _indicatorReceived;

        private void Awake()
        {
            _indicatorReceived = GetComponent<IndicatorReceivedCurrency>();
        }

        private void Start()
        {
            _score.text = Plus + _indicatorReceived.AmountCurrencyReceived.ToString();
        }

        private void OnEnable()
        {
            _score.text = Plus + _indicatorReceived.AmountCurrencyReceived.ToString();
            _indicatorReceived.OnCurrencyReceived += OnSetScore;
        }

        private void OnDisable()
        {
            _indicatorReceived.OnCurrencyReceived -= OnSetScore;
        }

        private void OnSetScore(int score)
        {
            _score.text = Plus + score.ToString();
        }
    }
}