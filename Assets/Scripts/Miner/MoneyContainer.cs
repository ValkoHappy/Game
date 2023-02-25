using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyContainer : MonoBehaviour
{
    [SerializeField] private int _money;

    public int Money => _money;
    public event UnityAction<int> MoneyChanged;

    public void GetMoney(int value)
    {
        _money += value;
        MoneyChanged?.Invoke(_money);
    }

    public void BuyBuilding(Goods statsBuilding)
    {
        _money -= statsBuilding.Price;
    }
}
