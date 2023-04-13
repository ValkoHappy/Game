using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(FoundBuildings))]
[RequireComponent(typeof(AttackState), typeof(ApproachedObjectTransition), typeof(LostObjectTransition))]
[RequireComponent(typeof(NavMeshAgent), typeof(BrokenState), typeof(ChaseState))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyState _firstState;
    [SerializeField] private BrokenState _brokenState;

    private FoundBuildings _foundBuildings;
    private EnemyState _currentState;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private HealthContainer _healthContainer;

    private PeacefulConstruction _targetConstruction;

    public event UnityAction<Enemy> Died;

    public EnemyState CurrentState => _currentState;
    public EnemyState BrokenState => _brokenState;

    private void OnEnable()
    {
        _healthContainer.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        _healthContainer.Died -= OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        Died?.Invoke(this);
        enabled = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
    }

    private void Awake()
    {
        _foundBuildings = GetComponent<FoundBuildings>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _targetConstruction = FindObjectOfType<PeacefulConstruction>();
        _healthContainer = GetComponentInChildren<HealthContainer>();
    }

    private void Start()
    {
        _currentState = _firstState;
        _targetConstruction = _foundBuildings.TargetConstruction;
        _currentState.Enter(_targetConstruction, _animator, _rigidbody);
    }

    private void Update()
    {
        _targetConstruction = _foundBuildings.TargetConstruction;

        foreach (var transition in _currentState.Transitions)
        {
            transition.enabled = true;
            transition.Init(_targetConstruction);
        }

        if (_currentState == null)
            return;

        EnemyState nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);

        if(_healthContainer.Health <= 0 && _currentState != _brokenState)
        {
            Transit(_brokenState);
        }
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_targetConstruction, _animator, _rigidbody);
    }

    public void ApplayDamage(Rigidbody rigidbody, int damage, int force)
    {
        if (_currentState != _brokenState)
        {
            _healthContainer.TakeDamage(damage);
            if(_healthContainer.Health <= 0)
            {
                Transit(_brokenState);
                _brokenState.ApplyDamage(rigidbody, force);
            }
        }
    }
}
