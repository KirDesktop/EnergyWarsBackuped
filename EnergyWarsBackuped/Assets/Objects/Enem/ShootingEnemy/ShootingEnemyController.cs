using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemyController : HealthScript
{
    [SerializeField] private EnemyDetector _detector;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _shootingPos;
    [SerializeField] private GameObject _enemyBullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletlifetime = 6f;
    [SerializeField] private float _spread;
    [SerializeField] private float _shootingDuration;
    private float _timeToShoot;

     private GameObject _player;

    [SerializeField] private bool _isPlayer = false;

    private void Update()
    {
        if(health <= 0)
        {
            _destroyThis();
        }

        if (_detector.isPlayer)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_detector.player.transform.position);
            //Debug.Log(Time.time - _timeToShoot);
            if(Time.time >= _timeToShoot)
            {
                Debug.Log("Shoot");
                _timeToShoot = Time.time + _shootingDuration;

                shoot();
            }
            this.transform.LookAt(_detector.player.transform.position);
            this.transform.rotation = Quaternion.Euler(0f, this.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            _agent.isStopped = true;
        }
    }

    private void shoot()
    {
        GameObject shooted = Instantiate(_enemyBullet, _shootingPos.position, this.transform.rotation);
        shooted.transform.LookAt(_detector.player.transform.position);
        shooted.GetComponent<Rigidbody>().AddForce(_bulletSpeed * shooted.transform.forward + new Vector3(Random.Range(-_spread, +_spread), Random.Range(-_spread, +_spread), Random.Range(-_spread, +_spread)), ForceMode.VelocityChange);
        Destroy(shooted, _bulletlifetime);
    }
}
