using UnityEngine;
using UnityEngine.Events;

public class CrystalsContainer : MonoBehaviour
{
    [SerializeField] private int _crystals;

    public int Crystals => _crystals;
    public event UnityAction<int> CrystalsChanged;

    public void AddCrystals(int value)
    {
        _crystals += value;
        CrystalsChanged?.Invoke(_crystals);
    }

    public void BuyBuilding(Goods statsBuilding)
    {
        _crystals -= statsBuilding.Price;
        CrystalsChanged?.Invoke(_crystals);
    }

    public void InitCrystals(int crystals)
    {
        _crystals = crystals;
    }
}
