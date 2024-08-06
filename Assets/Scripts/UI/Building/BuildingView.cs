using UnityEngine;

public class BuildingView : MonoBehaviour
{
    [SerializeField] private MovementScreen _movementScreen;
    [SerializeField] private BuildingCharacteristics _buildingCharacteristics;

    private Building _building;

    private void Awake()
    {
        _building = GetComponent<Building>();
    }

    private void OnEnable()
    {
        _building.Created += OnOpen;
        _building.Delivered += OnClose;
    }

    private void OnDisable()
    {
        _building.Created -= OnOpen;
        _building.Delivered -= OnClose;
    }

    private void OnOpen()
    {
        _movementScreen.OnOpen();
    }

    private void OnClose()
    {
        _buildingCharacteristics.CloseRadiusAttack();
        _movementScreen.OnClose();
    }
}
