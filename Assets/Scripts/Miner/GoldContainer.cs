using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoldContainer : MonoBehaviour
{
    [SerializeField] private int _gold;

    public int Gold => _gold;
    public event UnityAction<int> GoldChanged;

    public void GetGold(int value)
    {
        _gold += value;
        GoldChanged?.Invoke(_gold);
    }

    public void BuyBuilding(Goods statsBuilding)
    {
        _gold -= statsBuilding.Price;
        GoldChanged?.Invoke(_gold);
    }
}
