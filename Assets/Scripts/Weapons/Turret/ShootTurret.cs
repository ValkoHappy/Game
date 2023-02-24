using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class ShootTurret : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform[] _shootPoint;
    [SerializeField] private float _waitForSecounds;
    [SerializeField] private ParticleSystem _particleShoot;

    private Turret _turret;
    private RecoilAnimation _recoilAnimation;
    private Coroutine _shoot;
    private bool _isShoot = true;
    private bool _isShoott = true;


    private void Awake()
    {
        _turret = GetComponent<Turret>();
        _recoilAnimation = GetComponentInChildren<RecoilAnimation>();
    }

    public void StartShoot()
    {
        if (_turret.TargetEnemy != null && _isShoot == true && _turret.Construction.IsAlive())
        {
            _isShoott = false;
            if (_shoot != null)
            {
                StopCoroutine(_shoot);
            }
            _shoot = StartCoroutine(Shoot());
        }
        else
        {
            if (_shoot != null)
            {
                StopCoroutine(_shoot);
            }
        }
    }

    public void StopShoot()
    {
        _isShoot = false;
    }

    public void RestartShoot()
    {
        _isShoot = true;
    }

    public void CreateBullet(Transform shootPoint)
    {
        Bullet bullet = Instantiate(_bullet, shootPoint.position, Quaternion.identity);
        _particleShoot.transform.position = shootPoint.position;
        //_particleShoot.Play();
        Instantiate(_particleShoot, shootPoint.position, Quaternion.identity);
        bullet.Seek(_turret.TargetEnemy.transform);
    }

    private IEnumerator Shoot()
    {
        var waitForSecounds = new WaitForSeconds(_waitForSecounds);

        for (int i = 0; i < _shootPoint.Length; i++)
        {
            CreateBullet(_shootPoint[i]);
        }
        _recoilAnimation.StartRecoil(_waitForSecounds);
        yield return waitForSecounds;
        StartShoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyCollision enemy))
        {
            if (enemy != null)
            {
                if (_turret.Construction.IsAlive())
                {
                    _isShoot = true;
                    if (_isShoott)
                        StartShoot();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out EnemyCollision enemy))
        {
            if (enemy != null)
            {
                _isShoot = false;
            }
        }
    }
}
