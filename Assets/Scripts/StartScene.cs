using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public float zoomSpeed;
    public bool isGoingNextScene;
    public float shakePower;
    public float shakeTime;
    public bool isPaused;
    public bool isCanCameraZoom;

    Camera mainCamera;
    AudioSource typingSound;
    Text exclamationMark;
    Text exclamationMark2;
    GameObject error;
    GameObject error1;
    GameObject error2;
    GameObject error3;
    GameObject error4;
    GameObject error5;
    GameObject error6;
    GameObject error7;
    GameObject error8;
    GameObject error9;
    GameObject click;
    GameObject pausedPanel;
    Vector3 initialPosition;

    void Awake()
    {
        if (Application.isEditor)
            zoomSpeed = 0.001f;
        else
            zoomSpeed = 0.005f;
        isGoingNextScene = false;
        isCanCameraZoom = true;

        mainCamera = GetComponent<Camera>();
        typingSound = GetComponent<AudioSource>();
        exclamationMark = GameObject.Find("ExclamationMark").GetComponent<Text>();
        exclamationMark2 = GameObject.Find("ExclamationMark2").GetComponent<Text>();
        error = GameObject.Find("Error");
        error1 = GameObject.Find("Error (1)");
        error2 = GameObject.Find("Error (2)");
        error3 = GameObject.Find("Error (3)");
        error4 = GameObject.Find("Error (4)");
        error5 = GameObject.Find("Error (5)");
        error6 = GameObject.Find("Error (6)");
        error7 = GameObject.Find("Error (7)");
        error8 = GameObject.Find("Error (8)");
        error9 = GameObject.Find("Error (9)");
        click = GameObject.Find("Click");
        pausedPanel = GameObject.Find("Paused");
        pausedPanel.gameObject.SetActive(false);
        exclamationMark.gameObject.SetActive(false);
        exclamationMark2.gameObject.SetActive(false);
        error.gameObject.SetActive(false);
        error1.gameObject.SetActive(false);
        error2.gameObject.SetActive(false);
        error3.gameObject.SetActive(false);
        error4.gameObject.SetActive(false);
        error5.gameObject.SetActive(false);
        error6.gameObject.SetActive(false);
        error7.gameObject.SetActive(false);
        error8.gameObject.SetActive(false);
        error9.gameObject.SetActive(false);
        click.gameObject.SetActive(false);

        initialPosition = transform.position;

        SetResolution();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                isPaused = true;
                isCanCameraZoom = false;
                typingSound.Pause();
                pausedPanel.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                isPaused = false;
                isCanCameraZoom = true;
                typingSound.UnPause();
                pausedPanel.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("Game");

        if (isCanCameraZoom)
        {
            if (mainCamera.orthographicSize > 3.25f)
                mainCamera.orthographicSize -= zoomSpeed;
            else
                mainCamera.orthographicSize = 3.25f;

            if (mainCamera.orthographicSize == 3.25f && !isGoingNextScene)
            {
                isGoingNextScene = true;
                StartCoroutine(GoNextScene());
            }
        }

        if (shakeTime > 0f)
        {
            transform.position = Random.insideUnitSphere * shakePower + initialPosition;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0f;
            transform.position = initialPosition;
        }
    }

    void VibrateForTime(float power, float time)
    {
        shakePower = power;
        shakeTime = time;
    }

    IEnumerator GoNextScene()
    {
        bool isClicked = false;
        if (!isClicked)
        {
            yield return new WaitForSeconds(0.75f);
            error.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.35f);
        exclamationMark.gameObject.SetActive(true);
        typingSound.Stop();

        yield return new WaitForSeconds(0.75f);
        click.gameObject.SetActive(true);
        isClicked = true;

        yield return new WaitForSeconds(0.25f);
        error.gameObject.SetActive(false);
        exclamationMark.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.25f);
        typingSound.Play();

        yield return new WaitForSeconds(1.75f);
        error1.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        error2.gameObject.SetActive(true);
        exclamationMark2.gameObject.SetActive(true);
        typingSound.Stop();

        yield return new WaitForSeconds(0.2f);
        error3.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        error4.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.15f);
        error5.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.15f);
        error6.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.15f);
        error7.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.15f);
        error8.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        exclamationMark2.gameObject.SetActive(false);
        error9.gameObject.SetActive(true);
        VibrateForTime(0.2f, 0.2f);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game");

        yield return null;
    }

    public void SetResolution()
    {
        int setWidth = 1920;
        int setHeight = 1080;

        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight);
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
        }
        else
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight);
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
        }
    }
}
