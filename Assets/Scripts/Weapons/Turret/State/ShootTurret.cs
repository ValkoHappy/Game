using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class ShootTurret : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform[] _shootPoint;
    [SerializeField] private float _waitForSecounds;

    private Turret _turret;
    private Coroutine _shoot;
    private bool _isShoot = true;


    private void Awake()
    {
        _turret = GetComponent<Turret>();
    }

    public void StartShoot()
    {  
        if(_turret.TargetEnemy != null && _isShoot == true)
        {
            if (_shoot != null)
            {
                StopCoroutine(_shoot);
            }
            _shoot = StartCoroutine(Shoot());
            _isShoot =true;
        }
    }

    public void StopShoot()
    {
        _isShoot = false;
    }

    public void CreateBullet(Transform shootPoint)
    {
        Bullet bullet = Instantiate(_bullet, shootPoint.position, Quaternion.identity);
        bullet.Seek(_turret.TargetEnemy.transform);
    }

    private IEnumerator Shoot()
    {
        var waitForSecounds = new WaitForSeconds(_waitForSecounds);

        for (int i = 0; i < _shootPoint.Length; i++)
        {
            CreateBullet(_shootPoint[i]);
        }

        yield return waitForSecounds;
        StartShoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy != null)
            {
                _isShoot = true;
            }
            StartShoot();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            if (enemy != null)
            {
                _isShoot = false;
            }
        }
    }
}
