using UnityEngine;

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
        _buildingsHandler.AllBuildingsBroked += OnÑalmClip;
    }

    private void OnDisable()
    {
        _enemyHandler.AllEnemiesKilled -= OnÑalmClip;
        _buildingsHandler.AllBuildingsBroked -= OnÑalmClip;
    }

    public void OnÑalmClip()
    { 
        _audioSource.clip = _calmClip;
        _audioSource.Play();
    }

    public void OnFightClip()
    {
        _audioSource.clip = _fightClip;
        _audioSource.Play();
    }
}
