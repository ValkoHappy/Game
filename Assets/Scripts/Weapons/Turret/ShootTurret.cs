using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Turret))]
public class ShootTurret : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform[] _shootPoint;
    [SerializeField] private float _waitForSeconds;
    [SerializeField] private ParticleSystem _particleShoot;

    //public LineRenderer _lineRenderer;
    private Turret _turret;
    private RecoilAnimation _recoilAnimation;
    private Coroutine _shootCoroutine;
    private bool _canShoot = true;
    private bool _canStartShoot = true;


    private void Awake()
    {
        _turret = GetComponent<Turret>();
        //_lineRenderer= GetComponent<LineRenderer>();
        _recoilAnimation = GetComponentInChildren<RecoilAnimation>();
    }

    public void StartShoot()
    {
        if (_turret.TargetEnemy != null && _canShoot && _turret.Construction.IsAlive())
        {
            _canStartShoot = false;
            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
            }
            _shootCoroutine = StartCoroutine(Shoot());
        }
        else
        {
            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
            }
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
        Bullet bullet = Instantiate(_bullet, shootPoint.position, Quaternion.identity);
        _particleShoot.transform.position = shootPoint.position;
        Instantiate(_particleShoot, shootPoint.position, Quaternion.identity);
        bullet.Seek(_turret.TargetEnemy.transform);
    }

    private IEnumerator Shoot()
    {
        var waitForSeconds = new WaitForSeconds(_waitForSeconds);

        for (int i = 0; i < _shootPoint.Length; i++)
        {
            CreateBullet(_shootPoint[i]);
            //_lineRenderer.SetPosition(0, _shootPoint[i].position);
            //_lineRenderer.SetPosition(1, _turret.TargetEnemy.transform.position);
        }
        _recoilAnimation.StartRecoil(_waitForSeconds);
        yield return waitForSeconds;
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
                    if (_canStartShoot)
                        StartShoot();
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


