using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playermove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded = true;
    private bool isStoped = false;
    private Rigidbody2D rb;

    private Vector2 postForce;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 좌우 이동
        float moveInput = Input.GetAxis("Horizontal");
        if(!isStoped)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        
        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isStoped)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            if(isStoped)
            {
                isStoped = false;
                rb.gravityScale = 1;
                rb.velocity = postForce;
            }
            else
            {
                isStoped = true;
                postForce = rb.velocity;
                rb.velocity = Vector2.zero;
                rb.totalForce = Vector2.zero;
                rb.gravityScale = 0;
                
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Clear");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bound"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
