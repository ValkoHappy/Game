using Scripts.Build;
using Scripts.Enemy;
using UnityEngine;

namespace Scripts.Audio
{
    public class GroundAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _calmClip;
        [SerializeField] private AudioClip _fightClip;

        [SerializeField] private EnemyHandler _enemyHandler;
        [SerializeField] private BuildingsHandler _buildingsHandler;

        private void Start()
        {
            OnÑalmClip();
        }

        private void OnEnable()
        {
            _enemyHandler.AllEnemiesKilled += OnÑalmClip;
            _buildingsHandler.BuildingsBroked += OnÑalmClip;
        }

        private void OnDisable()
        {
            _enemyHandler.AllEnemiesKilled -= OnÑalmClip;
            _buildingsHandler.BuildingsBroked -= OnÑalmClip;
        }

        public void OnÑalmClip()
        {
            SetClip(_calmClip);
        }

        public void OnFightClip()
        {
            SetClip(_fightClip);
        }

        private void SetClip(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}