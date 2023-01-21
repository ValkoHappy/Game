using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _templateEnemy;
    [SerializeField] private int _spawnCount;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Vector2Int _spawnPoint;
    [SerializeField] private Transform _conteiner;

    public PeacefulConstruction[] Constructions { get; private set; }

    private void Awake()
    {
        Constructions = FindObjectsOfType<PeacefulConstruction>();
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while(_spawnCount >= 0)
        {
            _spawnCount--;
            Instantiate(_templateEnemy, SpawnPosition(), Quaternion.identity, _conteiner);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private Vector3 SpawnPosition()
    {
        return new Vector3(transform.position.x + Random.Range(_spawnPoint.x, _spawnPoint.y), transform.position.y, transform.position.z);
    }
}
