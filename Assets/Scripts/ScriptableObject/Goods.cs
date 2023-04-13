using UnityEngine;

[CreateAssetMenu(fileName = "new Goods", menuName = "Goods", order = 51)]
public class Goods : ScriptableObject
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isSoldForCrystals = false;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private bool _isSingleProduct = false;
    [SerializeField] private Building _buildingPrefab;

    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsSoldForCrystals => _isSoldForCrystals;
    public bool IsBuyed => _isBuyed;
    public bool IsSingleProduct => _isSingleProduct;
    public Building BuildingPrefab => _buildingPrefab;
}
