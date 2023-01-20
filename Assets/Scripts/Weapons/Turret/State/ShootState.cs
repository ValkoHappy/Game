using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class ShootState : PeacefulConstructionState
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform[] _shootPoint;
    [SerializeField] private float _waitForSecounds;

    private Turret _turret;
    private Coroutine _shoot;

    private void Start()
    {
        _turret = GetComponent<Turret>();
        StartShoot();
    }

    private void StartShoot()
    {
        if (_shoot != null)
        {
            StopCoroutine(_shoot);
        }
        _shoot = StartCoroutine(Shoot());
    }

    public void CreateBullet(Transform shootPoint)
    {
        Bullet bullet = Instantiate(_bullet, shootPoint.position, Quaternion.identity, transform);

        if(bullet != null)
        {
            bullet.Seek(_turret.TargetEnemy);
        }
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
}
