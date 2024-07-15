using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playermove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // �¿� �̵�
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // ����
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Bound"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Clear");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isGrounded = false;
        }
    }
}
