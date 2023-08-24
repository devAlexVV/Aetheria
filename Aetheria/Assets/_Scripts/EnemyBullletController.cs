using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullletController : MonoBehaviour
{
    [SerializeField]
    float speed = 500.0F;

    [SerializeField]
    float damage = 25.0F;

    Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthController controller = other.GetComponent<HealthController>();
            if (controller != null)
            {
                controller.TakeDamage(damage);
            }
        }
    }
}
