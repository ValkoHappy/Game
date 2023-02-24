using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuScreen : ScreenUI
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _questsButton;
    [SerializeField] private Button _rewardsButton;
    [SerializeField] private Button _rankingButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _userInfoButton;

    public event UnityAction PlayButtonClick;
    public event UnityAction ShopButtonClick;
    public event UnityAction InventoryButtonClick;
    public event UnityAction QuestsButtonClick;
    public event UnityAction RewardsButtonClick;
    public event UnityAction RankingButtonClick;
    public event UnityAction SettingButtonClick;
    public event UnityAction UserInfoButtonClick;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButton);
        _shopButton.onClick.AddListener(OnShopButton);
        _inventoryButton.onClick.AddListener(OnInventoryButton);
        _questsButton.onClick.AddListener(OnQuestsButton);
        _rewardsButton.onClick.AddListener(OnRewardsButton);
        _rankingButton.onClick.AddListener(OnRankingButton);
        _settingButton.onClick.AddListener(OnSettingButton);
        _userInfoButton.onClick.AddListener(OnUserInfoButton);

    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButton);
        _shopButton.onClick.RemoveListener(OnShopButton);
        _inventoryButton.onClick.RemoveListener(OnInventoryButton);
        _questsButton.onClick.RemoveListener(OnQuestsButton);
        _rewardsButton.onClick.RemoveListener(OnRewardsButton);
        _rankingButton.onClick.RemoveListener(OnRankingButton);
        _settingButton.onClick.RemoveListener(OnSettingButton);
        _userInfoButton.onClick.RemoveListener(OnUserInfoButton);

    }

    public void OnPlayButton()
    {
        PlayButtonClick?.Invoke();
    }

    public void OnShopButton()
    {
        ShopButtonClick?.Invoke();
    }

    public void OnInventoryButton()
    {
        InventoryButtonClick?.Invoke();
    }

    public void OnQuestsButton()
    {
        QuestsButtonClick?.Invoke();
    }

    public void OnRewardsButton()
    {
        RewardsButtonClick?.Invoke();
    }

    public void OnRankingButton()
    {
        RankingButtonClick?.Invoke();
    }

    public void OnSettingButton()
    {
        SettingButtonClick?.Invoke();
    }

    public void OnUserInfoButton()
    {
        UserInfoButtonClick?.Invoke();
    }
}
