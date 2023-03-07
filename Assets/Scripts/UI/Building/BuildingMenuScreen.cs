using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildingMenuScreen : ScreenUI
{
    [SerializeField] private Button _improvementsButton;
    [SerializeField] private Button _moveButton;
    [SerializeField] private Button _informationButton;

    private BuildingsGrid _grid;

    public event UnityAction ImprovementsButtonClick;
    public event UnityAction<Building> MoveButtonClick;
    public event UnityAction InformationButtonClick;

    private void Awake()
    {
        _grid = FindObjectOfType<BuildingsGrid>();
    }

    private void OnEnable()
    {
        _improvementsButton.onClick.AddListener(OnImprovementsButton);
        _moveButton.onClick.AddListener(OnMoveButton);
        _informationButton.onClick.AddListener(OnInformationButton);
    }

    private void OnDisable()
    {
        _improvementsButton.onClick.RemoveListener(OnImprovementsButton);
        _moveButton.onClick.RemoveListener(OnMoveButton);
        _informationButton.onClick.RemoveListener(OnInformationButton);
    }

    private void OnImprovementsButton()
    {
        ImprovementsButtonClick?.Invoke();
    }

    private void OnMoveButton()
    {
        _grid.MoveBuilding(GetComponentInParent<Building>());
    }

    private void OnInformationButton()
    {
        InformationButtonClick?.Invoke();
    }
}
