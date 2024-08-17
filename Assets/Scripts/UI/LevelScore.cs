using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class LevelScore : MonoBehaviour
    {
        [SerializeField] private Spawner.Spawner _spawner;
        [SerializeField] private TMP_Text _score;

        private void OnEnable()
        {
            _score.text = _spawner.LevelIndex.ToString();
            _spawner.LevelChanged += OnSetScore;
        }

        private void OnDisable()
        {
            _spawner.LevelChanged -= OnSetScore;
        }

        private void OnSetScore(int score)
        {
            _score.text = score.ToString();
        }
    }
}