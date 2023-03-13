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

    private MoveSelection _moveSelection;

    private void Awake()
    {
        _moveSelection = FindObjectOfType<MoveSelection>();
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
        _moveSelection.SetBuildingModeInsert();
    }
    private void OnDelateButton()
    {
        _moveSelection.SetBuildingModeDelete();

    }

    private void OnUpButton()
    {
        _moveSelection.SetBuildingMovesUp();

    }

    private void OnDownButton()
    {
        _moveSelection.SetBuildingMovesDown();

    }

    private void OnLeftButton()
    {
        _moveSelection.SetBuildingMovesLeft();

    }

    private void OnRightButton()
    {
        _moveSelection.SetBuildingMovesRight();

    }
}
