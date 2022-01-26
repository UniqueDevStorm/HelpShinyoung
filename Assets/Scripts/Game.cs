using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public float fadeTime;
    public bool fadeIsPlaying;
    public int stageLevel;
    public bool isPaused;
    public bool isDelaying;
    public float timer, delayTime;

    GameObject itemParent;
    GameObject item;
    GameObject algorithm;
    GameObject bug;
    GameObject idea;
    GameObject gameOverPanel;
    GameObject pausedPanel;
    GameCamera gameCamera;
    Text score_GameOver;
    Text stage_GameOver;
    Image fadeImage;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        fadeTime = 1f;
        fadeIsPlaying = false;
        stageLevel = 1;
        isPaused = false;
        isDelaying = false;
        timer = 0f;
        delayTime = 0.15f;

        gameOverPanel = GameObject.Find("GameOver");
        gameOverPanel.gameObject.SetActive(false);
        pausedPanel = GameObject.Find("Paused");
        pausedPanel.gameObject.SetActive(false);
        gameCamera = GameObject.Find("Main Camera").GetComponent<GameCamera>();
        score_GameOver = gameOverPanel.transform.Find("Score_GameOver").GetComponent<Text>();
        stage_GameOver = gameOverPanel.transform.Find("Stage_GameOver").GetComponent<Text>();
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        fadeImage.gameObject.SetActive(false);
        itemParent = GameObject.Find("Item");
        player = GameObject.Find("ShinYoung").GetComponent<Player>();

        SetResolution();

        StartCoroutine(StartGame(stageLevel));
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOverPanel.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    Time.timeScale = 0f;
                    isPaused = true;
                    gameCamera.isCanCameraMove = false;
                    gameCamera.isCanCameraShake = false;
                    pausedPanel.gameObject.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1f;
                    isPaused = false;
                    gameCamera.isCanCameraShake = true;
                    pausedPanel.gameObject.SetActive(false);
                    isDelaying = true;
                }
            }

            if (isDelaying)
            {
                timer += Time.deltaTime;
                if (timer >= delayTime)
                {
                    gameCamera.isCanCameraMove = true;
                    timer = 0f;
                    isDelaying = false;
                }
            }
        }
    }

    IEnumerator StartGame(int level)
    {
        player.stage = stageLevel;
        while (player.isStarted)
        {
            if (player.time <= 1f)
            {
                player.time = 30f;
                break;
            }

            if (player.life <= 0f)
                break;

            if (level >= 10)
                level = Random.Range(8, 11);

            yield return new WaitForSeconds((float)level / (float)(level * level) + 0.2f);

            float ran = Random.Range(0f, 99f);
            if (ran < 62.5f - ((float)level * 0.5f))
            {
                int ran2 = Random.Range(0, 3);
                if (ran2 == 0)
                {
                    item = GameObject.Find("AlgorithmOriginal");
                    algorithm = new GameObject("Algorithm");
                    algorithm.transform.parent = itemParent.transform;
                    algorithm.transform.position = new Vector3(-20f, 3.75f, 0f);
                    algorithm.AddComponent<SpriteRenderer>();
                    algorithm.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                    algorithm.AddComponent<CircleCollider2D>();
                    algorithm.AddComponent<Rigidbody2D>();
                    algorithm.GetComponent<Rigidbody2D>().gravityScale = 3f;
                    algorithm.GetComponent<Rigidbody2D>().freezeRotation = true;
                    algorithm.AddComponent<Item>();
                    algorithm.tag = "Item";
                }
                else if (ran2 == 1)
                {
                    item = GameObject.Find("AlgorithmOriginal");
                    algorithm = new GameObject("Algorithm");
                    algorithm.transform.parent = itemParent.transform;
                    algorithm.transform.position = new Vector3(-20f, 1.25f, 0f);
                    algorithm.AddComponent<SpriteRenderer>();
                    algorithm.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                    algorithm.AddComponent<CircleCollider2D>();
                    algorithm.AddComponent<Rigidbody2D>();
                    algorithm.GetComponent<Rigidbody2D>().gravityScale = 3f;
                    algorithm.GetComponent<Rigidbody2D>().freezeRotation = true;
                    algorithm.AddComponent<Item>();
                    algorithm.tag = "Item";
                }
                else if (ran2 == 2)
                {
                    item = GameObject.Find("AlgorithmOriginal");
                    algorithm = new GameObject("Algorithm");
                    algorithm.transform.parent = itemParent.transform;
                    algorithm.transform.position = new Vector3(-20f, -1.25f, 0f);
                    algorithm.AddComponent<SpriteRenderer>();
                    algorithm.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                    algorithm.AddComponent<CircleCollider2D>();
                    algorithm.AddComponent<Rigidbody2D>();
                    algorithm.GetComponent<Rigidbody2D>().gravityScale = 3f;
                    algorithm.GetComponent<Rigidbody2D>().freezeRotation = true;
                    algorithm.AddComponent<Item>();
                    algorithm.tag = "Item";
                }
            }
            else if (ran >= 62.5f - ((float)level * 0.5f) && ran < 96.5f)
            {
                int ran2 = Random.Range(0, 3);
                if (ran2 == 0)
                {
                    item = GameObject.Find("BugOriginal");
                    bug = new GameObject("Bug");
                    bug.transform.parent = itemParent.transform;
                    bug.transform.position = new Vector3(-20f, 3.75f, 0f);
                    bug.AddComponent<SpriteRenderer>();
                    bug.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                    bug.AddComponent<CircleCollider2D>();
                    bug.AddComponent<Rigidbody2D>();
                    bug.GetComponent<Rigidbody2D>().gravityScale = 3f;
                    bug.GetComponent<Rigidbody2D>().freezeRotation = true;
                    bug.AddComponent<Item>();
                    bug.tag = "Item";
                }
                else if (ran2 == 1)
                {
                    item = GameObject.Find("BugOriginal");
                    bug = new GameObject("Bug");
                    bug.transform.parent = itemParent.transform;
                    bug.transform.position = new Vector3(-20f, 1.25f, 0f);
                    bug.AddComponent<SpriteRenderer>();
                    bug.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                    bug.AddComponent<CircleCollider2D>();
                    bug.AddComponent<Rigidbody2D>();
                    bug.GetComponent<Rigidbody2D>().gravityScale = 3f;
                    bug.GetComponent<Rigidbody2D>().freezeRotation = true;
                    bug.AddComponent<Item>();
                    bug.tag = "Item";
                }
                else if (ran2 == 2)
                {
                    item = GameObject.Find("BugOriginal");
                    bug = new GameObject("Bug");
                    bug.transform.parent = itemParent.transform;
                    bug.transform.position = new Vector3(-20f, -1.25f, 0f);
                    bug.AddComponent<SpriteRenderer>();
                    bug.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                    bug.AddComponent<CircleCollider2D>();
                    bug.AddComponent<Rigidbody2D>();
                    bug.GetComponent<Rigidbody2D>().gravityScale = 3f;
                    bug.GetComponent<Rigidbody2D>().freezeRotation = true;
                    bug.AddComponent<Item>();
                    bug.tag = "Item";
                }
            }
            else if (ran >= 96.5f)
            {
                int ran2 = Random.Range(0, 3);
                if (ran2 == 0)
                {
                    item = GameObject.Find("IdeaOriginal");
                    idea = new GameObject("Idea");
                    idea.transform.parent = itemParent.transform;
                    idea.transform.position = new Vector3(-20f, 3.75f, 0f);
                    idea.AddComponent<SpriteRenderer>();
                    idea.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                    idea.AddComponent<CircleCollider2D>();
                    idea.AddComponent<Rigidbody2D>();
                    idea.GetComponent<Rigidbody2D>().gravityScale = 3f;
                    idea.GetComponent<Rigidbody2D>().freezeRotation = true;
                    idea.AddComponent<Item>();
                    idea.tag = "Item";
                }
                else if (ran2 == 1)
                {
                    item = GameObject.Find("IdeaOriginal");
                    idea = new GameObject("Idea");
                    idea.transform.parent = itemParent.transform;
                    idea.transform.position = new Vector3(-20f, 1.25f, 0f);
                    idea.AddComponent<SpriteRenderer>();
                    idea.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                    idea.AddComponent<CircleCollider2D>();
                    idea.AddComponent<Rigidbody2D>();
                    idea.GetComponent<Rigidbody2D>().gravityScale = 3f;
                    idea.GetComponent<Rigidbody2D>().freezeRotation = true;
                    idea.AddComponent<Item>();
                    idea.tag = "Item";
                }
                else if (ran2 == 2)
                {
                    item = GameObject.Find("IdeaOriginal");
                    idea = new GameObject("Idea");
                    idea.transform.parent = itemParent.transform;
                    idea.transform.position = new Vector3(-20f, -1.25f, 0f);
                    idea.AddComponent<SpriteRenderer>();
                    idea.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                    idea.AddComponent<CircleCollider2D>();
                    idea.AddComponent<Rigidbody2D>();
                    idea.GetComponent<Rigidbody2D>().gravityScale = 3f;
                    idea.GetComponent<Rigidbody2D>().freezeRotation = true;
                    idea.AddComponent<Item>();
                    idea.tag = "Item";
                }
            }
        }

        if (player.life > 0f)
        {
            stageLevel += 1;
            StartCoroutine(StartGame(level + 1));
        }

        if (player.life <= 0f)
        {
            yield return new WaitForSeconds(1f);
            if (!fadeIsPlaying)
            {
                fadeIsPlaying = true;

                gameOverPanel.gameObject.SetActive(true);
                score_GameOver.gameObject.SetActive(true);
                score_GameOver.text = "Score: " + player.score.ToString();
                stage_GameOver.gameObject.SetActive(true);
                stage_GameOver.text = "Last Stage: " + player.stage.ToString();
                fadeImage.gameObject.SetActive(true);

                Color color = fadeImage.color;
                float time = 0f;
                color.a = Mathf.Lerp(1f, 0f, time);

                while (color.a > 0f)
                {
                    time += Time.deltaTime / fadeTime;
                    color.a = Mathf.Lerp(1f, 0f, time);
                    fadeImage.color = color;

                    yield return null;
                }

                fadeIsPlaying = false;
                fadeImage.gameObject.SetActive(false);
            }
        }
    }

    public void SetResolution()
    {
        int setWidth = 1920;
        int setHeight = 1080;

        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), false);

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
