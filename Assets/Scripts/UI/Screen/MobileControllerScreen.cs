using UnityEngine;

public class MobileControllerScreen : UIScreenAnimator  
{
    [SerializeField] private LobbyCameraAnimation _lobbyCameraAnimation;
    [SerializeField] private BuildingsGrid _buildingsGrid;

    private void OnEnable()
    {
        _lobbyCameraAnimation.AnimationIsFinished += OpenScreen;
        _buildingsGrid.CreatedBuilding += OpenScreen;
        _buildingsGrid.RemoveBuilding += CloseScreen;
        _buildingsGrid.DeliveredBuilding += CloseScreen;
    }

    private void OnDisable()
    {
        _lobbyCameraAnimation.AnimationIsFinished -= OpenScreen;
        _buildingsGrid.CreatedBuilding -= OpenScreen;
        _buildingsGrid.RemoveBuilding -= CloseScreen;
        _buildingsGrid.DeliveredBuilding -= CloseScreen;
    }
}
