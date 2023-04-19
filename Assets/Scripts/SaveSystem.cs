using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GoldContainer _goldContainer;
    [SerializeField] private CrystalsContainer _crystalsContainer;
    [SerializeField] private SceneNext _sceneManage;

    private const string _currentLevel = "CurrentLevel";
    private const string _level = "Level";
    private const string _gold = "Gold";
    private const string _allGold = "AllGold";
    private const string _crystals = "Crystals";
    private const string _map = "Map";

    public event UnityAction SaveNotFound;

    public void Save()
    {
        PlayerPrefs.SetInt(_currentLevel, _spawner.CurrentLevelIndex);
        PlayerPrefs.SetInt(_level, _spawner.LevelIndex);
        PlayerPrefs.SetInt(_gold, _goldContainer.Gold);
        PlayerPrefs.SetInt(_crystals, _crystalsContainer.Crystals);
        PlayerPrefs.SetInt(_map, _sceneManage.SceneIndex);
        PlayerPrefs.SetInt(_allGold, _goldContainer.AllGoldReceived);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(_level))
        {
            _spawner.InitCurrentLevel(PlayerPrefs.GetInt(_currentLevel));
            _spawner.InitLevel(PlayerPrefs.GetInt(_level));
            _goldContainer.InitGold(PlayerPrefs.GetInt(_gold), PlayerPrefs.GetInt(_allGold));
            _crystalsContainer.InitCrystals(PlayerPrefs.GetInt(_crystals));
            _sceneManage.InitScene(PlayerPrefs.GetInt(_map));
        }

    }

    public void LoadScene()
    {
        if (PlayerPrefs.HasKey(_map))
        {
            _sceneManage.InitScene(PlayerPrefs.GetInt(_map));
            SaveNotFound?.Invoke();
        }
    }

    public void ResetLevel()
    {
        PlayerPrefs.SetInt(_currentLevel, 0);
        PlayerPrefs.SetInt(_map, _sceneManage.SceneIndex);
    }

    public void ResetSave()
    {
        PlayerPrefs.SetInt(_currentLevel, 0);
        PlayerPrefs.SetInt(_level, 1);
        PlayerPrefs.SetInt(_gold, 175); //175
        PlayerPrefs.SetInt(_crystals, 75);
        PlayerPrefs.SetInt(_map, 1);
    }
}
