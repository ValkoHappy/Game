using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extraction : MonoBehaviour
{
    [SerializeField] private int _amountMoneyProduced;
    [SerializeField] private float _waitForSecounds;

    private MoneyContainer _moneyContainer;
    private Coroutine _extract;

    private void Awake()
    {
        _moneyContainer = FindObjectOfType<MoneyContainer>();
    }

    private void Start()
    {
        StartExtract();
    }

    private void StartExtract()
    {
        if (_extract != null)
        {
            StopCoroutine(_extract);
        }
        _extract = StartCoroutine(Extract());
    }

    private IEnumerator Extract()
    {
        var waitForSecounds = new WaitForSeconds(_waitForSecounds);

        _moneyContainer.GetMoney(_amountMoneyProduced);
        yield return waitForSecounds;

        StartExtract();
    }
}
