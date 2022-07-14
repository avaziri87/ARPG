using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] List<EnemyLocomotion> _enemyPoolList = new List<EnemyLocomotion>();

    PlayerLocomotion _playerLocomotion;

    private void Start()
    {
        _playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    void GetNewTarget()
    {
        if(_enemyPoolList.Count >0)
        {
            int randomIndex = Random.Range(0, _enemyPoolList.Count);
            _playerLocomotion.CurrentTarget = _enemyPoolList[randomIndex];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyLocomotion newEnemyLocomotion = other.GetComponent<EnemyLocomotion>();
            if (_playerLocomotion.CurrentTarget == null)
            {
                _playerLocomotion.CurrentTarget = newEnemyLocomotion;
            }
            else if (_enemyPoolList.Contains(newEnemyLocomotion) == false && _playerLocomotion.CurrentTarget != newEnemyLocomotion)
            {
                _enemyPoolList.Add(newEnemyLocomotion);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyLocomotion newEnemyLocomotion = other.GetComponent<EnemyLocomotion>();
            if(_playerLocomotion.CurrentTarget == newEnemyLocomotion)
            {
                GetNewTarget();
            }
            else
            {
                _enemyPoolList.Remove(newEnemyLocomotion);
            }
        }
    }
}
