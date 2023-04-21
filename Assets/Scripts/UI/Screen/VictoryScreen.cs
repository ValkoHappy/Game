using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VictoryScreen : UIScreenAnimator
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _bonusButton;
    //[SerializeField] private ParticleSystem[] _particles;
    //[SerializeField] private Transform[] _pointsParticles;

    public event UnityAction ResumeButtonClick;
    public event UnityAction BonusButtonClick;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButton);
        _bonusButton.onClick.AddListener(OnBonusButton);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButton);
        _bonusButton.onClick.RemoveListener(OnBonusButton);
    }

    public void OnResumeButton()
    {
        ResumeButtonClick?.Invoke();
    }

    public void OnBonusButton()
    {
        BonusButtonClick?.Invoke();
    }

    //public void OnStartEffect()
    //{
    //    foreach (var particl in _particles)
    //    {
    //        for (int i = 0; i < _pointsParticles.Length; i++)
    //        {
    //            Instantiate(particl, _pointsParticles[i]);
    //        }
    //    }
    //}
}
