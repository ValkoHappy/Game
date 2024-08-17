using Scripts.Build;
using Scripts.Camera;
using UnityEngine;

namespace Scripts.UI.Screen
{
    public class MobileControllerScreen : UIScreenAnimator
    {
        [SerializeField] private LobbyCameraAnimation _lobbyCameraAnimation;
        [SerializeField] private BuildingsGrid _buildingsGrid;

        private void OnEnable()
        {
            _lobbyCameraAnimation.AnimationFinished += OnOpen;
            _buildingsGrid.BuildingCreated += OnOpen;
            _buildingsGrid.BuildingRemoved += OnClose;
            _buildingsGrid.BuildingDelivered += OnClose;
        }

        private void OnDisable()
        {
            _lobbyCameraAnimation.AnimationFinished -= OnOpen;
            _buildingsGrid.BuildingCreated -= OnOpen;
            _buildingsGrid.BuildingRemoved -= OnClose;
            _buildingsGrid.BuildingDelivered -= OnClose;
        }
    }
}