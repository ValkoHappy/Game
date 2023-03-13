using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PeacefulConstruction), typeof(Animator))]
public class Extraction : MonoBehaviour
{
    [SerializeField] private int _amountMoneyProduced;
    [SerializeField] private float _waitForSecounds;

    private GoldContainer _moneyContainer;
    private Coroutine _extract;
    private PeacefulConstruction _peacefulConstruction;
    private Animator _animator;
    private Building _building;
    private BuildingsGrid _buildingGrid;

    private void Awake()
    {
        _building = GetComponentInParent<Building>();
        _moneyContainer = FindObjectOfType<GoldContainer>();
        _buildingGrid = FindObjectOfType<BuildingsGrid>();
        _peacefulConstruction = GetComponent<PeacefulConstruction>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartExtract();
    }

    private void OnEnable()
    {
        _building.DeliveryBuilding += OnExtractionAnimation;
        _buildingGrid.EditPositionBuilding += OffExtractionAnimation;
        _buildingGrid.DeliveredBuilding += OnExtractionAnimation;
    }

    private void OnDisable()
    {
        _building.DeliveryBuilding -= OnExtractionAnimation;
        _buildingGrid.EditPositionBuilding -= OffExtractionAnimation;
        _buildingGrid.DeliveredBuilding -= OnExtractionAnimation;
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
            _animator.SetTrigger("Extraction");
        }
    }

    private IEnumerator Extract()
    {
        var waitForSecounds = new WaitForSeconds(_waitForSecounds);

        _moneyContainer.GetGold(_amountMoneyProduced);
        yield return waitForSecounds;

        if (_peacefulConstruction.IsAlive())
        {
            StartExtract();
        }
        else
        {
            _animator.SetTrigger("Extraction");
        }
    }

    public void OnExtractionAnimation()
    {
        _animator.SetTrigger("Extraction");
    }

    public void OffExtractionAnimation()
    {
        _animator.SetTrigger("Idle");
    }
}
