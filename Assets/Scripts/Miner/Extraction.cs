using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PeacefulConstruction))]
public class Extraction : MonoBehaviour
{
    [SerializeField] private int _amountMoneyProduced;
    [SerializeField] private float _waitForSecounds;

    private MoneyContainer _moneyContainer;
    private Coroutine _extract;
    private PeacefulConstruction _peacefulConstruction;
    private MinerAnimation _minerAnimation;

    private void Awake()
    {
        _moneyContainer = FindObjectOfType<MoneyContainer>();
        _peacefulConstruction = GetComponent<PeacefulConstruction>();
        _minerAnimation = GetComponentInChildren<MinerAnimation>();
    }

    private void Start()
    {
        StartExtract();
    }

    private void StartExtract()
    {
        if (_moneyContainer != null && _peacefulConstruction.IsAlive())
        {
            if (_extract != null)
            {
                StopCoroutine(_extract);
            }
            _extract = StartCoroutine(Extract());
        }
        else
        {
            _minerAnimation.Pause();
        }
    }

    private IEnumerator Extract()
    {
        var waitForSecounds = new WaitForSeconds(_waitForSecounds);

        _moneyContainer.GetMoney(_amountMoneyProduced);
        yield return waitForSecounds;

        if (_peacefulConstruction.IsAlive())
        {
            StartExtract();
        }
        else
        {
            _minerAnimation.Pause();
        }
    }
}
