using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangeController : MonoBehaviour
{

    [Header("Targeting")]

    [SerializeField]
    Transform target;

    [SerializeField]
    float sightRange;

    [SerializeField]
    float attackRange;

    [SerializeField]
    float attackTimeout;

    NavMeshAgent _navAgent;

    LayerMask _whatIsTarget;

    Vector3 _walkPoint;

    bool _isTargetInSightRange;
    bool _isTargetInAttackRange;
    bool _isAttacking;

    [Header("Gun")]

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform firePoint;

    [SerializeField]
    Vector3 bulletRotation;

    [SerializeField]
    float bulletSpeed = 50.0F;

    [SerializeField]
    float lifeTime = 5.0F;

    [SerializeField]
    int maximumAmmunition = 16;

    [SerializeField]
    float realoadTime = 2.0F;

    [SerializeField]
    int fireRate = 4;

    Animator animator;

    float _currentAmmunition = -1;

    float _nextFireTime = 0.0F;

    bool _isReloading = false;

    void Awake()
    {
        _whatIsTarget = LayerMask.GetMask("Player");
        _navAgent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isReloading)
        {
            return;
        }

        if (_currentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Time.time > _nextFireTime && _isAttacking)
        {
            _nextFireTime = Time.time + 1.0F / fireRate;
            Shoot();
        }
    }

    void FixedUpdate()
    {
        _isTargetInSightRange = Physics.CheckSphere(transform.position, sightRange, _whatIsTarget);
        _isTargetInAttackRange = Physics.CheckSphere(transform.position, attackRange, _whatIsTarget);

        if (_isTargetInAttackRange)
        {
            HandleAttack();
        }
        else if (_isTargetInSightRange)
        {
            HandleChase();
        }
    }

    void Start()
    {
        if (_currentAmmunition == -1)
        {
            _currentAmmunition = maximumAmmunition;
        }
    }

    #region Targeting

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void HandleChase()
    {
        _navAgent.SetDestination(target.position);
    }

    void HandleAttack()
    {
        _navAgent.SetDestination(transform.position);
        transform.LookAt(target);

        if (!_isAttacking)
        {
            _isAttacking = true;
            Invoke(nameof(ResetAttack), attackTimeout);
        }
    }

    void ResetAttack()
    {
        _isAttacking = false;
    }

    #endregion

    #region Gun

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(bulletRotation));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.velocity = firePoint.forward * bulletSpeed;

        Destroy(bullet, lifeTime);

        _currentAmmunition--;
    }

    IEnumerator Reload()
    {
        _isReloading = true;

        //animator.SetBool("reload", true);

        yield return new WaitForSeconds(realoadTime);

        _currentAmmunition = maximumAmmunition;

        //animator.SetBool("reload", false);
        _isReloading = false;
    }

    #endregion
}
