using System;
using UnityEngine;

public class CrystalsContainer : MonoBehaviour
{
    [SerializeField] private int _crystals;

    public event Action<int> CrystalsChanged;
    public int Crystals => _crystals;

    public void Add(int value)
    {
        _crystals += value;
        CrystalsChanged?.Invoke(_crystals);
    }

    public void BuyBuilding(Goods statsBuilding)
    {
        _crystals -= statsBuilding.Price;

        if(_crystals < 0)
            _crystals = 0;

        CrystalsChanged?.Invoke(_crystals);
    }

    public void Init(int crystals)
    {
        _crystals = crystals;
    }
}
