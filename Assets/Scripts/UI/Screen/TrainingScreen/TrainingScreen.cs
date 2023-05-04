using UnityEngine;
using UnityEngine.Events;

public class TrainingScreen : MonoBehaviour
{
    [SerializeField] private MovingCameraSpawnEnemies _cameraSpawnEnemies;
    [SerializeField] private InitialTrainingWindow _initialTrainingWindow;
    [SerializeField] private TrainingWindow _trainingWindow;
    [SerializeField] private FinalWindowOfTraining _finalWindow;

    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private ShopScreen _shopScreen;

    private const string Level = "Level";

    public event UnityAction TrainingFinished;

    private void Start()
    {
        int level = PlayerPrefs.GetInt(Level);
        if (level > 1)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _initialTrainingWindow.RefuseToStudyButtonClick += TurnOffTrainingScreen;
        _trainingWindow.ResumeButtonClick += ExitButtle;
        _cameraSpawnEnemies.AnimationIsFinished += _initialTrainingWindow.OnOpenScreen;
        _finalWindow.ResumeButtonClick += TurnOffTrainingScreen; 
    }

    private void OnDisable()
    {
        _initialTrainingWindow.RefuseToStudyButtonClick -= TurnOffTrainingScreen;
        _trainingWindow.ResumeButtonClick -= ExitButtle;
        _cameraSpawnEnemies.AnimationIsFinished -= _initialTrainingWindow.OnOpenScreen;
        _finalWindow.ResumeButtonClick -= TurnOffTrainingScreen;
    }

    private void TurnOffTrainingScreen()
    {
        TrainingFinished?.Invoke();
        _shopScreen.EnabletAllButton();
        _mainMenuScreen.EnabletAllButton();
        gameObject.SetActive(false);
    }

    private void ExitButtle()
    {
        _shopScreen.EnabletAllButton();
        _mainMenuScreen.EnabletShopButton();
    }
}
