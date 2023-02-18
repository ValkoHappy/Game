using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _templateEnemy;
    [SerializeField] private Enemy _templateMiniBossEnemy;
    [SerializeField] private int _spawnCount;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Vector2Int _spawnPoint;
    [SerializeField] private Transform _conteiner;

    private Coroutine _coroutine;
    private int number = 25;

    int numderRundom =100;

    public void StartSpawn()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        if (_spawnCount > 0)
        {
            numderRundom = Random.Range(1, 100);
            if (number <= numderRundom)
            {
                Instantiate(_templateMiniBossEnemy, SpawnPosition(), Quaternion.identity, _conteiner);
            }
            else if(number >= numderRundom)
            {
                Instantiate(_templateEnemy, SpawnPosition(), Quaternion.identity, _conteiner);
            }
            numderRundom = 0;
            _spawnCount--;
            yield return new WaitForSeconds(_spawnDelay);
        }

        StartSpawn();
    }

    private Vector3 SpawnPosition()
    {
        return new Vector3(transform.position.x + Random.Range(_spawnPoint.x, _spawnPoint.y), transform.position.y, transform.position.z);
    }
}
