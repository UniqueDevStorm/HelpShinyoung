using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    Player player;
    Text stage;
    Text time;
    Text score;
    Text life;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ShinYoung").GetComponent<Player>();
        stage = GameObject.Find("Stage").GetComponent<Text>();
        time = GameObject.Find("Time").GetComponent<Text>();
        score = GameObject.Find("Score").GetComponent<Text>();
        life = GameObject.Find("Life").GetComponent<Text>();

        SetResolution();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.stage <= 9999999f)
            stage.text = "Stage: " + player.stage.ToString();
        else
            stage.text = "Stage: 9999999+";

        if (player.time >= 10f)
            time.text = "Time: " + player.time.ToString();
        else
            time.text = "Time: 0" + player.time.ToString();

        if (player.score <= 9999999f)
            score.text = "Score: " + player.score.ToString();
        else
            score.text = "Score: 9999999+";

        if (player.life <= 99f)
            life.text = "Life: " + player.life.ToString();
        else
            life.text = "Life: 99+";
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
