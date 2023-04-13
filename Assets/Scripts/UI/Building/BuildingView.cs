using UnityEngine;

public class BuildingView : MonoBehaviour
{
    [SerializeField] private MovementScreen _movementScreen;

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
        _movementScreen.Open();
        _movementScreen.Panel.alpha = 1.0f;
    }
    private void OnClose()
    {
        _movementScreen.Close();
        _movementScreen.Panel.alpha = 0f;
    }
}
