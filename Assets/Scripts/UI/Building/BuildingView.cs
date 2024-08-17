using Scripts.Build;
using UnityEngine;

namespace Scripts.UI.Building
{
    public class BuildingView : MonoBehaviour
    {
        [SerializeField] private MovementScreen _movementScreen;
        [SerializeField] private BuildingCharacteristics _buildingCharacteristics;

        private Build.Building _building;

        private void Awake()
        {
            _building = GetComponent<Build.Building>();
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
}