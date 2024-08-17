using System.Collections;
using Scripts.Enemy;
using UnityEngine;

namespace Scripts.Weapons.Turret
{
    public class AttackTurret : MonoBehaviour
    {
        [SerializeField] private Bullet.Bullet _bullet;
        [SerializeField] private Transform[] _shootPoint;
        [SerializeField] private float _waitForSeconds;
        [SerializeField] private float _radiusAttack;
        [SerializeField] private float _yOffset = 1;
        [SerializeField] private ParticleSystem _particleShoot;

        private SphereCollider _sphereCollider;
        private Turret _turret;
        private EnemyHandler _enemyHandler;
        private RecoilAnimation _recoilAnimation;
        private Coroutine _shootCoroutine;
        private WaitForSeconds _shootWait;
        private bool _canShoot = true;
        private bool _canShooting = false;

        public float ShootDelay => _waitForSeconds;
        public float Damage => _shootPoint.Length;
        public float RadiusAttack => _radiusAttack;

        private void Awake()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _enemyHandler = FindObjectOfType<EnemyHandler>();
            _turret = GetComponent<Turret>();
            _recoilAnimation = GetComponentInChildren<RecoilAnimation>();
            _shootWait = new WaitForSeconds(_waitForSeconds);
        }

        private void Start()
        {
            _sphereCollider.radius = _radiusAttack;
        }

        private void Update()
        {
            if (_enemyHandler.IsAttackBegun)
            {
                if (_canShoot)
                {
                    if (_canShooting == false)
                        StartShoot();
                }

                if (_turret.TargetEnemy == null)
                    _canShooting = false;

                if (_turret.TargetEnemy != null && _turret.TargetEnemy.IsAlive)
                    Restart();
                else
                    Stop();
            }
            else
            {
                _canShoot = false;
            }
        }

        public void StartShoot()
        {
            if (_turret.TargetEnemy != null && _canShoot && _turret.Construction.IsAlive)
            {
                if (_shootCoroutine != null)
                    StopCoroutine(_shootCoroutine);

                _shootCoroutine = StartCoroutine(Shoot());
            }
        }

        public void Stop()
        {
            _canShoot = false;
        }

        public void Restart()
        {
            _canShoot = true;
        }

        public void CreateBullet(Transform shootPoint)
        {
            Bullet.Bullet bullet = Instantiate(_bullet, shootPoint.position, shootPoint.rotation);
            _particleShoot.transform.position = shootPoint.position;
            Instantiate(_particleShoot, shootPoint.position, Quaternion.identity);

            bullet.transform.LookAt(
                new Vector3(
                    _turret.TargetEnemy.transform.position.x,
                    _turret.TargetEnemy.transform.position.y + _yOffset,
                    _turret.TargetEnemy.transform.position.z));
        }

        private IEnumerator Shoot()
        {
            _canShooting = true;

            for (int i = 0; i < _shootPoint.Length; i++)
            {
                CreateBullet(_shootPoint[i]);
            }

            _recoilAnimation.StartRecoil(_waitForSeconds);

            yield return _shootWait;

            _canShooting = false;

            if (_turret.TargetEnemy.IsAlive)
                StartShoot();
        }
    }
}