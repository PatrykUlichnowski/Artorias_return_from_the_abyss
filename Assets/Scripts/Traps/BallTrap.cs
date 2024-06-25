 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] balls;
    private float cooldownTimer;

    [Header("sfx")]
    [SerializeField] private AudioClip sound;

    private void Attack()
    {
        cooldownTimer = 0;
        SoundManager.instance.PlaySound(sound);
        balls[FindBall()].transform.position = firePoint.position;
        balls[FindBall()].GetComponent<EnemyProjectile>().ActiveProjectile();
    }

    private int FindBall()
    {
        //searches for the first available ball in the hierarchy
        for (int i = 0; i < balls.Length; i++)
        {
            if (!balls[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}
