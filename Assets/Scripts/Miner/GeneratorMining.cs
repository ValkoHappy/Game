using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PeacefulConstruction), typeof(Animator))]
public class GeneratorMining : MonoBehaviour
{
    private const string Extraction = "Extraction";

    [SerializeField] private int _amountMoneyProduced;
    [SerializeField] private float _waitForSecounds;

    private GoldContainer _moneyContainer;
    private Coroutine _extract;
    private PeacefulConstruction _peacefulConstruction;
    private HealthHandler _healthContainer;
    private Animator _animator;
    private Building _building;
    private BuildingsGrid _buildingGrid;

    private WaitForSeconds _extractWait;

    public int AmountMoneyProduced => _amountMoneyProduced;

    private void Awake()
    {
        _building = GetComponentInParent<Building>();
        _moneyContainer = FindObjectOfType<GoldContainer>();
        _buildingGrid = FindObjectOfType<BuildingsGrid>();
        _peacefulConstruction = GetComponent<PeacefulConstruction>();
        _healthContainer = GetComponent<HealthHandler>();
        _animator = GetComponent<Animator>();
        _extractWait = new WaitForSeconds(_waitForSecounds);
    }

    private void OnEnable()
    {
        _building.Delivered += OnExtractionAnimation;
        _buildingGrid.BuildingDelivered += OnExtractionAnimation;
        _buildingGrid.BuildingDelivered += OnStartExtract;
        _peacefulConstruction.Died += OnOffExtractionAnimation;
        _peacefulConstruction.BuildingRestored += OnStartExtract;
        _healthContainer.Died += OnOffExtractionAnimation;
    }

    private void OnDisable()
    {
        _building.Delivered -= OnExtractionAnimation;
        _buildingGrid.BuildingDelivered -= OnExtractionAnimation;
        _buildingGrid.BuildingDelivered -= OnStartExtract;
        _peacefulConstruction.Died -= OnOffExtractionAnimation;
        _peacefulConstruction.BuildingRestored -= OnStartExtract;
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

    private void OnStartExtract()
    {
        if (_moneyContainer != null && _peacefulConstruction.IsAlive())
        {
            if (_extract != null)
            {
                StopCoroutine(_extract);
                _extract = null;
            }

            OnExtractionAnimation();
            _extract = StartCoroutine(Extract());
        }
        else
            OnOffExtractionAnimation();
    }

    private IEnumerator Extract()
    {
        _moneyContainer.Add(_amountMoneyProduced);

        yield return _extractWait;

        if (_peacefulConstruction.IsAlive())
            OnStartExtract();
        else
            OnOffExtractionAnimation();
    }
}
