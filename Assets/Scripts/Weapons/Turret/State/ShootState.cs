using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : PeacefulConstructionState
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform[] _shootPoint;
    [SerializeField] private float _waitForSecounds;

    private Coroutine _shoot;

    private void OnEnable()
    {
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
        Bullet bullet = Instantiate(_bullet, shootPoint.position, Quaternion.identity);
        if (bullet != null)
        {
            bullet.Seek(Turret.TargetEnemy);

            //Debug.Log(bullet.transform.position);
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
