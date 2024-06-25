using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Healt")] //header for unity UI
    [SerializeField] private float startingHealth;
     
    public float currentHealth { get; private set; }/* get;private set ==== you can fetch (get) the value of the variable to another class,
    but you can only set it within this class */
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("components to disable after death")]
    [SerializeField] private Behaviour[] components;

    [Header("death sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);//keeping the health withing the range of 0 and starting (max) value

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            //print("damage");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);

        }
        else 
        {
            //when player has died
            if (!dead)
            {
                    
                //deactivate all objects that are attached
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                anim.SetBool("Grounded", true);
                anim.SetTrigger("Death");

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }

    public void AddHealt (float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true); //player is on layer 10, while enemies are on layer 11.
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2)); // multiple by two cause we have 2 delays. one here
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2)); // and one here
        }   
        Physics2D.IgnoreLayerCollision(10, 11, false); 
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
