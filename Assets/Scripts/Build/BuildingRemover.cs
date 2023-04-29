using UnityEngine;

public class BuildingRemover : MonoBehaviour
{
    [SerializeField] private BuildingsHandler _buildingsHandler;
    [SerializeField] private GoldContainer _goldContainer;
    [SerializeField] private CrystalsContainer _crystalsContainer;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Start()
    {
        enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 clickPoint = Input.mousePosition;
            if (Input.touchCount > 0)
            {
                clickPoint = Input.GetTouch(0).position;
            }

            Ray ray = _camera.ScreenPointToRay(clickPoint);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out Building building))
                {
                    _buildingsHandler.OnDestroyBuilding(building);
                    ReturnCurrency(building);
                    Destroy(building.gameObject);
                }
            }
        }
    }

    private void ReturnCurrency(Building building)
    {
        Goods goods = building.BuildingCharacteristics.Goods;
        if (goods.IsSoldForCrystals)
        {
            _crystalsContainer.AddCrystals(goods.Price);
        }
        else
        {
            _goldContainer.AddGold(goods.Price);
        }
    }
}
