using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MovementScreen : ScreenUI
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _delateButton;

    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    private BuildingsGrid _buildingsGrid;

    private void Awake()
    {
        _buildingsGrid = FindObjectOfType<BuildingsGrid>();
    }

    private void OnEnable()
    {
        _saveButton.onClick.AddListener(OnSaveButton);
        _delateButton.onClick.AddListener(OnDelateButton);

        _upButton.onClick.AddListener(OnUpButton);
        _downButton.onClick.AddListener(OnDownButton);
        _leftButton.onClick.AddListener(OnLeftButton);
        _rightButton.onClick.AddListener(OnRightButton);
    }

    private void OnDisable()
    {
        _saveButton.onClick.RemoveListener(OnSaveButton);
        _delateButton.onClick.RemoveListener(OnDelateButton);

        _upButton.onClick.RemoveListener(OnUpButton);
        _downButton.onClick.RemoveListener(OnDownButton);
        _leftButton.onClick.RemoveListener(OnLeftButton);
        _rightButton.onClick.RemoveListener(OnRightButton);
    }

    private void OnSaveButton()
    {
        _buildingsGrid.SetBuildingModeInsert();
    }
    private void OnDelateButton()
    {
        _buildingsGrid.SetBuildingModeDelete();

    }

    private void OnUpButton()
    {
        _buildingsGrid.SetBuildingMovesUp();

    }

    private void OnDownButton()
    {
        _buildingsGrid.SetBuildingMovesDown();

    }

    private void OnLeftButton()
    {
        _buildingsGrid.SetBuildingMovesLeft();

    }

    private void OnRightButton()
    {
        _buildingsGrid.SetBuildingMovesRight();

    }
}
