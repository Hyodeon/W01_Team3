using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Laser _shooter;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void Initialize(GameObject shooter)
    {
        _shooter = shooter.GetComponent<Laser>();
        _collider = GetComponent<BoxCollider2D>();
        _collider.size = new Vector2(1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Detected new object on Laser");
        //if (!collision.gameObject.CompareTag("Player")) _shooter.GenerateLaser();
    }
}
