using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyState _firstState;

    private EnemyState _currentState;
    private Animator _animator;
    private List<PeacefulConstruction> _constructions = new List<PeacefulConstruction>();

    [SerializeField] public PeacefulConstruction PeacefulConstruction;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        PeacefulConstruction = FindObjectOfType<PeacefulConstruction>();
    }

    private void Start()
    {
        _currentState = _firstState;
        SortEnemies();
        _currentState.Enter(PeacefulConstruction, _animator);
    }

    private void Update()
    {
        SortEnemies();

        if (_currentState == null)
            return;

        EnemyState nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(PeacefulConstruction, _animator);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PeacefulConstruction peacefulConstruction))
        {
            if (peacefulConstruction != null && peacefulConstruction.IsAlive())
            {
                _constructions.Add(peacefulConstruction);
            }
        }
    }

    private void SortEnemies()
    {
        if (_constructions.Count > 0 || PeacefulConstruction != null)
        {
            float shortestDistance = Mathf.Infinity;
            PeacefulConstruction nearestEnemy = null;

            foreach (var construction in _constructions)
            {
                float distanceToConstruction = Vector3.Distance(transform.position, construction.transform.position);

                if (distanceToConstruction < shortestDistance)
                {
                    shortestDistance = distanceToConstruction;
                    nearestEnemy = construction;
                }


                if (nearestEnemy != null && nearestEnemy.IsAlive())
                {
                    PeacefulConstruction = nearestEnemy;
                }
            }
        }
    }
}
