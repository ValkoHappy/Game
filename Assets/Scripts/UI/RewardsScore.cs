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
        _levelReward.CrystalsChanged += OnSetCrystals;
        _levelReward.GoldChanged += OnSetGold;
    }

    private void OnDisable()
    {
        _levelReward.CrystalsChanged -= OnSetCrystals;
        _levelReward.GoldChanged -= OnSetGold;
    }

    private void OnSetGold(int score)
    {
        _gold.text = score.ToString();
    }

    private void OnSetCrystals(int score)
    {
        _crystals.text = score.ToString();
    }
}
