using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardsScore : MonoBehaviour
{
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private TMP_Text _gold;
    [SerializeField] private TMP_Text _crystals;

    private void OnEnable()
    {
        _crystals.text = _levelReward.CrystalsCount.ToString();
        _gold.text = _levelReward.GoldCount.ToString();
        _levelReward.CrystalsChanged += OnCrystalsChanged;
        _levelReward.GoldChanged += OnGoldChanged;
    }

    private void OnDisable()
    {
        _levelReward.CrystalsChanged -= OnCrystalsChanged;
        _levelReward.GoldChanged -= OnGoldChanged;
    }

    private void OnGoldChanged(int score)
    {
        _gold.text = score.ToString();
    }

    private void OnCrystalsChanged(int score)
    {
        _crystals.text = score.ToString();
    }
}
