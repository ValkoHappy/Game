using System;
using UnityEngine;

public class TrainingScreen : MonoBehaviour
{
    private const string Level = "Level";

    [SerializeField] private MovingCameraSpawnEnemies _cameraSpawnEnemies;
    [SerializeField] private InitialTrainingWindow _initialTrainingWindow;
    [SerializeField] private TrainingWindow _trainingWindow;
    [SerializeField] private FinalWindowOfTraining _finalWindow;

    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private ShopScreen _shopScreen;

    public event Action Finished;

    private void Start()
    {
        int level = PlayerPrefs.GetInt(Level);

        if (level > 1)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _initialTrainingWindow.StudyRefused += OnTurnOffTraining;
        _trainingWindow.Resumed += OnExitButtle;
        _cameraSpawnEnemies.AnimationFinished += OnOpenInitialWindow;
        _finalWindow.ButtonResumed += OnTurnOffTraining; 
    }

    private void OnDisable()
    {
        _initialTrainingWindow.StudyRefused -= OnTurnOffTraining;
        _trainingWindow.Resumed -= OnExitButtle;
        _cameraSpawnEnemies.AnimationFinished -= OnOpenInitialWindow;
        _finalWindow.ButtonResumed -= OnTurnOffTraining;
    }

    private void OnTurnOffTraining()
    {
        Finished?.Invoke();
        _shopScreen.EnabletAllButton();
        _mainMenuScreen.EnabletAllButton();
        gameObject.SetActive(false);
    }

    private void OnExitButtle()
    {
        _shopScreen.EnabletAllButton();
        _mainMenuScreen.EnabletShopButton();
    }

    private void OnOpenInitialWindow()
    {
        _initialTrainingWindow.OnOpen();
    }
}
