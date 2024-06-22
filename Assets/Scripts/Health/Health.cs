using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        //keeping the health withing the range of 0 and starting (max) value
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            print("damage");
        }
        else 
        {
            //when player has died
            if (!dead)
            {
                anim.SetTrigger("Death");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }
}
