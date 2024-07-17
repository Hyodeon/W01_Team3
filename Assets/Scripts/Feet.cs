using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    public playermove player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || (collision.gameObject.CompareTag("Dust")))
        {
            player.isGrounded = true;
        }
    }
}
