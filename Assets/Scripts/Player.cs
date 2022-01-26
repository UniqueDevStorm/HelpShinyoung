using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public bool isJumping;
    public float platformSpeed;
    public float life;
    public float stage;
    public float score;
    public float time;
    public bool isStarted;
    public bool isLastPlatform;

    Rigidbody2D rigid;
    GameObject itemParent;
    GameCamera gameCamera;

    void Awake()
    {
        maxSpeed = 5f;
        jumpPower = 15f;
        isJumping = true;
        life = 3f;
        stage = 0f;
        score = 0f;
        time = 30f;
        isStarted = false;
        isLastPlatform = false;

        rigid = GetComponent<Rigidbody2D>();
        itemParent = GameObject.Find("Item");
        gameCamera = GameObject.Find("Main Camera").GetComponent<GameCamera>();

        SetUp();
    }

    void Update()
    {
        if (life <= 0f)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isJumping)
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJumping = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isJumping)
            {
                if (!isLastPlatform)
                    rigid.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (life <= 0f)
        {
            rigid.GetComponent<BoxCollider2D>().enabled = false;
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (life <= 0f)
            return;

        if (collider.gameObject.transform.parent == itemParent.transform)
        {
            Destroy(collider.gameObject);
            if (collider.gameObject.name == "Algorithm")
            {
                score += 10f;

                if (rigid.velocity.y > 0)
                    rigid.GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (collider.gameObject.name == "Idea")
            {
                life += 1f;

                if (rigid.velocity.y > 0)
                    rigid.GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (collider.gameObject.name == "Bug")
            {
                gameCamera.VibrateForTime(0.2f, 0.3f);

                life -= 1f;
                if (score >= 10f)
                    score -= 10f;
                else
                    score = 0f;

                if (rigid.velocity.y > 0)
                    rigid.GetComponent<BoxCollider2D>().enabled = false;
            }
            
            return;
        }

        if (collider.name == "Platform (3)")
        {
            if (!isLastPlatform)
                isLastPlatform = true;
        }
        else
        {
            if (isLastPlatform)
                isLastPlatform = false;
        }

        if (rigid.velocity.y < 0)
            isJumping = false;
        else
            rigid.GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (life <= 0f)
            return;

        if (collision.gameObject.transform.parent == itemParent.transform)
        {
            Destroy(collision.gameObject);
            if (collision.gameObject.name == "Algorithm")
            {
                score += 10f;

                if (rigid.velocity.y > 0)
                    rigid.GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (collision.gameObject.name == "Idea")
            {
                life += 1f;

                if (rigid.velocity.y > 0)
                    rigid.GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (collision.gameObject.name == "Bug")
            {
                gameCamera.VibrateForTime(0.2f, 0.3f);

                life -= 1f;
                if (score >= 10f)
                    score -= 10f;
                else
                    score = 0f;

                if (rigid.velocity.y > 0)
                    rigid.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (life <= 0f)
            return;

        if (collider.name == "Platform (3)")
        {
            if (rigid.velocity.y < 0)
                rigid.transform.position = new Vector3(rigid.transform.position.x, -1.25f, 0f);
        }

        rigid.GetComponent<BoxCollider2D>().enabled = true;
    }

    void SetUp()
    {
        if (!isStarted)
            isStarted = true;

        StartCoroutine(StatSystem());
    }

    IEnumerator StatSystem()
    {
        while (isStarted)
        {
            yield return new WaitForSeconds(1f);
            if (score > 0f)
                score -= 1f;
            time -= 1f;

            if (life <= 0f)
                break;
        }
    }
}
