using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float fireTrapDamage;
    [Header("Firetrap Timers")]
    [SerializeField] private float activeDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    [Header("sfx")]
    [SerializeField] private AudioClip sound;

    private Health player;

    private bool triggered, active;
    private bool load;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
                load = true;
            }

            player = collision.GetComponent<Health>(); // we assign the player variable when it connects to the trap
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null; // this is needed cause the player can exit the collision to escape the damage
    }

    private void Update()
    {
        if (active && player != null && load)
        {
            player.TakeDamage(fireTrapDamage); // player takes damage when the firetrap is active AND he didnt escape collision
            load = false; // otherwise player would take dmg every frame which is not ideal
        }
       
    }

    private IEnumerator ActivateFireTrap()
    {
        triggered = true;
        spriteRenderer.color = Color.red; // gives player a clue that he activated the trap

        yield return new WaitForSeconds(activeDelay);
        SoundManager.instance.PlaySound(sound);
        spriteRenderer.color = Color.white;
        active = true;
        anim.SetBool("Activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("Activated", false);
    }
}
