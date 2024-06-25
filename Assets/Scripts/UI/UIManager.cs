using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("gameover")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [Header("pause")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject winScreen;

    private bool playerWon;

    private void Awake()
    {
        PauseGame(false);
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
        if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) && playerWon)
               Restart();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    //game over functions

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit(); // works in build
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // works in unity editor
    #endif
    }

    public void PauseGame(bool _status)
    {
        pauseScreen.SetActive(_status);

        if (_status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void Win()
    {
        playerWon = true;
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

}
