using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLocomotion : MonoBehaviour
{
    [SerializeField] float _projectileSpeed = 10.0f;
    [SerializeField] float _damage = 1.0f;

    private void Update()
    {
        transform.Translate(Vector3.forward * _projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;

        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(_damage);
        }

        Destroy(this.gameObject);
    }
}
