using UnityEngine;
using UnityEngine.Events;

public class TrainingScreen : MonoBehaviour
{
    [SerializeField] private MovingCameraSpawnEnemies _cameraSpawnEnemies;
    [SerializeField] private TrainingWindow _trainingWindow1;
    [SerializeField] private FinalWindowOfTraining _finalWindow;

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
        _cameraSpawnEnemies.AnimationIsFinished += _trainingWindow1.OnOpenScreen;
        _finalWindow.ResumeButtonClick += TurnOffTrainingScreen; 
    }

    private void OnDisable()
    {
        _cameraSpawnEnemies.AnimationIsFinished -= _trainingWindow1.OnOpenScreen;
        _finalWindow.ResumeButtonClick -= TurnOffTrainingScreen;
    }

    private void TurnOffTrainingScreen()
    {
        TrainingFinished?.Invoke();
        gameObject.SetActive(false);
    }
}
