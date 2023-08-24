using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBossController : MonoBehaviour
{
    [Header("Targeting")]

    [SerializeField]
    Transform target;

    [SerializeField]
    float rotateSpeed;

    [Header("Attack")]
    [SerializeField]
    float attackRange;

    [SerializeField]
    Transform attackPoint;

    [SerializeField]
    float attackRadius = 2.0F;

    [SerializeField]
    LayerMask _whatIsTarget;

    [SerializeField]
    float damage = 25.0F;

    [SerializeField]
    int attackRate = 2;

    [SerializeField]
    float attackTimeout;

    float _nextAttackTime = 0.0F;

    Quaternion targetRotation;

    bool _isTargetInAttackRange;
    bool _isAttacking;

    Animator _animator;
    NavMeshAgent _navAgent;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time > _nextAttackTime && _isAttacking)
        {
            _nextAttackTime = Time.time + 1.0F / attackRate;
            OnAttack();
        }

        targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
    }

    void FixedUpdate()
    {
        _isTargetInAttackRange = Physics.CheckSphere(transform.position, attackRange, _whatIsTarget);

        if (_isTargetInAttackRange)
        {
            HandleAttack();
        }
    }

    #region Targeting

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void OnAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(attackPoint.position, attackRadius, _whatIsTarget);

        foreach (Collider collider in colliders)
        {
            HealthController controller = collider.GetComponent<HealthController>();
            if (controller != null)
            {
                controller.TakeDamage(damage);
            }
        }
        //_animator.ResetTrigger("attack");
    }

    void HandleAttack()
    {
        //transform.LookAt(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

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
}
