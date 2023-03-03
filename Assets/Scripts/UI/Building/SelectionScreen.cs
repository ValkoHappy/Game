using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionScreen : ScreenUI
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _delateButton;

    private BuildingsGrid _buildingsGrid;

    private void Awake()
    {
        _buildingsGrid = FindObjectOfType<BuildingsGrid>();
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
        _buildingsGrid.SetBuildingModeInsert();
    }
    private void OnDelateButton()
    {
        _buildingsGrid.SetBuildingModeDelete();

    }
}
