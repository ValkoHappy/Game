using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GoldContainer _goldContainer;
    [SerializeField] private CrystalsContainer _crystalsContainer;
    [SerializeField] private SceneNext _sceneManage;

    private int _initialLevel = 1;
    private int _initialMap = 1;
    private int _initialAmountGold = 175;
    private int _initialAmountCrystals = 75;

    private const string ÑurrentLevel = "CurrentLevel";
    private const string Level = "Level";
    private const string Gold = "Gold";
    private const string AllGold = "AllGold";
    private const string Crystals = "Crystals";
    private const string Map = "Map";

    public event UnityAction SaveNotFound;

    private void Awake()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(ÑurrentLevel, _spawner.CurrentLevelIndex);
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
                _spawner.InitCurrentLevel(PlayerPrefs.GetInt(ÑurrentLevel));
                _spawner.InitLevel(PlayerPrefs.GetInt(Level));
                _goldContainer.InitGold(PlayerPrefs.GetInt(Gold), PlayerPrefs.GetInt(AllGold));
                _crystalsContainer.InitCrystals(PlayerPrefs.GetInt(Crystals));
                _sceneManage.InitScene(PlayerPrefs.GetInt(Map));
            }
        }
    }

    public void LoadScene()
    {
        if (PlayerPrefs.HasKey(Map))
        {
            _sceneManage.InitScene(PlayerPrefs.GetInt(Map));
            SaveNotFound?.Invoke();
        }
    }

    public void ResetLevel()
    {
        PlayerPrefs.SetInt(ÑurrentLevel, 0);
        PlayerPrefs.SetInt(Map, _sceneManage.SceneIndex);
    }

    public void ResetSave()
    {
        PlayerPrefs.SetInt(ÑurrentLevel, 0);
        PlayerPrefs.SetInt(Level, _initialLevel);
        PlayerPrefs.SetInt(Gold, _initialAmountGold);
        PlayerPrefs.SetInt(Crystals, _initialAmountCrystals);
        PlayerPrefs.SetInt(Map, _initialMap);
    }
}
