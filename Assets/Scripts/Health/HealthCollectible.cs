using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [Header("sfx")]
    [SerializeField] private AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealt(healthValue);
            gameObject.SetActive(false);
            SoundManager.instance.PlaySound(sound);
        }
    }
}
