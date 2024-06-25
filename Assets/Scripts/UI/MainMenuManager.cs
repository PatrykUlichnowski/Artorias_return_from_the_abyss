using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
