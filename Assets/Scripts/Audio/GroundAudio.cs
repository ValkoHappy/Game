using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _calmClip;
    [SerializeField] private AudioClip _fightClip;

    public void On—almClip()
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
