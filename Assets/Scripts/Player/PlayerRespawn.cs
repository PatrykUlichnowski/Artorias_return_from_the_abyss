using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager UIManager;

    private void Awake()
    {
        UIManager = FindObjectOfType<UIManager>(); //returns first result of type form hierarchy
        playerHealth = GetComponent<Health>();
    }
    public void CheckRespawn()
    {
        // check if checkpoint is available
        if (currentCheckpoint == null)
        {
            UIManager.GameOver(); //shows game over screen
            return; // gameover if there is no check point
        }

        transform.position = currentCheckpoint.position; //get player back to checkpoint
        playerHealth.Respawn(); //restores hp 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}
