using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MovementScreen : ScreenUI
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _delateButton;

    private MoveSelection _moveSelection;

    private void Awake()
    {
        _moveSelection = FindObjectOfType<MoveSelection>();
    }

    private void Start()
    {
        Open();
    }

    private void OnEnable()
    {
        _saveButton.onClick.AddListener(OnSaveButton);
        _delateButton.onClick.AddListener(OnDelateButton);
    }

    private void OnDisable()
    {
        _saveButton.onClick.RemoveListener(OnSaveButton);
        _delateButton.onClick.RemoveListener(OnDelateButton);
    }

    private void OnSaveButton()
    {
        _moveSelection.SetBuildingModeInsert();
        //Close();
    }
    private void OnDelateButton()
    {
        _moveSelection.SetBuildingModeDelete();
        //Close();
    }
}
