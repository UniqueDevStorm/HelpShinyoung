using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float maxSpeed;

    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 7.5f;

        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x > 1f)
            Destroy(gameObject);

        if (GameObject.Find("ShinYoung").GetComponent<Player>().life <= 0f)
            Destroy(gameObject);

        if (GameObject.Find("ShinYoung").GetComponent<Player>().score < 0f)
            Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            rigid.AddForce(Vector2.right * 0.5f, ForceMode2D.Impulse);

            if (rigid.velocity.x > maxSpeed)
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
    }
}
