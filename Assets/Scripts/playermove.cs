using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playermove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 좌우 이동
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
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
            ShowDeathNum.death++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
