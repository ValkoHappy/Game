using Scripts.Miner;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class GoldScore : MonoBehaviour
    {
        [SerializeField] private GoldContainer _goldContainer;
        [SerializeField] private TMP_Text _score;

        private void Start()
        {
            _score.text = _goldContainer.Gold.ToString();
        }

        private void OnEnable()
        {
            _score.text = _goldContainer.Gold.ToString();
            _goldContainer.GoldChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _goldContainer.GoldChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            _score.text = score.ToString();
        }
    }
}