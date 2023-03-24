using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Turret))]
public class ShootTurret : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private bool _isLaser = false;
    [SerializeField] private Transform[] _shootPoint;
    [SerializeField] private float _waitForSeconds;
    [SerializeField] private int _yOffset = 1;
    [SerializeField] private ParticleSystem _particleShoot;

    private Turret _turret;
    private RecoilAnimation _recoilAnimation;
    private Coroutine _shootCoroutine;
    private bool _canShoot = true;
    public bool _canShooting = false;


    private void Awake()
    {
        _turret = GetComponent<Turret>();
        _recoilAnimation = GetComponentInChildren<RecoilAnimation>();
    }

    private void Update()
    {
        if (_canShoot)
        {
            if (_canShooting == false)
            {
                StartShoot();
            }
        }

        if (_turret.TargetEnemy == null)
        {
            _canShooting = false;
        }
    }

    public void StartShoot()
    {
        if (_turret.TargetEnemy != null && _canShoot && _turret.Construction.IsAlive())
        {
            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
            }
            _shootCoroutine = StartCoroutine(Shoot());
        }
    }

    public void StopShoot()
    {
        _canShoot = false;
    }

    public void RestartShoot()
    {
        _canShoot = true;
    }

    public void CreateBullet(Transform shootPoint)
    {
        Bullet bullet = Instantiate(_bullet, shootPoint.position, shootPoint.rotation);
        _particleShoot.transform.position = shootPoint.position;
        Instantiate(_particleShoot, shootPoint.position, Quaternion.identity);
        bullet.transform.LookAt(new Vector3(_turret.TargetEnemy.transform.position.x, _turret.TargetEnemy.transform.position.y + _yOffset, _turret.TargetEnemy.transform.position.z));
    }

    private IEnumerator Shoot()
    {
        var waitForSeconds = new WaitForSeconds(_waitForSeconds);
        _canShooting = true;
            for (int i = 0; i < _shootPoint.Length; i++)
            {
                CreateBullet(_shootPoint[i]);
            }
            _recoilAnimation.StartRecoil(_waitForSeconds);
        yield return waitForSeconds;
        _canShooting = false;
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
                    _canShoot = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyCollision enemy))
        {
            if (enemy != null)
            {
                _canShoot = false;
            }
        }
    }
}



