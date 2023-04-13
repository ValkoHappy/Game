using UnityEngine;
using UnityEngine.Events;

public class IndicatorReceivedCurrency : MonoBehaviour
{
    [SerializeField] private string _extraction;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private Spawner _spawner;

    public event UnityAction<int> OnCurrencyReceived;

    public int AmountCurrencyReceived { get; private set; }

    private void OnEnable()
    {
        _buildingsGrid.BuildingSupplied += AddAmountCurrent;
        _spawner.LevelStarted += RemoveAllAmountCurrent;
        _spawner.LevelCreated += RemoveAllAmountCurrent;
    }

    private void OnDisable()
    {
        _buildingsGrid.BuildingSupplied -= AddAmountCurrent;
        _spawner.LevelStarted -= RemoveAllAmountCurrent;
        _spawner.LevelCreated += RemoveAllAmountCurrent;
    }

    private void AddAmountCurrent(Building building)
    {
        if(building.tag == _extraction)
        {
            AmountCurrencyReceived += building.GetComponentInChildren<Extraction>().AmountMoneyProduced;
        }
        OnCurrencyReceived?.Invoke(AmountCurrencyReceived);
    }

    public void RemoveAllAmountCurrent()
    {
        AmountCurrencyReceived = 0;
        OnCurrencyReceived?.Invoke(AmountCurrencyReceived);
    }
}
