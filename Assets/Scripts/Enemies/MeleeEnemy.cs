using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("attack params")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;

    [Header("collider params")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("player params")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("attack sound")]
    [SerializeField] private AudioClip attackSound;

    private Health playerHealth;
    private Animator animator;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();  
    }

    private void OnDisable()
    {
        animator.SetBool("Moving", false);
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                animator.SetTrigger("Attack");
                SoundManager.instance.PlaySound(attackSound);
            }
        }
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight(); //if you see the player stop patrolling
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
                boxCollider.bounds.center + (transform.right * range * -transform.localScale.x * colliderDistance),
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), //so that we can adjust the size of the box
                0, Vector2.left, 0, playerLayer
            );
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        // gizmos are boxes that help with debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
                boxCollider.bounds.center + transform.right * range * -transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
            );
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
