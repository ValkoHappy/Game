using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GoldContainer _goldContainer;
    [SerializeField] private CrystalsContainer _crystalsContainer;
    [SerializeField] private SceneNext _sceneManage;

    private const string CurrentLevel = "CurrentLevel";
    private const string Level = "Level";
    private const string Gold = "Gold";
    private const string Crystals = "Crystals";
    private const string Map = "Map";

    public event UnityAction SaveNotFound;

    public void Save()
    {
        PlayerPrefs.SetInt(CurrentLevel, _spawner.CurrentLevelIndex);
        PlayerPrefs.SetInt(Level, _spawner.LevelIndex);
        PlayerPrefs.SetInt(Gold, _goldContainer.Gold);
        PlayerPrefs.SetInt(Crystals, _crystalsContainer.Crystals);
        PlayerPrefs.SetInt(Map, _sceneManage.SceneIndex);
        Debug.Log(_sceneManage.SceneIndex);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(Level))
        {
            _spawner.InitCurrentLevel(PlayerPrefs.GetInt(CurrentLevel));
            _spawner.InitLevel(PlayerPrefs.GetInt(Level));
            _goldContainer.InitGold(PlayerPrefs.GetInt(Gold));
            _crystalsContainer.InitCrystals(PlayerPrefs.GetInt(Crystals));
            _sceneManage.InitScene(PlayerPrefs.GetInt(Map));
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
        PlayerPrefs.SetInt(CurrentLevel, 0);
        PlayerPrefs.SetInt(Map, _sceneManage.SceneIndex);
    }

    public void ResetSave()
    {
        PlayerPrefs.SetInt(CurrentLevel, 0);
        PlayerPrefs.SetInt(Level, 1);
        PlayerPrefs.SetInt(Gold, 150);
        PlayerPrefs.SetInt(Crystals, 50);
        PlayerPrefs.SetInt(Map, 1);
    }

}
