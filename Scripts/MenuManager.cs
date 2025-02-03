using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameObject player;
    bool gameIsPaused;
    bool gameStarted;

    [SerializeField] GameObject panel;
    [SerializeField] GameObject pauseMenu;
 
    void Start()
    {
        // stop game from playing
        Time.timeScale = 0f;
        // to stop the player script
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FirstPersonController>().enabled = false;

        gameIsPaused = false;
        
    }
    private void Update()
    {
        Debug.Log(gameStarted);
        if (Input.GetKeyDown(KeyCode.Tab) && gameStarted == true)
        {
            // sto that if the tab key is clicked two times the game resumes
            gameIsPaused = !gameIsPaused;

            // to avoid seeing the curson while playing
            Cursor.visible = !Cursor.visible;
            if (gameIsPaused == true)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void StartGame()
    {
        //if (gameStarted == true)
        //{
           //SceneManager.LoadScene(0);
        //}
        Cursor.visible = false;
        Time.timeScale = 1f;
        player.GetComponent<FirstPersonController>().enabled = true;
        panel.SetActive(false);
        gameStarted = true;
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        // stop game from playing
        Time.timeScale = 0f;
        player.GetComponent<FirstPersonController>().enabled = false;

        panel.SetActive(true);
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        Cursor.visible = false;
        Time.timeScale = 1f;
        player.GetComponent<FirstPersonController>().enabled = true;

        panel.SetActive(false);
        pauseMenu.SetActive(false);
    }
    
    public void ExitGame()
    {
        Application.Quit();
        // for the editor
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void NoOverLappingMenus()
    {
        gameStarted = false;
    }
    public void NoExitGame()
    {
        gameStarted = true;
    }
}
