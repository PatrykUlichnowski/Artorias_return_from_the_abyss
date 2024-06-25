using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("movement")]
    [SerializeField] private float enemySpeed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("idle begaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;


    [Header("enemy animator")]
    [SerializeField] private Animator animator;


    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        animator.SetBool("Moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction * -1, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * enemySpeed,
            enemy.position.y, enemy.position.z);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        idleTimer += Time.deltaTime;
        animator.SetBool("Moving", false);
        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;   
    }
}
