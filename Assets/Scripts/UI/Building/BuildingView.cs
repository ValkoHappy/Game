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
        _building.CreateBuilding += OnOpen;
        _building.DeliveryBuilding += OnClose;
    }

    private void OnDisable()
    {
        _building.CreateBuilding -= OnOpen;
        _building.DeliveryBuilding -= OnClose;
    }

    private void OnOpen()
    {
        _movementScreen.OpenScreen();
    }

    private void OnClose()
    {
        _buildingCharacteristics.CloseRadiusAttack();
        _movementScreen.CloseScreen();
    }
}
