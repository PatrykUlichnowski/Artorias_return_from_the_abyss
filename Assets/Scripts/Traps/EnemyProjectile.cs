using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage // inheritance from enemy damage class
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private float resetTime;
    private float lifetime;
    public void ActiveProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        float movementSpeed = attackSpeed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        { 
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); //executes parent script, cause there is also OnTriggerEnter2D in the EnemyDamage class
        gameObject.SetActive(false); // ball deactivate if it touches anything
    }
}
