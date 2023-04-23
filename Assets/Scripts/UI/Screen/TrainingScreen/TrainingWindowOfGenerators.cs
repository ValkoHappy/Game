using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TrainingWindowOfGenerators : UIScreenAnimator
{
    [SerializeField] private GameObject _indicator;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private GameObject _shop;
    [SerializeField] private BuildingsGrid _buildingGrid;

    private const string Oil = "Oil";

    public event UnityAction ResumeButtonClick;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButton);
        _buildingGrid.BuildingSupplied += OnOpenScreen;
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButton);
        _buildingGrid.BuildingSupplied -= OnOpenScreen;
    }

    private void OnOpenScreen(Building building)
    {
        if (building.tag == Oil)
        {
            _shop.SetActive(false);
            OpenScreen();
            if (_indicator != null)
                _indicator.SetActive(true);
        }
    }

    private void OnResumeButton()
    {
        _shop.SetActive(true);
        ResumeButtonClick?.Invoke();
        CloseScreen();
        if (_indicator != null)
            _indicator.SetActive(false);
    }
}
