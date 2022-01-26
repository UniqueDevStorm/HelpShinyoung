using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    StartScene startScene;
    AudioSource typingSound;
    Game game;
    GameCamera gameCamera;
    GameObject pausedPanel;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            game = GameObject.Find("Game").GetComponent<Game>();
            gameCamera = GameObject.Find("Main Camera").GetComponent<GameCamera>();
        }
        else
        {
            startScene = GameObject.Find("Main Camera").GetComponent<StartScene>();
            typingSound = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        }
        pausedPanel = GameObject.Find("Paused");
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickQuit()
    {
        if (!Application.isEditor)
            Application.Quit();
    }

    public void OnClickContinue()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            Time.timeScale = 1f;
            game.isPaused = false;
            gameCamera.isCanCameraShake = true;
            pausedPanel.gameObject.SetActive(false);
            game.isDelaying = true;
        }
        else
        {
            Time.timeScale = 1f;
            startScene.isPaused = false;
            startScene.isCanCameraZoom = true;
            typingSound.UnPause();
            pausedPanel.gameObject.SetActive(false);
        }
    }
}
