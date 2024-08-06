using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private AudioMixerGroup _audioMixerGroup; 

    private AudioSource _audioSource; 
    private int _lastPlayedClipIndex = -1; 
    private float _maxDelayBetweenClips = 15f;

    private Coroutine _playClipCoroutine;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = _audioMixerGroup;
        StartPlayingRandomClip();
    }

    private void StartPlayingRandomClip()
    {
        if (_playClipCoroutine != null)
        {
            StopCoroutine(_playClipCoroutine);
            _playClipCoroutine = null;
        }

        _playClipCoroutine = StartCoroutine(PlayRandomClipWithDelay());
    }

    private IEnumerator PlayRandomClipWithDelay()
    {
        float delay = Random.Range(0, _maxDelayBetweenClips);

        yield return new WaitForSeconds(delay);

        PlayRandomClip();

        while (_audioSource.isPlaying)
        {
            yield return null;
        }

        StartPlayingRandomClip();
    }

    private void PlayRandomClip()
    {
        int randomIndex = GetRandomClipIndex();

        AudioClip randomClip = _audioClips[randomIndex];
        _audioSource.clip = randomClip;
        _audioSource.Play();
        _lastPlayedClipIndex = randomIndex;
    }

    private int GetRandomClipIndex()
    {
        int randomIndex = Random.Range(0, _audioClips.Length);

        while (randomIndex == _lastPlayedClipIndex)
        {
            randomIndex = Random.Range(0, _audioClips.Length);
        }

        return randomIndex;
    }
}
