using System;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string CurrentLevel = "CurrentLevel";
    private const string Level = "Level";
    private const string Gold = "Gold";
    private const string AllGold = "AllGold";
    private const string Crystals = "Crystals";
    private const string Map = "Map";

    [SerializeField] private Spawner _spawner;
    [SerializeField] private GoldContainer _goldContainer;
    [SerializeField] private CrystalsContainer _crystalsContainer;
    [SerializeField] private SceneNext _sceneManage;

    private int _initialLevel = 1;
    private int _initialMap = 1;
    private int _initialAmountGold = 175;
    private int _initialAmountCrystals = 75;

    public event Action SaveNotFounded;

    private void Awake()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(CurrentLevel, _spawner.CurrentLevelIndex);
        PlayerPrefs.SetInt(Level, _spawner.LevelIndex);
        PlayerPrefs.SetInt(Gold, _goldContainer.Gold);
        PlayerPrefs.SetInt(Crystals, _crystalsContainer.Crystals);
        PlayerPrefs.SetInt(Map, _sceneManage.SceneIndex);
        PlayerPrefs.SetInt(AllGold, _goldContainer.AllGoldReceived);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(Level))
        {
            if(_spawner != null)
            {
                _spawner.InitCurrentLevel(PlayerPrefs.GetInt(CurrentLevel));
                _spawner.InitLevel(PlayerPrefs.GetInt(Level));
                _goldContainer.Init(PlayerPrefs.GetInt(Gold), PlayerPrefs.GetInt(AllGold));
                _crystalsContainer.Init(PlayerPrefs.GetInt(Crystals));
                _sceneManage.Init(PlayerPrefs.GetInt(Map));
            }
        }
    }

    public void LoadScene()
    {
        if (PlayerPrefs.HasKey(Map))
        {
            _sceneManage.Init(PlayerPrefs.GetInt(Map));
            SaveNotFounded?.Invoke();
        }
    }

    public void ResetLevel()
    {
        PlayerPrefs.SetInt(CurrentLevel, 0);
        PlayerPrefs.SetInt(Map, _sceneManage.SceneIndex);
    }

    public void Clear()
    {
        PlayerPrefs.SetInt(CurrentLevel, 0);
        PlayerPrefs.SetInt(Level, _initialLevel);
        PlayerPrefs.SetInt(Gold, _initialAmountGold);
        PlayerPrefs.SetInt(Crystals, _initialAmountCrystals);
        PlayerPrefs.SetInt(Map, _initialMap);
    }
}
