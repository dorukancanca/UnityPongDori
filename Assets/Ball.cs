using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float speed = 1f;
    public Vector2 vel;
    public int leftPlayerScore = 0;
    public int rightPlayerScore = 0;
    public TextMeshProUGUI leftPlayerText;
    public TextMeshProUGUI rightPlayerText;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        ResetBall();
        SendBallToRandomDirection();
        leftPlayerText.text = "0";
        rightPlayerText.text = "0";
    }

    private void Update()
    {
        if (rigidbody2D.velocity.magnitude < .1f && Input.GetKeyUp(KeyCode.Space))
            SendBallToRandomDirection();
    }

    private void SendBallToRandomDirection()
    {
        rigidbody2D.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed;
        vel = rigidbody2D.velocity;
    }

    private void ResetBall()
    {
        rigidbody2D.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidbody2D.velocity = Vector2.Reflect(vel, collision.contacts[0].normal);
        vel = rigidbody2D.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (transform.position.x < 0)
            rightPlayerScore += 1;

        if (transform.position.x > 0)
            leftPlayerScore += 1;

        rightPlayerText.text = rightPlayerScore.ToString();
        leftPlayerText.text = leftPlayerScore.ToString();
        ResetBall();
        
    }
}
