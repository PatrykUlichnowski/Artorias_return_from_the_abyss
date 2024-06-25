using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SifScript : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Sif")
        {
            Debug.Log("dziala");
            uiManager.Win();
        }
    }
}
