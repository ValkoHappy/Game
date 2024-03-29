using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PeacefulConstruction), typeof(Animator))]
public class GeneratorMining : MonoBehaviour
{
    [SerializeField] private int _amountMoneyProduced;
    [SerializeField] private float _waitForSecounds;

    private GoldContainer _moneyContainer;
    private Coroutine _extract;
    private PeacefulConstruction _peacefulConstruction;
    private HealthContainer _healthContainer;
    private Animator _animator;
    private Building _building;
    private BuildingsGrid _buildingGrid;
    private const string Extraction = "Extraction";

    public int AmountMoneyProduced => _amountMoneyProduced;

    private void Awake()
    {
        _building = GetComponentInParent<Building>();
        _moneyContainer = FindObjectOfType<GoldContainer>();
        _buildingGrid = FindObjectOfType<BuildingsGrid>();
        _peacefulConstruction = GetComponent<PeacefulConstruction>();
        _healthContainer = GetComponent<HealthContainer>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _building.DeliveryBuilding += OnExtractionAnimation;
        _buildingGrid.DeliveredBuilding += OnExtractionAnimation;
        _buildingGrid.DeliveredBuilding += StartExtract;
        _peacefulConstruction.Died += OnOffExtractionAnimation;
        _peacefulConstruction.BuildingRestored += StartExtract;
        _healthContainer.Died += OnOffExtractionAnimation;
    }

    private void OnDisable()
    {
        _building.DeliveryBuilding -= OnExtractionAnimation;
        _buildingGrid.DeliveredBuilding -= OnExtractionAnimation;
        _buildingGrid.DeliveredBuilding -= StartExtract;
        _peacefulConstruction.Died -= OnOffExtractionAnimation;
        _peacefulConstruction.BuildingRestored -= StartExtract;
        _healthContainer.Died -= OnOffExtractionAnimation;
    }

    public void OnExtractionAnimation()
    {
        _animator.enabled = true;
        _animator.SetTrigger(Extraction);
    }

    public void OnOffExtractionAnimation()
    {
        _animator.enabled = false;
    }

    private void StartExtract()
    {
        if (_moneyContainer != null && _peacefulConstruction.IsAlive())
        {
            if (_extract != null)
            {
                StopCoroutine(_extract);
            }
            OnExtractionAnimation();
            _extract = StartCoroutine(Extract());
        }
        else
        {
            OnOffExtractionAnimation();
        }
    }

    private IEnumerator Extract()
    {
        var waitForSecounds = new WaitForSeconds(_waitForSecounds);
        _moneyContainer.AddGold(_amountMoneyProduced);
        yield return waitForSecounds;

        if (_peacefulConstruction.IsAlive())
        {
            StartExtract();
        }
        else
        {
            OnOffExtractionAnimation();
        }
    }
}
